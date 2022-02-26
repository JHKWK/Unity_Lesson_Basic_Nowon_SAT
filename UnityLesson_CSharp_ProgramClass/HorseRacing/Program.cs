using System;
using System.Threading;

namespace HorseRacing
{
    internal class Program
    {
        static Random random;
        static bool isGameFinished = false;
        static int minSpeed = 10;
        static int maxSpeed = 20;
        static int finishDistance = 200;
        static void Main(string[] args)
        {
            Horse[] arr_horse = new Horse[5]; //말 5마리 배열
            string[] arr_finishedHorseName = new string[5]; // 결승점 통과한 말들의 이름
            int currentGrade = 1; // 현재 등수

            // 말 생성 및 초기화
            int length = arr_horse.Length;
            for (int i = 0; i < length; i++)
            {
                arr_horse[i] = new Horse(); // 말 생성
                arr_horse[i].name = $"경주마{i + 1}"; //말의 이름 설정
            }

            Console.WriteLine("시작 하려면 엔터를 누르세요");
            Console.ReadLine();
            Console.WriteLine("경주 시작!");
            int count = 0;

            while (isGameFinished == false) // 게임이 끝날 때 가지 돌아가는 반복문
            {
                Thread.Sleep(1000); // 1초 지연
                count++;
                Console.WriteLine($"=========={count}초==========");
                for (int i = 0; i < length; i++)
                {
                    if (arr_horse[i].available)
                    {
                        random = new Random(); // 난수 인스턴스화
                        int tmpMoveDistance = random.Next(minSpeed, maxSpeed + 1);
                        arr_horse[i].Run(tmpMoveDistance); //i 번째 말을 10~20 사이 거리만큼 움직임
                        Console.WriteLine($"{arr_horse[i].name} : {arr_horse[i].distance} m");
                        //결승점 도착 체크

                        if (arr_horse[i].distance >= finishDistance)
                        {
                            arr_finishedHorseName[currentGrade - 1] = arr_horse[i].name;
                            currentGrade++;
                            arr_horse[i].available = false;
                        }
                    }
                }
                Console.WriteLine("=======================");
                //경주 끝났는지 체크 {모든 말이 들어왔는지}

                if (currentGrade > length)
                {
                    isGameFinished = true;
                    Console.WriteLine("경주 끝!");
                }
            }

            Console.WriteLine("==========결과발표==========");
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine($"{i+1}등 {arr_finishedHorseName[i]}");
            }
            Console.WriteLine("종료 하려면 엔터를 누르세요");
            Console.ReadLine();
        }
    }
}
