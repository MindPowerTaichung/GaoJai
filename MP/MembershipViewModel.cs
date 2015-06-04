using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPERP2015.MP
{
    public class UserPasswordViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string TimestampString { get; set; }
    }
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string TimestampString { get; set; }
    }

    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TimestampString { get; set; }
    }

    public class MenuViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ContentUrl { get; set; }
        public string CssClass { get; set; }
        public int ParentId { get; set; }

        public string TimestampString { get; set; }

        //private List<MenuViewModel> _items;
        //public List<MenuViewModel> SubItems
        //{
        //    get
        //    {
        //        if (_items == null)
        //        {
        //            _items = new List<MenuViewModel>();
        //        }
        //        return _items;
        //    }
        //}

    }
}