using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWeb.Models
{
    public class User
    {
        public int iduser { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
    }
}