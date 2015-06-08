using MPERP2015.MP;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MPERP2015.Controllers
{
    public class MenusController : ApiController
    {
        MembershipModelContainer db = new MembershipModelContainer();

        // GET: api/Menus/Json
        [Route("api/Menus/Json")]
        public IEnumerable<MenuViewModel> GetJson()
        {
            var items = GetMenus(db.Menus.ToList(),0);
            return items;
        }
        List<MenuViewModel> GetMenus(List<Menu> list, int parentId)
        {
            var items= list.Where(x => x.ParentId == parentId).Select(x => new MenuViewModel
            {
                Id = x.Id,
                Text = x.Text,
                ContentUrl = x.ContentUrl,
                ParentId = x.ParentId,
                CssClass= x.CssClass,
                Checked=x.Id==1 || x.Id==2,
                TimestampString= Convert.ToBase64String( x.Timestamp),
                SubMenus = GetMenus(list, x.Id)
            }).ToList();

            foreach (var item in items)
            {
                item.hasChildren = item.SubMenus.Count > 0;    
            }
            return items;
        }        

        // GET: api/Menus
        public IEnumerable<MenuViewModel> Get()
        {
            var items = db.Menus.ToArray<Menu>().Select(item => ToMenuViewModel(item));
            return items;
        }
        // GET: api/Menus/1
        [Route("api/Menus/Role/{roleId}")]
        public IEnumerable<MenuViewModel> GetMenusByRole(int roleId)
        {
            return db.Roles.Find(roleId).Menus.ToArray<Menu>().Select(item => ToMenuViewModel(item));
        }

        // GET: api/Menus/5
        public IHttpActionResult Get(int id)
        {
            Menu item = db.Menus.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(ToMenuViewModel(item));
        }

        // POST: api/Menus
        [HttpPost]
        public IHttpActionResult Post(MenuViewModel item_viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Menu item = new Menu { Id = item_viewModel.Id, 
                                                 Text = item_viewModel.Text, 
                                                 ContentUrl=item_viewModel.ContentUrl,
                                                 CssClass=item_viewModel.CssClass,
                                                 ParentId= item_viewModel.ParentId};
            db.Menus.Add(item);
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
            catch (DbUpdateException ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message));
            }

            return CreatedAtRoute("DefaultApi", new { id = item.Id }, ToMenuViewModel(item));
        }

        // PUT: api/Menus/5
        [HttpPut]
        public IHttpActionResult Put(int id, MenuViewModel item_viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != item_viewModel.Id)
                return BadRequest();

            //把資料庫中的那筆資料讀出來
            var item_db = db.Menus.Find(id);
            if (item_db == null)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "這筆資料已被刪除!"));
            }
            else
            {
                try
                {
                    item_db.Text = item_viewModel.Text;
                    item_db.ContentUrl = item_viewModel.ContentUrl;
                    item_db.CssClass = item_viewModel.CssClass;
                    item_db.ParentId = item_viewModel.ParentId;
                    db.Entry(item_db).OriginalValues["Timestamp"] = Convert.FromBase64String(item_viewModel.TimestampString);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.Menus.Find(id) == null)
                        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound, "這筆資料已被刪除!"));
                    else
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict, "這筆資料已被其他人修改!"));
                }
            }

            return Ok(ToMenuViewModel(item_db));

        }

        // DELETE: api/Menus/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            Menu item_db = db.Menus.Find(id);
            if (item_db == null)
            {
                return NotFound();
            }

            //db.Menus.Remove(item_db);            
            db.Menus.RemoveRange(db.Menus.Where(item=> item.ParentId==id || item.Id==id));
            db.SaveChanges();

            return Ok(new MenuViewModel { Id = id });
        }

        private MenuViewModel ToMenuViewModel(Menu item)
        {
            return new MenuViewModel { Id = item.Id, Text = item.Text, TimestampString = Convert.ToBase64String(item.Timestamp),
                                                      ContentUrl=item.ContentUrl, CssClass=item.CssClass, ParentId=item.ParentId};
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

