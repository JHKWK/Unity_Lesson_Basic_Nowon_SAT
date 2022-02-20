using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 숙제_경마게임
{
    internal class Horse
    {
        public string name;
        public float speed;
        public float distance = 0;
        public int rank = 0;

        public void Run(float b)
        {
            float a = speed;            
            float c = a * b;
            distance += c;
        }
    }

}
