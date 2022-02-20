using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 숙제_주사위게임
{
    internal class Player
    {
        public int playerPosition = 0;
        public int starsPlayerHas = 0;
        public int playerMoveTotal = 0;        

        public void WhereIsPlayer(int mapsize)
        {
            if (playerMoveTotal <= mapsize)
            {
                playerPosition = playerMoveTotal;
            }
            else
            {
                if (playerMoveTotal % mapsize == 0)
                {
                    playerPosition = mapsize;
                }
                else
                {
                    playerPosition = playerMoveTotal % mapsize;
                }
            }
        }
    }
}
