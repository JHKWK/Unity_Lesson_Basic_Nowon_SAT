using System;

namespace UnityLesson_CSharp_Array
{
    internal class Program
    {
        // array
        // 형태 : 자료형[] 
        // 자료형이 '정적'으로 나열되어 있는 형태.. 
        // 크기를 한번 정해 놓으면 바꿀 수 없다.

        static int[] arr_TestInt = new int[5]; //int 형태 자료 5개를 넣을 수있는 배열
        static float[] arr_TestFloat = new float[3]; // 실수 3개를 저장할수 있는 저장공간이 생김
        static float[] arr_TestFloat2 = { 11.1f, 22.2f, 33.3f, 44.4f }; // = 크기는 [4], 값도 동시에 할당
        static string[] arr_TestString = new string[3];
                

        static void Main(string[] args)
        {
            // 배열은 0부터 시작 0,1,2,3,4
            arr_TestInt[0] = 5;
            arr_TestInt[1] = 4;
            arr_TestInt[2] = 3;
            arr_TestInt[3] = 2;
            arr_TestInt[4] = 1;

            Console.WriteLine(arr_TestInt); // type만 출력이 된다
            Console.WriteLine(arr_TestInt[0]);
            Console.WriteLine(arr_TestInt[1]);
            Console.WriteLine(arr_TestInt[2]);
            Console.WriteLine(arr_TestInt[3]);
            Console.WriteLine(arr_TestInt[4]);

            //=======================================================

            arr_TestFloat[0] = 1.1f;
            arr_TestFloat[1] = 2.2f;
            arr_TestFloat[2] = 3.3f;

            Console.WriteLine(arr_TestFloat[0]);
            Console.WriteLine(arr_TestFloat[1]);
            Console.WriteLine(arr_TestFloat[2]);

            //========================================================

            Console.WriteLine(arr_TestFloat2[0]);
            Console.WriteLine(arr_TestFloat2[1]);
            Console.WriteLine(arr_TestFloat2[2]);
            Console.WriteLine(arr_TestFloat2[3]);

            //========================================================

            arr_TestString[0] = "하나";
            arr_TestString[1] = "둘";
            arr_TestString[2] = "셋";

            Console.WriteLine(arr_TestString[0]);
            Console.WriteLine(arr_TestString[1]);
            Console.WriteLine(arr_TestString[2]);
                        

        }
    }
}
