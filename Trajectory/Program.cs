using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trajectory
{
    class Program
    {


        void showInfo()
        {
            Console.Write("-Enemy Location");
            Console.Write("-Power:");
            Console.Write("-Angle:");
        }

        static void Main(string[] args)
        {

            Console.Title = "Trajectory";

            NewGame:
            double gravity = 9.8;
            double power = 0;
            double angle = 0;
            double missile_impact_range = 10;
            int[] Level = { 80, 120, 150 };
            string[] Rank = { "Private", "Sergeant", "Lieutenant" };
            int diffculty;
            int totalTries = 2;


            Console.WriteLine("Please Select difficulty.\n -1. Easy.\n -2. Medium.\n -3. Hard.");

            while (!int.TryParse(Console.ReadLine(), out diffculty) || diffculty < 1 || diffculty > 3)
            {
                Console.Clear();
                Console.WriteLine("Please Select difficulty.\n -1. Easy.\n -2. Medium.\n -3. Hard.");
            }

            Console.WriteLine("Rank: " + Rank[diffculty - 1]);

            for (int i = 0; i < 3; i++)
            {
                System.Threading.Thread.Sleep(500);
                Console.Write(".");
            }
            System.Threading.Thread.Sleep(2000);

            //declare enemy position
            Random tr = new Random();
            double range = tr.NextDouble() * 40 * (diffculty * 1.025);
            double enemy_Xloc = Level[diffculty - 1] + range;



            replay:

            Console.Clear();
            Console.ResetColor();


            void info()
            {
                Console.Clear();
                Console.WriteLine("The enemy is {0:0.00} away.\n", enemy_Xloc);
                Console.WriteLine("-Power: {0} \n-Angle is: {1}\n", power, angle);
            }


            //Request for power
            info();
            Console.WriteLine("Please enter the amount of power:");
            while (!double.TryParse(Console.ReadLine(), out power))
            {
                Console.Clear();
                Console.WriteLine("Please enter a number:\n\t");
            }



            Console.Clear();
            //Request for angle
            info();
            Console.WriteLine("Please enter launching angle:");
            while (!double.TryParse(Console.ReadLine(), out angle))
            {
                Console.WriteLine("Please enter a number:\n\t");
            }



            totalTries--;
            //Calculating Initial Velocity
            info();
            double y_initVel = power * Math.Sin(Math.PI * angle / 180.0);           //initial velocity on verical axis 
            double x_initVel = power * Math.Cos(Math.PI * angle / 180.0);           //initial velocity on horrizontal axis
            Console.Write("Initial Velocity on Y-axis is: {0:0.00} m/s\n", y_initVel);
            Console.Write("Initial Velocity on X-axis is: {0:0.00} m/s\n", x_initVel);


            //Calulating Time
            double t = (y_initVel / gravity) * 2;
            double y_dist = y_initVel * t / 2;                                      //Maximum height
            double x_dist = x_initVel * t;                                          //Maximum distance


            //wait for missile impact
            for (int i = 1; i <= t; i++)
            {
                System.Threading.Thread.Sleep(1000);
                //Console.Write("\t{0} Second...\n", i);
            }


            System.Threading.Thread.Sleep(1000);

            Console.Clear();
            Console.Write("******Launching*****");
            Console.Beep(2000, 1000);
            System.Threading.Thread.Sleep((int)t * 1000);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\n\tBoom!!\n");
            Console.ResetColor();

            for (int i = 130; i >= 42; i = i - 2)
            {
                Console.Beep(i, 60);

                if (i % 2 == 1)
                {
                    Console.Write(".");
                }

            }


            System.Threading.Thread.Sleep(2500);

            Console.Clear();
            Console.Write("\nTime Traveled:\t\t {0:0.00} seconds\n", t);
            Console.Write("Maximum height:\t\t {0:0.00} m\nFlying Distance:\t {1:0.00} m\n", y_dist, x_dist);



            //Calculate distance from the target

            double x_dif = Math.Abs(enemy_Xloc - x_dist);
            //double y_dif = Math.Abs(enemy_Yloc - y_dist);


            Console.WriteLine("\n\tThe missile dropped {0:0.00}m away from the target.", x_dif);


            if (x_dif <= missile_impact_range)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\tCongratulation! You have eliminated the enemy!");

            }
            else if (totalTries == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\t\tMission Failed.");

                goto check_cont;
            }
            else
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tTarget Missed.");
                Console.ResetColor();
                Console.WriteLine("\tYou have {0} times left. Let's try it again.\n\n\t\t\t\t\tPlease press any key...", totalTries);
                Console.ReadKey();

                goto replay;
            }
            Console.ResetColor();





            //Checking if Continue

            check_cont:
            Console.ResetColor();
            Console.WriteLine("\nEnter 1 to exit and 2 to continue.");
            int caseExit = 1;
            while (!int.TryParse(Console.ReadLine(), out caseExit))
            {
                Console.Clear();
                Console.WriteLine("Please enter 1 for exit and 2 to continue:\n\t");
            }

            Console.Clear();
            switch (caseExit)
            {
                case 1:
                    Console.WriteLine("Good day.");
                    System.Threading.Thread.Sleep(2000);
                    break;
                case 2:
                    Console.WriteLine("Lets get you back on duty.");
                    System.Threading.Thread.Sleep(2000);
                    goto NewGame;

                default:
                    Console.WriteLine("Please enter 1 for exit and 2 to continue:\n\t");
                    goto check_cont;
            }

            //Console.ReadKey();
        }
    }
}
