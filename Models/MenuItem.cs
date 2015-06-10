using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPERP2015.Membership.Models
{
    public class MenuItem
    {
        public string Text { get; set; }
        public string ContentUrl { get; set; }
        public string SpriteCssClass { get; set; }
        public bool HasChildren { get; set; }
        public List<MenuItem> Items { get; set; }

        //private List<MenuItem> _items;
        //public List<MenuItem> Items
        //{
        //    get
        //    {
        //        if (_items == null)
        //        {
        //            _items = new List<MenuItem>();
        //        }
        //        return _items;
        //    }
        //}

    }


}