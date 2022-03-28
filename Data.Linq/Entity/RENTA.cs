using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Linq.Entity
{
    public partial class RENTA
    {
        public override string ToString()
        {
            return this.dteFechaInicio.ToString();
        }
    }
}
