using System;
using System.Reflection.Metadata.Ecma335;

namespace QuadraticEquation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            (double a, double b, double c) coeffs = ReadCoefficients();

            var solutionTuple = SolveEquation(coeffs.a, coeffs.b, coeffs.c);

            PrintSolutions(solutionTuple);
        }

        static (double a, double b, double c) ReadCoefficients()
        {
            string aStr, bStr, cStr;
            Console.Write("Input a: ");
            aStr = Console.ReadLine();
            Console.Write("Input b: ");
            bStr = Console.ReadLine();
            Console.Write("Input c: ");
            cStr = Console.ReadLine();

            return (double.Parse(aStr), double.Parse(bStr), double.Parse(cStr));
        }
        
        static (double? x_1, double? x_2) SolveEquation(double a, double b, double c)
        {
            if (a == 0.0) 
            {
                if (b == 0.0) 
                {
                    if (c == 0.0)
                        return (c, null);
                    else
                        return (null, null);
                }
                else
                {
                    return (-(c / b), null);
                }
            }
            else
            {
                double delta = b * b - 4 * a * c;
                if ( delta > 0.0)
                {
                    double sqrtDelta = Math.Sqrt(delta);
                    return (-(b - sqrtDelta) / 2 * a, -(b + sqrtDelta) / 2 * a);
                }
                if (delta == 0.0)
                    return (-(b / 2 * a), null);
            }
            return (null, null);
        }

        static void PrintSolutions((double? x_1, double? x_2) solutionTuple)
        {
            if (solutionTuple.x_1 == null && solutionTuple.x_2 == null)
            {
                Console.WriteLine("\n\nThis equation has 0 solutions.");
            }
            else
            {
                if (solutionTuple.x_2 == null)
                {
                    Console.WriteLine("\n\nThis equation has 1 solution: x = {0:0.00} .", solutionTuple.x_1);
                }
                else
                {
                    Console.WriteLine($"\n\nThis equation has 2 solutions: x_1 = {solutionTuple.x_1:0.00} and x_2 = {solutionTuple.x_2:0.00} .");
                }
            }
        }
    }
}
