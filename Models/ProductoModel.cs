using System;
namespace systemMantenimiento.Models
{
	public class ProductoModel
	{
        public int ProductoId { get; set; }
        public string Producto { get; set; }
        public double Costo { get; set; }
        public int Serie { get; set; }
        public int Stock { get; set; }
        public string Estado { get; set; }
    }
}

