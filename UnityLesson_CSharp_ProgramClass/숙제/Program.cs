using System;
using System.Collections.Generic;
using System.Threading;


namespace 숙제_경마게임
{
    internal class Program
    {        
        static void Main(string[] args)
        {
            // 참여할 경주마의 수
            int howMuchHorses = 5;
            // 달릴 트랙 총 길이
            int trackDistance = 200;
            // 최소속도/최고속도
            int horseMinSpeed = 10;
            int horseMaxSpeed = 20;
            // 시간 배속
            float timeSpeed = 1f;
            // 게임 속도
            int gameSpeed = 1000;
            // 계산용 변수
            int horseNumer = 1;
            int raceRank = 0;            
            int rankIndex = 1;
            float currentTime = timeSpeed;

            //경주마 생성
            List<Horse> horses = new List<Horse>();
            for (int i = 0; i < howMuchHorses; i++)
            {
                horses.Add(new Horse());
            }

            //이름및 랜덤속도 할당
            foreach (var item in horses)
            {                                
                item.name = $"{horseNumer}번마";
                item.speed = GetRandomSpeed(horseMinSpeed,horseMaxSpeed);
                horseNumer++;
                Console.WriteLine($"{item.name} 준비 완료");
            }
            Thread.Sleep(gameSpeed);

            //레이스
            Console.WriteLine("-----출발-----");
            foreach (var item in horses)
            {
                Console.WriteLine($"{item.name} : {item.distance}m");
            }
            Thread.Sleep(gameSpeed);                        
            while (raceRank != howMuchHorses)
            {                
                Console.WriteLine($"-----{currentTime}초-----");
                foreach (Horse item in horses)
                {
                    item.Run(timeSpeed);
                    Console.WriteLine($"{item.name} : {item.distance}m");
                    if (item.distance >= trackDistance & item.rank == 0)
                    {
                        item.rank = raceRank+1;
                        raceRank++;
                    }
                }
                currentTime = currentTime + timeSpeed;
                Thread.Sleep(gameSpeed);
            }
            //경기결과 발표
            Console.WriteLine("----경기결과----");
            
            for (int i = 0; i < howMuchHorses; i++)
            {                
                foreach (var item in horses)
                {                    
                    if (rankIndex == item.rank)
                    {
                        Console.WriteLine($"{item.rank}등 : {item.name} ");
                        rankIndex++;
                    }
                }
            }
        }
        //말속도 랜덤
        static private int GetRandomSpeed(int a,int b)
        {
            Random randomInt = new Random();            
            return randomInt.Next(a, b+1);
        }
    }
}
