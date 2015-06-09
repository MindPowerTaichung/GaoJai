using MPERP2015.Membership.Models;
using MPERP2015.MP;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;

namespace MPERP2015.Controllers
{
    public class MembershipController : ApiController
    {
        MembershipModelContainer db = new MembershipModelContainer();

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
        // GET: api/Roles
        [Route("Membership/Roles")]
        public IEnumerable<RoleViewModel> GetRoles()
        {
            var roles = db.Roles.ToArray<Role>().Select(item => ToRoleViewModel(item));
            return roles;
        }

        // GET: api/Roles/5
        [Route("Membership/Roles", Name="GetRoleById")]
        public IHttpActionResult GetRoles(int id)
        {
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }

            return Ok(ToRoleViewModel(role));
        }

        // POST: api/Roles
        [HttpPost]
        [Route("Membership/Roles")]
        public IHttpActionResult PostRole(RoleViewModel role_viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Role role = new Role { Id = role_viewModel.Id, Name = role_viewModel.Name };
            db.Roles.Add(role);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var entityError = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                var getFullMessage = string.Join("; ", entityError);
                var exceptionMessage = string.Concat(ex.Message, "errors are: ", getFullMessage);
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, exceptionMessage));
            }

            return CreatedAtRoute("GetRoleById", new { id = role.Id }, ToRoleViewModel(role));
        }

        // PUT: api/Roles/5
        [HttpPut]
        [Route("Membership/Roles/{id}")]
        public IHttpActionResult PutRole(int id, RoleViewModel role_viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != role_viewModel.Id)
                return BadRequest();

            //把資料庫中的那筆資料讀出來
            var role_db = db.Roles.Find(id);
            if (role_db == null)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "這筆資料已被刪除!"));
            }
            else
            {
                try
                {
                    role_db.Name = role_viewModel.Name;
                    db.Entry(role_db).OriginalValues["Timestamp"] = Convert.FromBase64String(role_viewModel.TimestampString);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.Roles.Find(id) == null)
                        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound, "這筆資料已被刪除!"));
                    else
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict, "這筆資料已被其他人修改!"));
                }
            }

            return Ok(ToRoleViewModel(role_db));

        }

        // DELETE: api/Roles/5
        [Route("Membership/Roles/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteRole(int id)
        {
            Role role_db = db.Roles.Find(id);
            if (role_db == null)
            {
                return NotFound();
            }

            db.Roles.Remove(role_db);
            db.SaveChanges();

            return Ok(new RoleViewModel { Id = id });
        }

        private RoleViewModel ToRoleViewModel(Role role)
        {
            return new RoleViewModel { Id = role.Id, Name = role.Name, TimestampString = Convert.ToBase64String(role.Timestamp) };
        }
        #endregion

        #region Membership/RoleMenu
        // POST: Membership/RoleMenu
        [HttpPost]
        [Route("Membership/RoleMenu")]
        public IHttpActionResult PostRoleMenu(RoleMenuViewModel roleMenu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var role = db.Roles.Find(roleMenu.RoleId);
            if (role == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound, "不存在的RoleId!"));

            var menus = db.Menus.Where(item => item.Id==roleMenu.MenuId || item.ParentId==roleMenu.MenuId);

            foreach (var item in menus)
            {
                role.Menus.Add(item);
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            return Ok();
        }

        // DELETE: Membership/RoleMenu
        [Route("Membership/RoleMenu")]
        [HttpDelete]
        public IHttpActionResult DeleteRoleMenu(RoleMenuViewModel roleMenu)
        {
            var role = db.Roles.Find(roleMenu.RoleId);
            if (role == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound, "不存在的RoleId!"));

            var menus = db.Menus.Where(item => item.Id == roleMenu.MenuId || item.ParentId == roleMenu.MenuId);

            foreach (var item in menus)
            {
                role.Menus.Remove(item);
            }
            
            db.SaveChanges();

            return Ok();
        }
        #endregion

        #region Membership/UserMenu
        // POST: Membership/UserMenu
        [HttpPost]
        [Route("Membership/UserMenu")]
        public IHttpActionResult PostUserMenu(UserMenuViewModel userMenu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = db.Users.Find(userMenu.UserName);
            if (user == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound, "不存在的使用者!"));

            var menus = db.Menus.Where(item => item.Id == userMenu.MenuId || item.ParentId == userMenu.MenuId);

            foreach (var item in menus)
            {
                user.Menus.Add(item);
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            return Ok();
        }

        // DELETE: Membership/UserMenu
        [Route("Membership/UserMenu")]
        [HttpDelete]
        public IHttpActionResult DeleteUserMenu(UserMenuViewModel userMenu)
        {
            var user = db.Users.Find(userMenu.UserName);
            if (user == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound, "不存在的使用者!"));

            var menus = db.Menus.Where(item => item.Id == userMenu.MenuId || item.ParentId == userMenu.MenuId);

            foreach (var item in menus)
            {
                user.Menus.Remove(item);
            }

            db.SaveChanges();

            return Ok();
        }
        #endregion

        #region Membership/Users
        [Route("Membership/Users")]
        public IEnumerable<UserViewModel> GetUsers()
        {
            var users = db.Users.ToArray<User>().Select(u => ToUserViewModel(u));
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
            return ToUserViewModel(user);
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
            return CreatedAtRoute("GetUserByUserName", new { userName = user.UserName }, ToUserViewModel(user));
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

            return Ok(ToUserViewModel(user_db));

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

        private UserViewModel ToUserViewModel(User user)
        {
            return new UserViewModel { UserName = user.UserName, TimestampString = Convert.ToBase64String(user.Timestamp) };
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
