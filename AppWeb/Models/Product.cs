using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWeb.Models
{
    public class Product
    {
        public int IDProduct { get; set; }
        public int IDCategory { get; set; }
        public string NameProduct { get; set; }
        public string PriceProduct { get; set; }
        public string CategoryName { get; set; }
        public int UserId { get; set; }
        public int Stock { get; set; }
    }
}