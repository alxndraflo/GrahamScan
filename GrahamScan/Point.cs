using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrahamScan
{
    public class Point : IComparable<Point>
    {
        private readonly float x;
        private readonly float y;

        public Point(float xIn, float yIn)
        {
            this.x = xIn;
            this.y = yIn;
        }

        public float GetX()
        {
            return this.x;
        }

        public float GetY()
        {
            return this.y;
        }

        public int CompareTo(Point that)
        {
            int result = 0;
            float p0p1_pA = CalculatePolarAngle(ConvexHull.P0, this);
            float p0p2_pA = CalculatePolarAngle(ConvexHull.P0, that);

            if (p0p1_pA < p0p2_pA)
            {
                //first point is smaller
                result = -1;
            }
            else if (p0p1_pA > p0p2_pA)
            {
                //first point is larger
                result = 1;
            }
            else if (p0p1_pA == p0p2_pA) //Polar angles equal - points collinear
            {
                //Get euclidean distance
                float p0p1_dist = CalculateEuclideanDistance(ConvexHull.P0, this);
                float p0p2_dist = CalculateEuclideanDistance(ConvexHull.P0, that);

                if (p0p1_dist < p0p2_dist)
                {
                    //first point is smaller
                    result = -1;
                }
                else
                {
                    //first point is larger
                    result = 1;
                }
            }

            return result;
        }

        private float CalculatePolarAngle(Point p0, Point p1)
        {
            float xVect = p1.x - p0.x;
            float yVect = p1.y - p0.y;

            return (float)(Math.Atan2(yVect, xVect) * (180.0f / Math.PI));
        }

        //Function to calculate euclidean distance between two points
        public float CalculateEuclideanDistance(Point p1, Point p2)
        {
            return (p1.GetX() - p2.GetX()) * (p1.GetX() - p2.GetX()) +
                   (p1.GetY() - p2.GetY()) * (p1.GetY() - p2.GetY());
        }
    }
}
