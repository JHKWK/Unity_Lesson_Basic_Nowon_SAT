using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 숙제_주사위게임_2
{
    internal class MapNormalTile
    {
        public int index;
        public string name;
        public string icon;
        public string discription;

        virtual public int ReturnStars()
        {
            return 0;
        }

        public void TileInfo()
        {
            Console.WriteLine($"타일 번호 : {index}, 플레이어는 {name}타일에 있습니다. {discription}");            
        }
        
    }
}
