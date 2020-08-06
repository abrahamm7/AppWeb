using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWeb.Models
{
    public class Product
    {
        public int ProductoId { get; set; }
        public int CategoriaId { get; set; }
        public string Nombre { get; set; }
        public string Precio { get; set; }
        public string Categoria { get; set; }
        public int Usuario { get; set; }
    }
}