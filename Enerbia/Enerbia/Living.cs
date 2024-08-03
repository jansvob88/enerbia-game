using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enerbia
{
    class Living: Item
    {

        public Living(int id, int evidenceNumber, string name, int modIndex) : base(id, evidenceNumber, name, modIndex)
        {
            className = "Living";
        }
    }
}
