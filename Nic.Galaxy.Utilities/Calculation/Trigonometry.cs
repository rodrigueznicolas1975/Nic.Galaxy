using System;
using Nic.Galaxy.Utilities.Model;

namespace Nic.Galaxy.Utilities.Calculation
{
    public static class Trigonometry
    {
        /// <summary>
        /// Calculates the X coordinate.
        /// </summary>
        /// <param name="radius">The radius.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="day">The day.</param>
        /// <returns>X coordinate</returns>
        public static double CalculateXCoordinate(int radius, int velocity, int direction, int day)
        {
            return radius * Math.Cos(velocity * direction * day);
        }

        /// <summary>
        /// Calculates the Y coordinate.
        /// </summary>
        /// <param name="radius">The radius.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="day">The day.</param>
        /// <returns>Y coordinate</returns>
        public static double CalculateYCoordinate(int radius, int velocity, int direction, int day)
        {
            return radius * Math.Sin(velocity * direction * day);
        }

        /// <summary>
        /// Calculates the distance between two points
        /// d = Sqrt((xa-xb)^2 + (ya-yb)^2)
        /// </summary>
        /// <param name="coordinateA">The coordinate A.</param>
        /// <param name="coordinateB">The coordinate B.</param>
        /// <returns></returns>
        public static double CalculateDistanceBetweenTwoPoints(Coordinate coordinateA, Coordinate coordinateB)
        {
            return Math.Sqrt(Math.Pow((coordinateA.X - coordinateB.X), 2) + Math.Pow((coordinateA.Y - coordinateB.Y), 2));
        }

        /// <summary>
        /// Calculates the perimeter of a triangle.
        /// P = a + b + c
        /// a is the distance between points A and B
        /// b is the distance between points B and C
        /// c is the distance between points C and A
        /// </summary>
        /// <param name="coordinateA">The coordinate A.</param>
        /// <param name="coordinateB">The coordinate B.</param>
        /// <param name="coordinateC">The coordinate C.</param>
        /// <returns></returns>
        public static double CalculatePerimeterTriangle(Coordinate coordinateA, Coordinate coordinateB, Coordinate coordinateC)
        {
            return CalculateDistanceBetweenTwoPoints(coordinateA, coordinateB) +
                   CalculateDistanceBetweenTwoPoints(coordinateB, coordinateC) +
                   CalculateDistanceBetweenTwoPoints(coordinateC, coordinateA);
        }

        /// <summary>
        /// Ares the three points aligned.
        /// (A.x - B.x) / (A.y - C.y) = (A.x - C.x) / (A.y - B.y) 
        /// </summary>
        /// <param name="coordinateA">The coordinate A.</param>
        /// <param name="coordinateB">The coordinate B.</param>
        /// <param name="coordinateC">The coordinate C.</param>
        /// <returns>true is aligned</returns>
        public static bool AreThreePointsAligned(Coordinate coordinateA, Coordinate coordinateB, Coordinate coordinateC)
        {
            //Verify that A.y-C.y or A.y-B.y are not 0 because otherwise the division can not be made
            var substractAyCy = coordinateA.Y - coordinateC.Y;
            var substractAyBy = coordinateA.Y - coordinateB.Y;
            if (substractAyBy.Equals(0) || substractAyCy.Equals(0))
            {
                return substractAyBy.Equals(substractAyCy);
            }

            var substractAxBx = coordinateA.X - coordinateB.X;
            var substractAxCx = coordinateA.X - coordinateC.X;

            var p1 = Math.Round(((substractAxBx / substractAyCy) * 10) / 10);
            var p2 = Math.Round(((substractAxCx / substractAyBy) * 10) / 10);

            return p1.Equals(p2);
        }

        /// <summary>
        /// Determines whether [is point into triangle] [the specified point].
        /// Dado un triángulo ABC y un punto P del plano, P está en el interior de este triángulo si la orientación de los triángulos ABP, BCP y CAP es la misma que la orientación del triángulo ABC.
        /// http://www.dma.fi.upm.es/personal/mabellanas/tfcs/kirkpatrick/Aplicacion/algoritmos.htm#puntoInteriorAlgoritmo
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="coordinateA">The coordinate A.</param>
        /// <param name="coordinateB">The coordinate B.</param>
        /// <param name="coordinateC">The coordinate C.</param>
        /// <returns>
        ///   <c>true</c> if [is point into triangle] [the specified point]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsPointIntoTriangle(Coordinate point, Coordinate coordinateA, Coordinate coordinateB, Coordinate coordinateC)
        {
            var abc = TriangleOrientation(coordinateA, coordinateB, coordinateC);
            var abp = TriangleOrientation(coordinateA, coordinateB, point);
            var bcp = TriangleOrientation(coordinateB, coordinateC, point);
            var cap = TriangleOrientation(coordinateC, coordinateA, point);

            return (abc == abp == bcp == cap);
        }

        /// <summary>
        /// Triangles the orientation.
        /// (A.x - C.x) * (B.y - C.y) - (A.y - C.y) * (B.x - C.x)
        /// </summary>
        /// <param name="coordinateA">The coordinate A.</param>
        /// <param name="coordinateB">The coordinate B.</param>
        /// <param name="coordinateC">The coordinate C.</param>
        /// <returns>true if Orientation is positive</returns>
        public static bool TriangleOrientation(Coordinate coordinateA, Coordinate coordinateB, Coordinate coordinateC)
        {
            var orientation = (coordinateA.X - coordinateC.X) * (coordinateB.Y - coordinateC.Y) - (coordinateA.Y - coordinateC.Y) * (coordinateB.X - coordinateC.X);
            return orientation >= 0;
        }
    }
}
