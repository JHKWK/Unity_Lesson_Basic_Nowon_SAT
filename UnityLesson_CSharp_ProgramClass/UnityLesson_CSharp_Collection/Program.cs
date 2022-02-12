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
            //Dictionary
            //------------------------------------------------------------------------------
            Dictionary<string,string>_dic=new Dictionary<string,string>();
            _dic.Add("검사", "양손대검을 사용하여 물리공격을 하는 클래스");
            _dic.Add("마법사", "지팡이를 사용하여 마법공격을 하는 클래스");
            _dic.Add("수호자", "창과 방패를 사용하여 물리공격 및 방어 위주의 클래스");
            //_dic.Remove("검사");
            bool isSwordMasterExist = _dic.ContainsKey("검사");
            if (isSwordMasterExist)
            {
                string tmpValue = _dic["검사"];
                Console.WriteLine($"검사 : {tmpValue}");
            }
            else
            {
                Console.WriteLine("검사가 없습니다");
            }
            // dictionary 도 foreach 구문의 가능하다.
            // dictionary 는 collection이고 
            // dictionary 에서 keys를 가져오면 KeyCollection을 가져올 수 있고
            // dictionary에서 values를 가져오면 ValueCollection을 가져올 수 있다.
            // dictionary 자체도 key, value 각각도 foreach 구문이 가능하다.            

            //dictionary를 foreach문 실행
            foreach (KeyValuePair<string,string> sub in _dic)
            {
                string tmpKey = sub.Key;
                string tmpValue = sub.Value;
                Console.WriteLine($"{tmpKey} : {tmpValue}");
                Console.WriteLine(sub);
            }

            //Keys를 foreach문 실행
            foreach (string sub in _dic.Keys)
            {
                string tmpValue = _dic[sub];
                Console.WriteLine($"{sub} : {tmpValue}");
            }

            //dictionary.Values를 foreach문 실행
            foreach (string sub in _dic.Values)
            {
                Console.WriteLine(sub);
            }

            //------------------------------------------------------------------------------
            //Queue {List와 비슷하나, FIFO, First Input First Ouput 체계이다}
            //------------------------------------------------------------------------------
            Queue<int>_queue = new Queue<int>();
            _queue.Enqueue(10);
            _queue.Enqueue(20);
            _queue.Enqueue(30);
            //_queue.Dequeue(); // 파라미터를 넣지 않는다
            Console.WriteLine(_queue.Peek()); // 가장 앞에있는 밸류 호출
            Console.WriteLine(_queue.Dequeue());
            Console.WriteLine(_queue.Peek());
            Console.WriteLine(_queue.Dequeue());
            Console.WriteLine(_queue.Peek());
            Console.WriteLine(_queue.Dequeue());
            //------------------------------------------------------------------------------
            //Stack (List와 비슷하나, LIFO, Last Input First OUT체계 이다)
            //------------------------------------------------------------------------------
            Stack<int> _stack = new Stack<int>();
            _stack.Push(11);
            _stack.Push(22);
            _stack.Push(33);
            Console.WriteLine(_stack.Peek());
            Console.WriteLine(_stack.Pop());
            Console.WriteLine(_stack.Peek());
            Console.WriteLine(_stack.Pop());
            Console.WriteLine(_stack.Peek());
            Console.WriteLine(_stack.Pop());

        }
    }            
}
