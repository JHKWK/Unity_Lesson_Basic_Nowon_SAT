using System;
using System.Collections.Generic;

namespace DiceGame
{
    internal class Program
    {
        //초기 데이터가 필요한 변수들 static
        //연산에 필요한 함수는 Main 안에..
        static private int totalTile = 20;
        static private int totalDiceNumber = 20;
        
        static private Random random;


        static void Main(string[] args)
        {
            int currentTileIndex = 0;
            int currentStarPoint = 0;
            int previouisTileIndex = 0;

            TileMap map = new TileMap();
            map.MapSetup(totalTile);
            int currentDiceNumber = totalDiceNumber;

            //주사위 게임 시작
            while (currentDiceNumber > 0)
            {
                int diceValue = RollADice(); //주사위 굴림
                currentDiceNumber--; // 남은주사위갯수 차감
                currentTileIndex += diceValue; // 플레이어 주사위 눈금만큼 전진

                //현재칸이 최대칸을 넘어 가벼렸을 때
                if (currentTileIndex > totalTile)
                {
                    currentTileIndex -= totalTile;
                }
                
                //현재칸의 정보 받아옴
                TileInfo info = map.mapInfo.GetValueOrDefault(currentTileIndex);
                if (info == null)
                {
                    // 타일 정보 받아오기 실패  -> null값을 반환하며 함수 종료
                    Console.WriteLine($"Failed to get Tileinfo. num : {currentTileIndex}");
                    return;
                }                
                info.TileEvent(); //타일 이벤트 발생

                Console.WriteLine($"현재 플레이어 위치{currentTileIndex}");

                //플레이어가 샛별칸을 지났는지 체크
                if ( currentTileIndex/5 > previouisTileIndex/5 )
                {
                    int passedStarTileIndex = currentTileIndex/5*5;
                    TileInfo_Star tileInfo_Star =(TileInfo_Star)map.mapInfo.GetValueOrDefault(passedStarTileIndex); // 앞괄호는 캐스팅
                    if (tileInfo_Star != null)
                    {
                        currentStarPoint += tileInfo_Star.starValue;
                    }
                }

                previouisTileIndex = currentTileIndex;
                Console.WriteLine($"현재 샛별 점수 : {currentStarPoint}");
                Console.WriteLine($"남은 주사위 갯수는 {currentDiceNumber}");
            }
            Console.WriteLine($"게임이 끝났습니다");



            
        }
        static int RollADice()
        {
            string userInput = "Default";
            while (userInput != "")
            {
                userInput = Console.ReadLine();
                Console.WriteLine("Roll A DIce!! Press Enter");
            }

            random = new Random();
            int diceValue = random.Next(1, 7);
            Console.WriteLine($"DiceValue = {diceValue}");
            DisplayDice(diceValue);

            return diceValue;
        }
        static void DisplayDice(int diceValue)
        {
            switch (diceValue)
            {
                case 1:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│     ●    │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("└───────────┘");
                    break;
                case 2:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│ ●        │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│         ●│");
                    Console.WriteLine("└───────────┘");
                    break;
                case 3:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│ ●        │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│     ●    │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│         ●│");
                    Console.WriteLine("└───────────┘");
                    break;
                case 4:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("└───────────┘");
                    break;
                case 5:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│     ●    │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("└───────────┘");
                    break;
                case 6:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("└───────────┘");
                    break; ;
                default:
                    break;
            }
        }

    }
}
