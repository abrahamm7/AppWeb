using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWeb.Models
{
    public class Receip
    {
        public int IDReceip { get; set; }
        public string ProductName { get; set; }
        public string PriceProduct { get; set; }
        public int UserId { get; set; }
        public int IdProduct { get; set; }
    }
}