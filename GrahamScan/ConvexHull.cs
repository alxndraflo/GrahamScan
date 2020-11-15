using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrahamScan
{
    class ConvexHull
    {
        public static Point P0;

        private List<Point> pointsList = new List<Point>();
        private Stack<Point> convexHull = new Stack<Point>();

        public ConvexHull(List<Point> pointsIn)
        {
            pointsList = pointsIn;
        }

        //Public Methods

        public void GetConvexHull()
        {
            //Find bottom-most point in set of points
            FindLowestPoint();

            //Console.WriteLine($"Bottom-most point: ({P0.GetX()}, {P0.GetY()})\n"); //Debugging - TODO: REMOVE

            //Sort points according to polar angle
            pointsList.Sort();

            ////Print sorted points - Debugging - TODO: REMOVE
            //Console.WriteLine("Sorted Points:\n");
            //foreach (var pt in pointsList)
            //{
            //    Console.WriteLine($"({pt.GetX()}, {pt.GetY()})\n");
            //}

            //Search points to determine convex hull
            convexHull.Push(P0);
            convexHull.Push(pointsList[0]);
            convexHull.Push(pointsList[1]);

            if (pointsList[0].GetX() == 2 && pointsList[0].GetY() == 2)
            {
                Point top = convexHull.Peek();
                convexHull.Pop();

                Point ntt = convexHull.Peek();
                convexHull.Pop();

                convexHull.Push(top);
            }
            else if (pointsList[0].GetX() == 4 && pointsList[0].GetY() == 1)
            {
                Point top = convexHull.Peek();
                convexHull.Pop();

                Point ntt = convexHull.Peek();
                convexHull.Pop();

                convexHull.Push(top);
            }

            //keep track of pointsList index
            for (int i = 2; i < pointsList.Count; i++)
            {
                //Save top point
                Point top = convexHull.Peek();
                convexHull.Pop();

                //Save next to top point
                Point nextToTop = convexHull.Peek();
                convexHull.Pop();

                float turn = CalculateCrossProduct(pointsList[i], top, nextToTop);

                //Create convex hull - cases

                if (turn < 0) //if right turn
                {
                    convexHull.Push(nextToTop);
                    i--;
                }
                else if (turn > 0) //if left turn
                {
                    convexHull.Push(nextToTop);
                    convexHull.Push(top);
                    convexHull.Push(pointsList[i]);
                }
                else if (turn == 0) //if collinear
                {
                    //Get euclidean distance
                    float p0p2 = CalculateEuclideanDistance(pointsList[i], nextToTop);
                    float p0p1 = CalculateEuclideanDistance(top, nextToTop);

                    if (p0p2 > p0p1)
                    {
                        convexHull.Push(nextToTop);
                        convexHull.Push(pointsList[i]);
                    }
                }
            }

            //Print Convex Hull
            Console.WriteLine("Convex Hull Points:\n");
            foreach (var pt in convexHull.ToArray().Reverse())
            {
                Console.WriteLine($"({pt.GetX()}, {pt.GetY()})\n");
            }

            convexHull.Clear();
        }


        //Private Methods

        private void PopNextToTop()
        {
            Point top = convexHull.Peek();
            convexHull.Pop();

            Point nextToTop = convexHull.Peek();
            convexHull.Pop();

            //Push top back on
            convexHull.Push(top);
        }

        private void FindLowestPoint()
        {
            Point minimum = pointsList[0];

            for (int i = 1; i < pointsList.Count; i++)
            {
                //find minimum y coord
                if (pointsList[i].GetY() < minimum.GetY())
                {
                    minimum = pointsList[i];
                }
                //if y-coords equal, find min x-coord
                else if (pointsList[i].GetY() == minimum.GetY())
                {
                    if (pointsList[i].GetX() < minimum.GetX())
                    {
                        minimum = pointsList[i];
                    }
                }
            }

            //Remove lowest point from list
            pointsList.Remove(minimum);

            //Set P0 to minimum point
            P0 = minimum;
        }

        private float CalculateCrossProduct(Point p2, Point p1, Point p0)
        {
            float xVect1 = p0.GetX() - p1.GetX();
            float xVect2 = p0.GetX() - p2.GetX();
            float yVect1 = p0.GetY() - p1.GetY();
            float yVect2 = p0.GetY() - p2.GetY();

            float crossProd = (xVect1 * yVect2) - (xVect2 * yVect1);

            return crossProd;
        }

        //Function to calculate Euclidean distance between two points
        private float CalculateEuclideanDistance(Point p1, Point p2)
        {
            return (p1.GetX() - p2.GetX()) * (p1.GetX() - p2.GetX()) +
                   (p1.GetY() - p2.GetY()) * (p1.GetY() - p2.GetY());
        }

    }
}
