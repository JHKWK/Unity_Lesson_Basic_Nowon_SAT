using System;

namespace UnityLesson_CSharp_WhileLoop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*//while의 구조
            //while (조건)
            {
                조건이 참일 때 반복 할 내용
            }

            // 무한루프 (while의 조건이 항상 참일경우)
            // 절대 코드에 있어서는 안되는 존재
            // 실수로 무한루프가 생길 수 있어 while문은 되도록 사용하지 않는다.
            while (true)
            {

            }*/

            string[] arr_PersonName = new string[3];
            {
                arr_PersonName[0] = "김일남";
                arr_PersonName[1] = "김차남";
                arr_PersonName[2] = "김삼남";

                int length = arr_PersonName.Length;
                
                for (int i=0; i < length; i++)
                {
                    Console .WriteLine(arr_PersonName[i]);
                }


                // 김 아무개만 출력을
                for (int i = 0; i < length; i++)
                {
                    if (arr_PersonName[i] == "김아무개")
                    {
                        Console.WriteLine(arr_PersonName[i]);
                    }
                    
                }


                /*
                while (count < length)
                {
                    Console.WriteLine(arr_PersonName[count]);
                    count++;
                }

                
                while (true)
                {
                    if (count<length)
                    {
                        Console.WriteLine(arr_PersonName[count]);
                    }
                    else
                    {
                        break; //if문을 빠져나오는 명령어
                    }
                    
                }
                */



            }
        }
    }
}
