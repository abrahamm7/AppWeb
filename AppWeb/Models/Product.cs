using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWeb.Models
{
    public class Product
    {
        public int IDproduct { get; set; }
        public int IDCategory { get; set; }
        public int IDCategoryC { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
    }
}