using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 숙제_주사위게임
{
    public class Dice
    {
        public int diceValue;        
        public int RollADice()
        {
            string userInput = "Default";
            while (userInput != "")
            {
                Console.WriteLine("'Enter'를 눌러 주사위를 굴려주세요!");
                userInput = Console.ReadLine();
            }

            Random random = new Random(); 
            int diceValue2 = random.Next(1, 6 + 1); // 1~6 중 랜덤한 정수
            Console.WriteLine($"나온숫자 : {diceValue2}");

            DisplayDice(diceValue2);
            diceValue = diceValue2;            
            return diceValue2;
        }
            private void DisplayDice(int diceValue)
        {
            {
                switch (diceValue)
                {
                    case 1:
                        Console.WriteLine("┌───────────┐");
                        Console.WriteLine("│           │");
                        Console.WriteLine("│           │");
                        Console.WriteLine("│     ●    │");
                        Console.WriteLine("│           │");
                        Console.WriteLine("│           │");
                        Console.WriteLine("└───────────┘");
                        break;
                    case 2:
                        Console.WriteLine("┌───────────┐");
                        Console.WriteLine("│ ●        │");
                        Console.WriteLine("│           │");
                        Console.WriteLine("│           │");
                        Console.WriteLine("│           │");
                        Console.WriteLine("│         ●│");
                        Console.WriteLine("└───────────┘");
                        break;
                    case 3:
                        Console.WriteLine("┌───────────┐");
                        Console.WriteLine("│ ●        │");
                        Console.WriteLine("│           │");
                        Console.WriteLine("│     ●    │");
                        Console.WriteLine("│           │");
                        Console.WriteLine("│         ●│");
                        Console.WriteLine("└───────────┘");
                        break;
                    case 4:
                        Console.WriteLine("┌───────────┐");
                        Console.WriteLine("│ ●      ●│");
                        Console.WriteLine("│           │");
                        Console.WriteLine("│           │");
                        Console.WriteLine("│           │");
                        Console.WriteLine("│ ●      ●│");
                        Console.WriteLine("└───────────┘");
                        break;
                    case 5:
                        Console.WriteLine("┌───────────┐");
                        Console.WriteLine("│ ●      ●│");
                        Console.WriteLine("│           │");
                        Console.WriteLine("│     ●    │");
                        Console.WriteLine("│           │");
                        Console.WriteLine("│ ●      ●│");
                        Console.WriteLine("└───────────┘");
                        break;
                    case 6:
                        Console.WriteLine("┌───────────┐");
                        Console.WriteLine("│ ●      ●│");
                        Console.WriteLine("│           │");
                        Console.WriteLine("│ ●      ●│");
                        Console.WriteLine("│           │");
                        Console.WriteLine("│ ●      ●│");
                        Console.WriteLine("└───────────┘");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
