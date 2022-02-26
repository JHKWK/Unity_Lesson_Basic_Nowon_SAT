using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRacing
{
    internal class Horse
    {
        public string name;
        public int distance;
        public bool available = true;
        public void Run(int moveDistance)
        {            
            distance +=moveDistance;
        }
    }
}
