using System;
// structure(구조체) 
// 멤버 변수를 가지는 타입 ex) 캐릭터의 능력치 등
namespace UnityLesson_CSharp_Structure
{
    internal class Program
    {
        static void Main(string[] args)
        {    
            Warrior warrior = new Warrior();
            warrior.stats._STR = 10;
            warrior.stats._DEX = 10;
            warrior.stats._CON = 10;
            warrior.stats._WIS = 10;
            warrior.stats._INT = 10;
            warrior.stats._REG = 10;
            
            Ranger ranger = new Ranger();
            ranger.stats._STR = 10;
            ranger.stats._DEX = 10;
            ranger.stats._CON = 10;
            ranger.stats._WIS = 10;
            ranger.stats._INT = 10;
            ranger.stats._REG = 10;

            Knight knight = new Knight();
            knight.stats._STR = 10;
            knight.stats._DEX = 10;
            knight.stats._CON = 10;
            knight.stats._WIS = 10;
            knight.stats._INT = 10;
            knight.stats._REG = 10;


            // 멤버를 생성하며 스텟을 초기화 하는 방법
            Warrior[] arr_warrior1 = new Warrior[10];
            int length = arr_warrior1.Length;
            for (int i = 0; i < length; i++)
            {
                arr_warrior1[i] = new Warrior();
                arr_warrior1[i].stats._STR = 10;
                arr_warrior1[i].stats._DEX = 10;
                arr_warrior1[i].stats._CON = 10;
                arr_warrior1[i].stats._WIS = 10;
                arr_warrior1[i].stats._INT = 10;
                arr_warrior1[i].stats._REG = 10;
            }


            //초기화 값을 설정한 지역변수 stats 를 생성하여 멤버에 일괄 적용 하기---------------------------            
            //지역변수 stats을 초기화 한후 for문에서 멤버변수 stats를 초기화 하는 방법
            Stats tmpStats = new Stats();
            tmpStats._STR = 10;
            tmpStats._DEX = 20;
            tmpStats._CON = 30;
            tmpStats._WIS = 40;
            tmpStats._INT = 50;
            tmpStats._REG = 60;

            //멤버에 적용
            for (int i = 0; i < length; i++)
            {
                arr_warrior1[i].stats = tmpStats;               
            }

            
            //stats을 초기화 하는 함수 활용-------------------------------------------------------------------           
            for (int i = 0; i < length; i++)
            {
                arr_warrior1[i].SetStats(12, 20, 30, 40, 50, 60);
            }


            //예제

            Equipment equipment1 = new Equipment();
            {
                equipment1.equipmentAblilty._ATK = 10;
                equipment1.equipmentAblilty._DEF = 10;
                equipment1.equipmentAblilty._HP = 10;
                equipment1.equipmentAblilty._MP = 10;
                equipment1.equipmentAblilty._DUR = 10;
            }

            Equipment[] arr_equipment = new Equipment[10];
            int length2 = arr_equipment.Length;
            
            for (int i = 0; i < length2; i++)
            {
                arr_equipment[i] = new Equipment();
                arr_equipment[i].equipmentAblilty._ATK = 10;
                arr_equipment[i].equipmentAblilty._DEF = 10;
                arr_equipment[i].equipmentAblilty._HP = 10;
                arr_equipment[i].equipmentAblilty._MP = 10;
                arr_equipment[i].equipmentAblilty._DUR = 10;               

            }

        }        
    }
}
