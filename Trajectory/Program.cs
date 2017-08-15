using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trajectory
{
    class Player
    {
        public string p_Name;
        public int p_HP = 100;
        public int p_Point = 0;
        public int p_S_Rank = 0;
        public string[] p_Rank = { "Bronze", "Silver", "Gold", "Platinum", "Diamond", "Master" };



        public void p_StatsPrint()
        {
            p_S_Rank = p_Point / 1000;

            if (p_S_Rank > 5)
            {
                p_S_Rank = 5;
            }


            Console.WriteLine("Name: \t" + p_Name);
            Console.WriteLine("HP:\t" + p_HP);
            Console.WriteLine("Points:\t" + p_Point);
            Console.Write("\nRank:\t");


            if (p_Point <= 1000)                             //0-1000 Bronze
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else if (p_Point > 1000 && p_Point <= 2000)      // 1k - 2k Silver
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            else if (p_Point > 2000 && p_Point <= 3000)     // 2k - 3k Gold
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (p_Point > 3000 && p_Point <= 4000)     // 3k - 4k Platinum
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (p_Point > 4000 && p_Point <= 5000)     // 4k - 5k Diamond
            {
                //Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            else if (p_Point > 5000)                        // 5k + Master
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.Write(p_Rank[p_S_Rank]);
            Console.ResetColor();




            if (p_Point > 5000)
            {
                Console.WriteLine("\nYou are in the Elite now!");
            }
            else
            {
                Console.WriteLine("\nYou are " + (1000 - p_Point % 1000) + " points away from " + p_Rank[p_S_Rank + 1]);
            }

        }


    }
    class Program
    {


        void showInfo()
        {
            Console.Write("-Enemy Location");
            Console.Write("-Speed:");
            Console.Write("-Angle:");
        }

        static void Main(string[] args)
        {

            Console.Title = "Trajectory";


            



            NewGame:
            
            double gravity = 9.8;
            double speed = 0;
            double angle = 0;
            double missile_impact_range = 10;
            int[] Level = { 80, 120, 150 };
            string[] Rank = { "Bronze", "Silver", "Gold", "Platinum", "Diamond", "Master"};
            string[] diffculty = { "Easy", "Medium","Hard" };
            int val_diff;
            int totalTries = 10;



            Console.WriteLine("Please enter your name");

            String name_input = Console.ReadLine();
            Player name = new Player();

            name.p_Name = name_input;



            Console.Clear();
            Console.WriteLine("Welcome! " + name.p_Name);

            name.p_StatsPrint();

            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("\n\tPress Enter to continue...");
            Console.ReadLine();
            Console.Clear();



            Console.WriteLine("Select Game Difficulty.\n -1. Easy.\n -2. Medium.\n -3. Hard.");

            while (!int.TryParse(Console.ReadLine(), out val_diff) || val_diff < 1 || val_diff > 3)
            {
                Console.Clear();
                Console.WriteLine("Please Select difficulty.\n -1. Easy.\n -2. Medium.\n -3. Hard.");
            }



            Console.WriteLine("\n\t\tYou have selected: " + diffculty[val_diff-1]);


            for (int i = 0; i < 3; i++)
            {
                System.Threading.Thread.Sleep(500);
                Console.Write(".");
            }
            System.Threading.Thread.Sleep(2000);


            //Display Target Location
            Random tr = new Random();
            double range = tr.NextDouble() * 40 * (val_diff * 1.025);
            double enemy_Xloc = Level[val_diff - 1] + range;



            replay:

            Console.Clear();
            Console.ResetColor();


            void info()
            {
                Console.Clear();
                Console.WriteLine("The enemy is {0:0.00} away.\n", enemy_Xloc);
                Console.WriteLine("-Speed: {0} \n-Angle is: {1}\n", speed, angle);
            }


            //Request for speed
            info();
            Console.WriteLine("Please enter Launching Speed:");
            while (!double.TryParse(Console.ReadLine(), out speed))
            {
                Console.Clear();
                Console.WriteLine("Please enter a number:\n\t");
            }



            //Request for angle
            Console.Clear();
            info();
            Console.WriteLine("Please enter launching angle:");
            while (!double.TryParse(Console.ReadLine(), out angle))
            {
                Console.WriteLine("Please enter a number:\n\t");
            }
            

            totalTries--;

            //Calculating Initial Velocity
            info();
            double y_initVel = speed * Math.Sin(Math.PI * angle / 180.0);               //initial velocity on verical axis 
            double x_initVel = speed * Math.Cos(Math.PI * angle / 180.0);               //initial velocity on horrizontal axis
            Console.Write("Initial Velocity on Y-axis is: {0:0.00} m/s\n", y_initVel);
            Console.Write("Initial Velocity on X-axis is: {0:0.00} m/s\n", x_initVel);


            //Calulating Time
            double t = (y_initVel / gravity) * 2;
            double y_dist = y_initVel * t / 2;                                          //Maximum height
            double x_dist = x_initVel * t;                                              //Maximum distance


          


            System.Threading.Thread.Sleep(1000);

            Console.Clear();
            Console.Write("******Launching*****");
            Console.Beep(2000, 1000);
            System.Threading.Thread.Sleep(2000);
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
