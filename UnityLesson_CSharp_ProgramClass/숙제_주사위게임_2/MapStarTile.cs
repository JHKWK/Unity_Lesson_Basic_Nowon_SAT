using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 숙제_주사위게임_2
{
    internal class MapStarTile : MapNormalTile
    {
        public int returnStars;
        public override int ReturnStars()
        {
            return returnStars; 
        }
    }
}
