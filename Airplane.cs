using engine.Common.Entities3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design.Behavior;

namespace landscape
{
    class Airplane : ComboElement3D
    {
        public Airplane()
        {
            // airplane fuselage
            var body = new Cylinder() { X = X, Y = Y, Z = Z, };

            // todo

        }
    }
}
