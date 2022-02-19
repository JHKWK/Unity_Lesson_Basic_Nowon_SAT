using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityLesson_CSharp__ClassInharitance
{
    internal class Creature
    {
        public string DNA;
        public float age;
        public float weight;

        // "virtual" 해당 함수를 오버라이딩이 가능 하도록 해준다.
        // 부모클래스의 함수라고 해서 전부 virtual을 붙이는것이 아니라,
        // 자식 클래스가 해당함수를 반드시 재정의 해야 할 때만 virtual을 붙인다.
        virtual public void Breath()
        {
            age++;        
        }

    }
}
