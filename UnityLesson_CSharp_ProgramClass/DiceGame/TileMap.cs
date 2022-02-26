using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    internal class TileMap
    {
        public Dictionary<int, TileInfo> mapInfo = new Dictionary<int, TileInfo>();
        public void MapSetup(int maxTileNum)
        {
            for (int i = 1; i <= maxTileNum; i++)
            {
                if ( i % 5 == 0 )
                {
                    TileInfo tileinfo_star = new TileInfo_Star();
                    tileinfo_star.index = i;
                    tileinfo_star.name = "Star";
                    tileinfo_star.discription = "This is Star tile";
                    mapInfo.Add(i, tileinfo_star);
                }
                else
                {
                    TileInfo tileinfo = new TileInfo();
                    tileinfo.index = i;
                    tileinfo.name = "Dummy";
                    tileinfo.discription = "This is dummy tile";
                    mapInfo.Add(i, tileinfo);
                }
            }
            Console.WriteLine($"Maps setup Complete. Max Tile num : {maxTileNum}");
        }
    }
}
