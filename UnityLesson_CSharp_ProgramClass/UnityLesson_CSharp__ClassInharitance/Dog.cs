using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityLesson_CSharp__ClassInharitance
{
    internal class Dog : Creature, IFourLeggedWalker
    
    {
        public float tailLength;

        public void FourLeggedWlk()
        {
            Console.WriteLine("네발로 걷는다");
        }
    }
}
