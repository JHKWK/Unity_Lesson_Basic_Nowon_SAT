using System;
using System.Collections.Generic;

namespace UnityLesson_CSharp__ClassInharitance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Creature creature = new Creature();
            for (int i = 0; i < 10; i++)
            {
                creature.Breath();
            }
            Console.WriteLine(creature.age);

            Human human = new Human();
            for (int i = 0; i < 10; i++)
            {
                human.Breath();
            }
            Console.WriteLine($"인간 age:{human.age}, height:{human.height}, weight:{human.weight}");

            WhiteMan humanWhite = new WhiteMan();
            for (int i = 0; i < 10; i++)
            {
                humanWhite.Breath();
            }
            Console.WriteLine($"백인 age:{humanWhite.age}, Height:{humanWhite.height}, Weight:{humanWhite.weight}");

            BlackMan humanBlack = new BlackMan();
            for (int i = 0; i < 10; i++)
            {
                humanBlack.Breath();
            }
            Console.WriteLine($"흑인 Age:{humanBlack.age}, Height:{humanBlack.height}, Weight:{humanBlack.weight}");

            YellowMan humanYellow = new YellowMan();
            for (int i = 0; i < 10; i++)
            {
                humanYellow.Breath();
            }
            Console.WriteLine($"황인 Age:{humanYellow.age}, Height:{humanYellow.height}, Weight:{humanYellow.weight}");

            // 어레이형---------------------------------------------------------------------------------------------------------
            Console.WriteLine("어레이형");
            WhiteMan[] array_WhiteMan = new WhiteMan[20];
            {
                for (int i = 0; i < 20; i++)
                {
                    array_WhiteMan[i] = new WhiteMan();
                    Console.WriteLine($"백인{i + 1}");
                    array_WhiteMan[i].TwoLeggedWalk();                    
                }
            }
            BlackMan[] array_BlackMan = new BlackMan[20];
            {
                for (int i = 0; i < 20; i++)
                {
                    array_BlackMan[i] = new BlackMan();
                    Console.WriteLine($"흑인{i + 1}");
                    array_BlackMan[i].TwoLeggedWalk();
                }
            }
            YellowMan[] array_YellowMan = new YellowMan[20];
            {
                for (int i = 0; i < 20; i++)
                {
                    array_YellowMan[i] = new YellowMan();
                    Console.WriteLine($"황인{i + 1}");
                    array_YellowMan[i].TwoLeggedWalk();
                }
            }
            //리스트형--------------------------------------------------------------------------------------------------------------
            Console.WriteLine("case 1");
            //case1. 각 인종에 대한 리스트 별개로 생성하기
            List<WhiteMan> whiteMen = new List<WhiteMan>();
            List<BlackMan> blackMen = new List<BlackMan>();
            List<YellowMan> yellowMen = new List<YellowMan>();

            for (int i = 0; i < 20; i++)
            {
                whiteMen.Add(new WhiteMan());
                blackMen.Add(new BlackMan());
                yellowMen.Add(new YellowMan());
            }
            foreach (var item in whiteMen)
            {
                item.TwoLeggedWalk();
            }
            foreach (var item in blackMen)
            {
                item.TwoLeggedWalk();
            }
            foreach (var item in yellowMen)
            {
                item.TwoLeggedWalk();
            }


            //----------------------------------------------------------------------------------------------
            // Breth();는 Human의 Breath()인가 WhiteMan의 Breath()인가
            // WhiteMan 객체화 -> Human으로 인스턴스화
            // Human에 있는 Breath를 호출하면 WhiteMan에 있는 Breath가 호출된다
            // 자식 객체를 부모 클래스타입으로 인스턴스화 시키고 해당 변수의 virtual멤버함수를 호출 하면
            // 자식 객체의 override된 함수가 호출 된다.
            Human testHuman = new WhiteMan();
            testHuman.Breath(); 
            Console.WriteLine($"tesHuman height : {testHuman.height}, weight {testHuman.weight}");

            //----------------------------------------------------------------------------------------------
            Console.WriteLine("case 2");
            // case2 위의 성질을 이용하여 부모클래스 리스트 하나만 생성
            List<Human> humen = new List<Human>();
            for (int i = 0; i < 20; i++)
            {
                Human tmpHumen1 = new WhiteMan();
                humen.Add(tmpHumen1);
                Human tmpHumen2 = new BlackMan();
                humen.Add(tmpHumen2);
                Human tmpHumen3 = new YellowMan();
                humen.Add(tmpHumen3);
            }
            foreach (var item in humen)
            {
                item.TwoLeggedWalk();
            }

            //------------------------------------------------------------------------------------------------
            // 인터페이스 인스턴스화 예시
            Console.WriteLine("인터페이스 인스턴스화 예시");

            ITwoLeggedWalker iTwoLeggedWalker = new WhiteMan();
            iTwoLeggedWalker.TwoLeggedWalk();


            Console.WriteLine("case 3");
            // case3 인터페이스로 인스턴스화 시키는 방법
            // ITwoLeggedWalker라는 인터페이스를 walkers로 인스턴스화
            // = TwoleggedWalk를 수행할 대상들을 List형태로 관리
            List<ITwoLeggedWalker> walkers = new List<ITwoLeggedWalker>();
            for (int i = 0; i < 20; i++)
            {
                ITwoLeggedWalker tmpWalker1 = new WhiteMan();
                walkers.Add(tmpWalker1);
                ITwoLeggedWalker tmpWalker2 = new BlackMan();
                walkers.Add(tmpWalker2);
                ITwoLeggedWalker tmpWalker3= new YellowMan();
                walkers.Add(tmpWalker3);
            }
            //기존에 생성했던 리스트를 인터페이스 인스턴스에 추가
            foreach (var item in humen)
            {
                walkers.Add(item);
            }

            // List 멤버버들이 걷는다
            foreach (var item in walkers)
            {
                item.TwoLeggedWalk();
            }
                
        }





    }
}
