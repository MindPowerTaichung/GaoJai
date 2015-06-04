﻿using MPERP2015.Membership.Models;
using MPERP2015.MP;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace MPERP2015.Controllers
{
    public class MembershipController : ApiController
    {
        MembershipModelContainer db = new MembershipModelContainer();

        private UserViewModel ToViewModel(User user)
        {
            return new UserViewModel { UserName = user.UserName, TimestampString = Convert.ToBase64String(user.Timestamp) };
        }

        #region Membership/Menu
        [Route("Membership/Menu")]
        public IEnumerable<MenuItem> Get()
        {
            List<MenuItem> menus = new List<MenuItem>();

            MenuItem menu = new MenuItem { Text = "產品", SpriteCssClass = "fa fa-cubes fa-fw" };
            menu.Items.Add(new MenuItem { Text = "產品", SpriteCssClass = "fa fa-cubes fa-fw", ContentUrl = "views/products.html" });
            menu.Items.Add(new MenuItem { Text = "分類", SpriteCssClass = "fa fa-cubes fa-fw", ContentUrl = "views/categories.html" });
            menus.Add(menu);

            menus.Add(new MenuItem { Text = "客戶", ContentUrl = "views/customers.html", SpriteCssClass = "fa fa-user-md fa-fw" });

            MenuItem menuOrder = new MenuItem { Text = "訂單", SpriteCssClass = "fa fa-table" };
            menuOrder.Items.Add(new MenuItem { Text = "查詢", ContentUrl = "views/ordersSearch.html", SpriteCssClass = "fa fa-table fa-fw" });
            menuOrder.Items.Add(new MenuItem { Text = "新增", ContentUrl = "views/ordersInsert.html", SpriteCssClass = "fa fa-table fa-fw" });
            menus.Add(menuOrder);

            menus.Add(new MenuItem { Text = "系統管理", ContentUrl = "views/settings.html", SpriteCssClass = "fa fa-wrench fa-fw" });
            return menus;
        }
        #endregion

        #region Membership/Roles
        [Route("Membership/Roles")]
        public IEnumerable<Role> GetRoles()
        {
            var roles = db.Roles.ToArray<Role>();
            return roles;
        }
        #endregion

        #region Membership/Users
        [Route("Membership/Users")]
        public IEnumerable<UserViewModel> GetUsers()
        {
            var users = db.Users.ToArray<User>().Select(u => ToViewModel(u));
            return users.AsQueryable();
        }

        [Route("Membership/Users/{userName}", Name="GetUserByUserName")]
        public UserViewModel GetUsers(string userName)
        {
            var user = db.Users.Find(userName);
            if (user==null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, "找不到資料")); 
            }
            return ToViewModel(user);
        }

        // POST: Membership/Users
        [ResponseType(typeof(UserViewModel))]
        [Route("Membership/Users")]
        [HttpPost]
        public IHttpActionResult PostUser(UserPasswordViewModel user_view_model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = db.Users.Find(user_view_model.UserName);
            if (user == null)
            {
                user = new User { UserName = user_view_model.UserName, Password = user_view_model.Password };
                db.Users.Add(user);
                try
                {
                    db.SaveChanges();                    
                }
                catch (Exception ex)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message)); 
                }
            }
            return CreatedAtRoute("GetUserByUserName", new { userName = user.UserName }, ToViewModel(user));
        }

        // PUT: Membership/Users/{userName}
        [ResponseType(typeof(UserViewModel))]
        [Route("Membership/Users/{userName}")]
        [HttpPut]
        public IHttpActionResult PutUser(string userName, UserPasswordViewModel user_view_model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (userName != user_view_model.UserName)
                return BadRequest();

            //把資料庫中的那筆資料讀出來
            var user_db = db.Users.Find(userName);
            if (user_db == null)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "這筆資料已被刪除!"));
            }
            else
            {
                try
                {
                    user_db.Password = user_view_model.Password;
                    db.Entry(user_db).OriginalValues["Timestamp"] = Convert.FromBase64String(user_view_model.TimestampString);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userName))
                        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound, "這筆資料已被刪除!"));
                    else
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict, "這筆資料已被其他人修改!"));// ""
                }
            }

            return Ok(ToViewModel(user_db));

        }

        // DELETE: Membership/Users/{userName}
        [ResponseType(typeof(UserViewModel))]
        [Route("Membership/Users/{userName}")]
        [HttpDelete]
        public IHttpActionResult DeleteUser(string userName)
        {
            User user = db.Users.Find(userName);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message));
            }

            return Ok(new UserViewModel { UserName=userName});
        }
        #endregion

        private bool UserExists(string userName)
        {
            return db.Users.Count(e => e.UserName == userName) > 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}