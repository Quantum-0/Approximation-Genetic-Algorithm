using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenAlgTest
{
    class Program
    {
        //static PointPopulation population = null;

        // 1.9x^2 + 0.4x + 4sin(2.3x)
        // 1.4x^2 + 2.2x + 2.5sin(2.8x)
        // 2x^2 - 1x - 5sin(-2x)

        // 2x^2 - x + 5sin(2x)
        /*static Point[] points = new Point[5]
        {
                new Point() { x = -0.8634, y = -2.5847 },
                new Point() { x = 1.4037, y = 4.1770 },
                new Point() { x = 0.3185, y = 2.8579 },
                new Point() { x = 2.5410, y = 5.7101 },
                new Point() { x = -1.4804, y = 4.9645 },
        };

        static Point[] anotherpoints = new Point[5]
        {
                new Point() { x = 0, y = 1 },
                new Point() { x = -1, y = 0 },
                new Point() { x = 1, y = 2 },
                new Point() { x = 2, y = 3 },
                new Point() { x = -2, y = -1 },
        };

        */
        static void Main(string[] args)
        { }/*
            var population = new FuncAproxPopulationFixed(points);
            var iter = 0;
            var lastfitness = double.MinValue;
            while(true)
            {
                var curfitness = population.Fitness(population.First());
                if (lastfitness == curfitness)
                {
                    if (iter == 0)
                        Console.WriteLine("Без изменений");
                    else
                    {
                        Console.SetCursorPosition(0, Console.CursorTop-1);
                        Console.WriteLine("Без изменений #" + iter);
                    }
                    iter++;
                }
                else
                {
                    Console.WriteLine(population.First() + "; Fitness = " + curfitness);
                    lastfitness = curfitness;
                    iter = 0;
                }

                if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        population.NextGeneration();
                        if (population.Fitness(population.First()) != curfitness)
                            break;
                    }
                }
                else
                    population.NextGeneration();
            }
        }

        /*static void Main(string[] args)
        {
            int n = 2;
            population = new PointPopulation(n);
            Help();
            Console.WriteLine($"Создана популяция размером {n}");

            while(true)
            {
                var key = Console.ReadKey(true);
                switch(key.Key)
                {
                    case ConsoleKey.A:
                        Console.WriteLine("Возраст популяции: " + population.age);
                        break;
                    case ConsoleKey.G:
                        NextGeneration();
                        break;
                    case ConsoleKey.N:
                        NextGenerationPrint();
                        break;
                    case ConsoleKey.P:
                        Print();
                        break;
                    case ConsoleKey.T:
                        Target();
                        break;
                    case ConsoleKey.C:
                        Console.Clear();
                        break;
                    case ConsoleKey.H:
                    case ConsoleKey.F1:
                        Help();
                        break;
                    default:
                        Console.WriteLine("Ошибка. Неверная клавиша. Нажмите F1 / H для справки");
                        break;
                }
            }
        }



        private static void Target()
        {
            for (int i = 0; i < population.points.Length; i++)
            {
                var p = population.points[i];
                Console.WriteLine($"Цель {i}: ({p.x}, {p.y})");
            }
            
        }

        private static void NextGenerationPrint()
        {
            population._debug = true;
            population.NextGeneration();
            population._debug = false;
        }

        private static void Print()
        {
            Console.WriteLine("Популяция:");
            foreach (var chr in population)
            {
                Console.WriteLine("- " + chr);
            }
        }

        private static void NextGeneration()
        {
            Console.WriteLine("Популяция перешла в следующее поколение");
            population.NextGeneration();
        }

        private static void Help()
        {
            Console.WriteLine($"G(eneration) - следующее поколение");
            Console.WriteLine($"N(ext generation) - следующее поколение с выводом всех созданных особей");
            Console.WriteLine($"T(arget) - цель");
            Console.WriteLine($"C(lear) - цель");
            Console.WriteLine($"P(rint) - вывод текущего поколения");
            Console.WriteLine($"F1 / H(elp) - справка");
            Console.WriteLine($"A(ge) - возраст");
        }*/
    }
}
