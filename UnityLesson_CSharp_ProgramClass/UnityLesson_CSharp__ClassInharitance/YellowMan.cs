using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityLesson_CSharp__ClassInharitance
{
    internal class YellowMan : Human
    {
        public override void Breath()
        {
            age++;
            height += 0.00003f;
            weight += 0.00001f;
        }
    }
}
