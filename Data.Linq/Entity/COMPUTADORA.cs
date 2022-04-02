using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Linq.Entity
{
    public partial class COMPUTADORA
    {
        public override string ToString()
        {
            return this.Id.ToString();
        }
    }
}
