using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 숙제_주사위게임_2
{
    internal class Map
    {
        public Dictionary<int, MapNormalTile> dic_tile = new Dictionary<int, MapNormalTile>();
        int mapsize;
        public void MapSetup(int maxTileNum, int segment, int getStarValue)
        {
            mapsize = maxTileNum;
            for (int i = 1; i < maxTileNum + 1; i++)
            {
                if (i % segment == 0)
                {                    
                    MapStarTile tileInfo_Star = new MapStarTile();
                    tileInfo_Star.index = i;
                    tileInfo_Star.name = "Star";
                    tileInfo_Star.icon = "▣";
                    tileInfo_Star.discription = "이 타일은 Star타일 입니다.";
                    tileInfo_Star.returnStars = getStarValue;
                    dic_tile.Add(i, tileInfo_Star);
                }
                else
                {                    
                    MapNormalTile tileInfo_Normal = new MapNormalTile();
                    tileInfo_Normal.index = i;
                    tileInfo_Normal.name = "Normal";
                    tileInfo_Normal.icon = "□";
                    tileInfo_Normal.discription = "이 타일은 Normal 타일입니다.";
                    dic_tile.Add(i, tileInfo_Normal);
                }
            }            
            Console.WriteLine($"맵생성이 완료되었습니다. 맵의 크기는{maxTileNum}입니다.");
            Console.WriteLine($"Star 타일은 간격은{segment}칸 입니다.");
            Console.WriteLine($"Star 타일을 통과할 때 얻을 수 있는 Star는{getStarValue}개 입니다");
            Console.WriteLine("");
        }

        public void MapSetup(int maxTileNum,string random,int getStarValue)
        {
            mapsize = maxTileNum;
            for (int i = 1; i < maxTileNum + 1; i++)
            {
                Random randomSegment = new Random();
                int i2 = randomSegment.Next(0,3);                

                if (i2 == 0)
                {
                    MapStarTile tileInfo_Star = new MapStarTile();
                    tileInfo_Star.index = i;
                    tileInfo_Star.name = "Star";
                    tileInfo_Star.icon = "▣";
                    tileInfo_Star.discription = "이 타일은 Star타일 입니다.";
                    tileInfo_Star.returnStars = getStarValue;
                    dic_tile.Add(i, tileInfo_Star);
                }
                else
                {
                    MapNormalTile tileInfo_Normal = new MapNormalTile();
                    tileInfo_Normal.index = i;
                    tileInfo_Normal.name = "Normal";
                    tileInfo_Normal.icon = "□";
                    tileInfo_Normal.discription = "이 타일은 Normal 타일입니다.";
                    dic_tile.Add(i, tileInfo_Normal);
                }
            }
            Console.WriteLine($"맵생성이 완료되었습니다. 맵의 크기는{maxTileNum}입니다.");
            Console.WriteLine($"Star 타일은 간격은 랜덤 입니다.");
            Console.WriteLine($"Star 타일을 통과할 때 얻을 수 있는 Star는{getStarValue}개 입니다");
            Console.WriteLine("");
        }


        public void ShowMap(int playerPosition)
        {
            for (int i = 1; i <= mapsize; i++)
            {
                if (i == playerPosition)
                    Console.Write($"▼");
                else  
                    Console.Write($"  ");
            }
            Console.WriteLine("");
            for (int i = 1; i <= mapsize; i++)
            {                
                Console.Write($"{ dic_tile[i].icon }");                
            }
            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}
