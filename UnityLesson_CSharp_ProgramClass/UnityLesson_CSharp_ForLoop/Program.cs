using System;

namespace UnityLesson_CSharp_ForLoop
{
    internal class Program
    {
        static void Main(string[] args)
        {


            /*
                int length = 5;

                for (int i = 0; i < length; i++) // ;부호 <- 한 문장이 끝난 것 으로 인식
                                                 // int변수 i를 선언하고 0으로 초기화
                                                 // length변수 호출하여 for문을 실행 할 조건 선언
                                                 // i++ -> 루프가 한사이클을 돌고 난 후 수행할 동작 
                {
                    //반복 수행 시 실행할 내용

                }
            */

            string[] arr_PersonName = new string[8];
            {
                arr_PersonName[0] = "김일남";
                arr_PersonName[1] = "김차남";
                arr_PersonName[2] = "김삼남";
                arr_PersonName[3] = "김사남";
                arr_PersonName[4] = "김오남";
                arr_PersonName[5] = "김육남";
                arr_PersonName[6] = "김칠남";
                arr_PersonName[7] = "김팔남";


                int length = arr_PersonName.Length;

                for (int i = 0; i < length; i++)
                {
                    Console.WriteLine(arr_PersonName[i]);
                }

                Console.WriteLine("김아무개만 출력");
                // 김 아무개만 출력을 하는경우
                // 모든 배열 요소를 검색하는 예시
                for (int i = 0; i < length; i++)
                {
                    if (arr_PersonName[i] == "김차남")
                    {
                        Console.WriteLine(arr_PersonName[i]);
                    }

                }

                Console.WriteLine("인덱스 규칙");
               
                // 인덱스 규칙을 활용한 예시
                // 3명 증가
                for (int i = 0; i < length; i+=3)
                {
                    Console.WriteLine(arr_PersonName[i]);

                }
            }
        }
    }
}