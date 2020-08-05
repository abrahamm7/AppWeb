using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWeb.Models
{
    public class Factura
    {
        public int IDReceip { get; set; }
        public string NombreProducto { get; set; }
        public string PrecioProducto { get; set; }
        public int UsuarioID { get; set; }
        public int IdProduct { get; set; }
    }
}