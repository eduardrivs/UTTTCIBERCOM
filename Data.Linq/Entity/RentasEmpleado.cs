using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Linq.Entity
{
    public class RentasEmpleado
    {
        public int Id { get; set; }
        public DateTime dteFechaInicio { get; set; }
        public DateTime dteFechaFinal { get; set; }
        public double dteTiempoTotal { get; set; }
        public int idEmpleado { get; set; }
        public int idEquipo { get; set; }
        public decimal monSubtotal { get; set; }
        public decimal monIVA { get; set; }
        public decimal monTotal { get; set; }
        public decimal monPago { get; set; }
        public decimal monCambio { get; set; }
        public String strEmpNombre{ get; set; }
    }
}
