using System;
using System.Collections.Generic; // Collection을 사용 하려면 추가해야 함 .Generic -> 정규화, 다양한 타입을 대입할 수 있게 한다

namespace UnityLesson_CSharp_Collection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //------------------------------------------------------------------------------
            // List
            // Generic 타입의 class는 <>를 사용한다
            //------------------------------------------------------------------------------
            List<int> _list = new List<int>();
            _list.Add(3); //3이 List에 추가됨
            _list.Add(2);
            _list.Add(1);
            _list.Add(0);
            _list.Add(3);
            // 첫번째로 찾은 오브젝트를 지운다 (첫번째3이 지워지고 2103이 된다)
            // 0번째 인덱스 부터 탐색하고, 첫번째로 파라미터와 같은 요소를 발견하면 삭제
            // 삭제 성공 시 true를 반환, 아니면 false를 반환
            _list.Remove(3);
            int length=_list.Count; // 동적배열의 길이는 Length가 아니고 Count이다
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(_list[i]);
            }
            // foreach는 collection이 가지고 있는 타입의 아이템만큼 반복문을 실행하면서 각 아이템을 반환 해준다
            // num에 리스트 0번째부터 n번째까지 
            foreach (int num in _list)
            {
                Console.WriteLine(num);
            }
            //------------------------------------------------------------------------------
            //------------------------------------------------------------------------------
            //------------------------------------------------------------------------------
        }
    }            
}
