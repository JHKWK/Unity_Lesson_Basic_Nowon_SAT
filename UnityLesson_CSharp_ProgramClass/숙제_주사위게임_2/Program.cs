using System;

namespace 숙제_주사위게임_2
{
    internal class Program
    {
        static int diceRoll = 20;
        static int mapSize = 20;
        static int mapSegment = 5;
        static int starsInStarTile = 3;
        static void Main(string[] args)
        {
            int CountDiceRoll;
            int starsPlayerHad;
            //인스턴스 생성
            Player player = new Player();
            Map map = new Map();
            Dice dice = new Dice();
            map.MapSetup(mapSize, mapSegment, starsInStarTile);//맵-타일 생성
            CountDiceRoll = diceRoll;
            for (int i = 0; i < diceRoll; i++) //전체게임 진행 : diceRoll회 반복
            {
                starsPlayerHad = player.starsPlayerHas; // 매턴마다 얻은 별의 갯수
                dice.RollADice(); // 주사위 굴림
                {
                    for (int j = 0; j < dice.diceValue; j ++) //주사위 눈금만큼 반복
                    {
                        player.playerMoveTotal++; // 플레이어 1칸씩 전진
                        player.WhereIsPlayer(mapSize); // 플레이어현재위치 업데이트 = 타일주소
                        player.starsPlayerHas += map.dic_tile[player.playerPosition].ReturnStars(); // 타일을 지날때 마다 별 획득
                    }
                }
                CountDiceRoll--;
                map.ShowMap(player.playerPosition);
                map.dic_tile[player.playerPosition].TileInfo(); //타일 정보, 별 갯수 텍스트 출력
                if (player.starsPlayerHas - starsPlayerHad == 0) // 증가한 별의 갯수 0 
                {
                    Console.WriteLine("Star를 얻지 못했습니다");                
                }
                else
                {
                    Console.WriteLine($"당신은 {player.starsPlayerHas - starsPlayerHad}개의 Star를 얻었습니다");                 
                }
                Console.WriteLine($"현재까지 당신이 모은 Star는 {player.starsPlayerHas}개 입니다");
                Console.WriteLine($"남은턴 수 : {CountDiceRoll}번");
                Console.WriteLine();
            }            
            Console.WriteLine("게임이 종료 되었습니다");
            Console.WriteLine($"플레이어가 모은 Star : {player.starsPlayerHas}");
            Console.WriteLine($"플레이어가 이동한 거리 : {player.playerMoveTotal}");            
        }
    }
}