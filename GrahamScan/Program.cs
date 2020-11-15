using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrahamScan
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Point> pointsList = new List<Point>();
            
            //Create list for filenames
            List<string> filenames = new List<string>();
            filenames.Add("case1.txt");
            filenames.Add("case2.txt");
            filenames.Add("case3.txt");
            filenames.Add("case4.txt");
            filenames.Add("case5.txt");
            filenames.Add("case6.txt");

            bool stop = true;

            try
            {
                do
                {
                    //Get filename from user
                    Console.Write("Please enter case to test: ");
                    string filename = Console.ReadLine();
                    Console.WriteLine($"-----Starting {filename}-----\n");

                    string input;
                    string[] fileText;
                    char[] delimiters = {' ', '\n'};

                    //Read from file corresponding to user input
                    using (StreamReader reader = new StreamReader(filename))
                    {
                        while ((input = reader.ReadLine()) != null)
                        {
                            fileText = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                            //Create new point
                            Point point = new Point(float.Parse(fileText[0]), float.Parse(fileText[1]));

                            pointsList.Add(point);
                        }
                    }

                    ////Print - Debugging only - TODO: REMOVE
                    //Console.WriteLine("Input Vertices\n------------\n");
                    //foreach (var p in pointsList)
                    //{
                    //    Console.WriteLine($"{p.GetX()}, {p.GetY()}\n");
                    //}

                    ConvexHull ch = new ConvexHull(pointsList);

                    //Get convex hull of set of points
                    ch.GetConvexHull();

                    //Ask user to enter another case
                    Console.WriteLine("Check another case: y/n: ");
                    string response = Console.ReadLine();

                    if (response == "y" || response == "Y")
                    {
                        stop = false;
                    }
                    else
                    {
                        stop = true;
                    }

                    //Clear points list
                    pointsList.Clear();

                } while (stop == false);


            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Stack Trace: {e.StackTrace}");
            }
        }
    }
}
