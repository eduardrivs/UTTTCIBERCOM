using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Linq.Entity
{
    public partial class EMPLEADO
    {
        public override string ToString()
        {
            return this.strNombre + " " + this.strAPaterno + " " + this.strAMaterno;
        }
    }
}
