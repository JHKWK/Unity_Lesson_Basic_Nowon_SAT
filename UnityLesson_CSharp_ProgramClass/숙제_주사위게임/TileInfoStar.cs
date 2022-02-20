using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 숙제_주사위게임
{    internal class TileInfo_Star : TileInfo
    {
        public int increaseStarValue;
        public override int TileEvent()
        {
            base.TileEvent();
            Console.WriteLine($"플레이어가 가진 샛별의 갯수가 {increaseStarValue}개 증가합니다.");
            return increaseStarValue;
        }
    }
}
