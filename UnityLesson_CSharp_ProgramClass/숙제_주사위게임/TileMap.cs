using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 숙제_주사위게임
{
    internal class TileMap
    {           
        public Dictionary<int, TileInfo> dic_tile = new Dictionary<int, TileInfo>(); 
        public void MapSetup(int maxTileNum,int getStarValue)
        {
            for (int i = 1; i < maxTileNum+1; i++)
            {                
                if (i % 5 == 0)
                {
                    // 샛별칸 생성
                    TileInfo_Star tileInfo_Star = new TileInfo_Star();
                    tileInfo_Star.index = i;
                    tileInfo_Star.name = "샛별";
                    tileInfo_Star.discription = $"이 타일은 샛별타일 입니다.";
                    tileInfo_Star.increaseStarValue = getStarValue;
                    dic_tile.Add(i, tileInfo_Star);
                }
                else
                {
                    // 일반칸 생성
                    TileInfo tileInfo_Dummy = new TileInfo();
                    tileInfo_Dummy.index = i;
                    tileInfo_Dummy.name = "보통";
                    tileInfo_Dummy.discription = "이 타일은 보통 타일입니다.";
                    dic_tile.Add(i, tileInfo_Dummy);
                }
            }
            Console.WriteLine($"맵생성이 완료되었습니다. 맵의 크기는{maxTileNum}입니다.");
            Console.WriteLine($"샛별타일을 통과할 때 얻을 수 있는 샛별의 갯수는{getStarValue}개 입니다");
            Console.WriteLine("");
        }
    }
}
