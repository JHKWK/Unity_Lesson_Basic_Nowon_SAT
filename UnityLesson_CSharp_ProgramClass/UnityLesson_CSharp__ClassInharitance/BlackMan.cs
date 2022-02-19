using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityLesson_CSharp__ClassInharitance
{
    internal class BlackMan : Human
    {
        public override void Breath()
        {
            age++;
            height += 0.00007f;
            weight += 0.00003f;
        }
    }
}
