using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWeb.Models
{
    public class Category
    {
        public int IDCategory { get; set; }
        public string CategoryName { get; set; }
        public string DescriptionCategory { get; set; }
        public DateTime DateCreated { get; set; }
    }
}