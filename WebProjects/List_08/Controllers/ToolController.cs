using Microsoft.AspNetCore.Mvc;
using System;

namespace List_08.Controllers
{
    public class ToolController : Controller
    {
        private string FormatStringEquation(double a, double b, double c)
        {
            if (a == 0 && b == 0 && c == 0)
                return "0 = 0";
            string aParamString = (a != 0) ? $"{a}x<sup>2</sup>" : "";
            string abSign = ((a != 0 && b > 0) || (a != 0 && b == 0 && c > 0)) ? " + " :
                (((a != 0 && b < 0) || (a != 0 && b == 0 && c < 0)) ? " - " : "");
            string bParamString = (b != 0) ? ((a != 0) ? $"{Math.Abs(b)}x" : $"{b}x") : "";
            string bcSign = (b != 0 && c > 0) ? " + " : ((b != 0 && c < 0) ? " - " : "");
            string cParamString = (c != 0) ? ((a != 0 || b != 0) ? $"{Math.Abs(c)}" : $"{c}") : "";

            return aParamString + abSign + bParamString + bcSign + cParamString + " = 0";
        }

        private (double? x_1, double? x_2) SolveEquation(double a, double b, double c)
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
                if (delta > 0.0)
                {
                    double sqrtDelta = Math.Sqrt(delta);
                    return (-(b - sqrtDelta) / 2 * a, -(b + sqrtDelta) / 2 * a);
                }
                if (delta == 0.0)
                    return (-(b / 2 * a), null);
            }
            return (null, null);
        }
        private (string, string, string) PrepareStringSolutions(double a, double b, double c)
        {
            if (a == 0.0 && b == 0.0 && c == 0.0)
                return ("infinity", "x &isin; &#8477", "inf_solutions");
            (double? x_1, double? x_2) solutionTuple = SolveEquation(a, b, c);

            if (solutionTuple.x_1 == null && solutionTuple.x_2 == null)
            {
                return ("0", "x &isin; &#8709;", "zero_solutions");
            }
            else
            {
                if (solutionTuple.x_2 == null)
                {
                    return ("1", $"x &isin; {{{Math.Round(solutionTuple.x_1.Value, 2)}}}", "one_solution");
                }
                else
                {
                    return ("2", $"x &isin; {{{Math.Round(solutionTuple.x_1.Value, 2)} ; {Math.Round(solutionTuple.x_2.Value, 2)}}}", "two_solutions");
                }
            }
        }

        public IActionResult Solve(double a, double b, double c)
        {

            ViewBag.equationString = FormatStringEquation(a,b,c);

            (string solutionNumber, string solutions, string cssClass) = PrepareStringSolutions(a,b,c);
            ViewBag.solutionsNumber = solutionNumber;
            ViewBag.solutions = solutions;
            ViewBag.cssClass = cssClass;

            return View();
        }
    }
}
