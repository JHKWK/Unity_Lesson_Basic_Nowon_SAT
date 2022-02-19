using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityLesson_CSharp__ClassInharitance
{
    internal class WhiteMan : Human
    {
        public override void Breath()
        {
            age++;
            height += 0.00005f;
            weight += 0.00006f;
        }
    }
}
