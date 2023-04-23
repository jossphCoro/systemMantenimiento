using System;
namespace systemMantenimiento.Models
{
	public class AccesorioModel
	{
        public int AccesorioId { get; set; }
        public string Accesorio { get; set; }
        public string Descripcion { get; set; }
        public double Costo { get; set; }
        public int Cantidad { get; set; }
        public string Estado { get; set; }
        
    }
}

