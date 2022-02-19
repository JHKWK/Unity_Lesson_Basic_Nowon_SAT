using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityLesson_CSharp_Structure
{
    internal class Equipment
    {
        public EquipmentAblility equipmentAblilty;
        public Stats stats;

        public void SetEquipmentAblilty(int ATK, int DEF, int HP, int MP,int DUR)
        {
            equipmentAblilty._ATK = ATK;
            equipmentAblilty._DEF = DEF;
            equipmentAblilty._DUR = DUR;
            equipmentAblilty._HP = HP;
            equipmentAblilty._MP = MP;
        }
        public void SetStats(int STR, int DEX, int CON, int WIS, int INT, int REG)
        {
            stats._STR = STR;
            stats._DEX = DEX;
            stats._CON = CON;
            stats._WIS = WIS;
            stats._INT = INT;
            stats._REG = REG;
        }
    }
}
