using System;
using System.Collections.Generic;
namespace 숙제_주사위게임
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int mapSize = 20;
            int dice = 20;
            int starValueInEachStarTile = 1;            

            Player player1 = new Player();
            Dice dice1 = new Dice();
            TileMap map1 = new TileMap();                                    

            map1.MapSetup(mapSize, starValueInEachStarTile);

            for (int i = 0; i < dice; i++)
            {                
                dice1.RollADice();
                player1.playerMoveTotal += dice1.diceValue;                
                player1.WhereIsPlayer(mapSize);

                //샛별칸을1칸 지나고 다시 도착한곳이 샛별칸 별2회 획득
                if (player1.playerMoveTotal - dice1.diceValue % 5 == 4 & dice1.diceValue == 6) 
                {
                    map1.dic_tile[player1.playerPosition].TileEvent();
                    player1.starsPlayerHas += starValueInEachStarTile * 2;
                    Console.WriteLine($"플레이어가 샛별타일을 통과 했습니다. 샛별의 갯수가 {starValueInEachStarTile}개 증가 합니다");
                    Console.WriteLine($"플레이어가 가진 샛별의 갯수는 총{player1.starsPlayerHas}개 입니다.");
                }
                //샛별칸을 1칸 지나온경우
                else if (DidPlayerPassStarTile(player1.playerMoveTotal, dice1.diceValue)) 
                {
                    map1.dic_tile[player1.playerPosition].TileEvent();
                    Console.WriteLine($"플레이어가 샛별 타일을 통과 했습니다. 샛별의 갯수가 {starValueInEachStarTile}개 증가합니다.");
                    player1.starsPlayerHas += starValueInEachStarTile;
                    Console.WriteLine($"플레이어가 가진 샛별의 갯수는 총{player1.starsPlayerHas}개 입니다.");
                }
                else //보통칸, 샛별칸
                {                    
                    int tmpStarIncrease = map1.dic_tile[player1.playerPosition].TileEvent();
                    player1.starsPlayerHas += tmpStarIncrease;

                    if (tmpStarIncrease == 0) //보통칸 샛별증가 없음 샛별갯수 출력 X
                    { }
                    else //샛별칸 샛별개수 출력
                        Console.WriteLine($"플레이어가 가진 샛별의 갯수는 총{player1.starsPlayerHas}개 입니다.");
                }
                
                Console.WriteLine($"남은 횟수 {dice-i-1}번");
                Console.WriteLine("");

            }
            Console.WriteLine("게임이 끝났습니다");
            Console.WriteLine($"플레이어가 획득한 샛별의 갯수는 총{player1.starsPlayerHas}개 입니다.");
        }
        static bool DidPlayerPassStarTile(int a, int b)//player1.playerMoveTotal, dice1.diceValue
        {            
            if (a % 5 == 0) //플레이어가 샛별칸에 있을 때 false 출력
                return false;                                       
            else if ((a-b)%5 + b >= 6) //플레이어의 이전위치 + 주사위 눈금의 합이 5이상
                return true;               
            else                             
                return false; 
        }
        
    }
}       
