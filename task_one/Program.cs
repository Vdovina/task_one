using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_one
{
    class Program
    {
        static Random rand = new Random();
        static void Main(string[] args)
        {
            Point a = new Point();
            Point b = new Point();
            Point c = new Point();
            Point d = new Point();

            #region menu
            //Console.WriteLine(@"Выберите способ задания координат для точек треугольника:
            //                                                    1 - с клавиатуры
            //                                                    2 - рандомно");
            //int option = EnterANumber();
            //switch(option)
            //{
            //    case 1:
            //        Console.ForegroundColor = ConsoleColor.Yellow;
            //        Console.WriteLine("Координаты точки А");
            //        Console.ResetColor();
            //        a = EnterCoordinates();
            //        Console.ForegroundColor = ConsoleColor.Yellow;
            //        Console.WriteLine("Координаты точки B");
            //        Console.ResetColor();
            //        b = EnterCoordinates();
            //        Console.ForegroundColor = ConsoleColor.Yellow;
            //        Console.WriteLine("Координаты точки C");
            //        Console.ResetColor();
            //        c = EnterCoordinates();
            //        Console.Clear();
            //        break;
            //    case 2:
            //        a = new Point(rand.Next(-100, 100), rand.Next(-100, 100));
            //        b = new Point(rand.Next(-100, 100), rand.Next(-100, 100));
            //        c = new Point(rand.Next(-100, 100), rand.Next(-100, 100));
            //        break;
            //}
            #endregion

            string line1 = "";
            string line2 = "";
            string line3 = "";
            string line4 = "";
            StreamReader read = new StreamReader("INPUT.txt");
            try
            {
                while (!read.EndOfStream)
                {
                    line1 = read.ReadLine();
                    line2 = read.ReadLine();
                    line3 = read.ReadLine();
                    line4 = read.ReadLine();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Файл не был создан!");
            }
            read.Close();

            a = Separate(line1);
            b = Separate(line2);
            c = Separate(line3);
            d = Separate(line4);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nВаш треугольник с вершинами A({a.X}, {a.Y}), B({b.X}, {b.Y}), C({c.X}, {c.Y}).");
            

            //Console.WriteLine("\nВведите координаты точки D:");
            //d = EnterCoordinates();

            Console.WriteLine($"Точка D({d.X}, {d.Y}).");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            bool ok = OnTheSameSide(a, b, c, d) && OnTheSameSide(b, c, a, d) && OnTheSameSide(c, a, b, d);
            StreamWriter write = new StreamWriter("OUTPUT.txt");
            if (ok) write.WriteLine("In"); else write.WriteLine("Out");
            write.Close();
            Console.WriteLine(ok ? "\nТочка принадлежит треугольнику АВС." : "\nТочка не принадлежит треугольнику АВС.");
            Console.ResetColor();
            Console.ReadLine();
        }

        static double DotPosition(Point a, Point b, Point d) // вычисление положения точки D относительно прямой AB, проходящей через точки A и B
        {
            return (d.X - a.X) * (b.Y - a.Y) - (d.Y - a.Y) * (b.X - a.X); // из канонического уравнения прямой с подстановкой координат точки D
        }

        static bool OnTheSameSide(Point a, Point b, Point c, Point d) // проверка, лежат ли точки С и D по одну сторону от пряой АВ
        {
            return DotPosition(a, b, c) * DotPosition(a, b, d) >= 0;
        }

        static Point EnterCoordinates() // ввод координат для тоочки
        {
            Console.Write("Введите х:   ");
            int x = EnterANumber();
            Console.Write("Введите у:   ");
            int y = EnterANumber();
            return new Point(x, y);
        }


        public static Point Separate(string l)
        {
            string[] meanings = l.Split(' ');
            return new Point(Convert.ToInt32(meanings[0]), Convert.ToInt32(meanings[1]));
        }

        public static int EnterANumber() //ввод a number
        {
            bool ok = false;
            int number = 0;
            do
            {
                try
                {
                    number = Convert.ToInt32(Console.ReadLine());
                    ok = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введите целое число!");
                }
            } while (!ok);
            return number;
        }

        public static int MenuOption()
        {
            int option = 0;
            bool alright = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Пункт:    ");
                Console.ResetColor();
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                    if (option < 1 || option > 7) Console.WriteLine("Error!");
                    else alright = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error!");
                    alright = false;
                }
            } while (!alright);

            return option;
        }
    }

    public class Point
    {
        private int x;
        private int y;

        public Point()
        {
            X = 0;
            Y = 0;
        }

        public Point(int a, int b)
        {
            X = a;
            Y = b;
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }
    }
}
