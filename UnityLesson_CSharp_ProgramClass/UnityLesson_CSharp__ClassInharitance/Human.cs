using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityLesson_CSharp__ClassInharitance
{
    internal class Human : Creature, ITwoLeggedWalker
    {
        
        
        public float height;
        // 'override' 부모의 virtual 키워드가 붙은 함수를 재정의 하는 키워드
        public override void Breath()
        {
            base.Breath(); // 부모의 내용을 받아서 쓴다. 필요 없으면 지운다.
            height += 0.00004f;
            weight += 0.00002f;
        }

        public void TwoLeggedWalk()
        {
            Console.WriteLine("두발로 걷는다");
        }
    }
}
