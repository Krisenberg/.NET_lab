using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace List_06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var personalInfo = (name: "Creed", surname: "Bratton", age: 58, salary: 13450.50);
            Console.WriteLine("\n____________________________TASK 1________________________________");
            Tasks_1_4.TupleDemo(personalInfo);

            Console.WriteLine("\n____________________________TASK 2________________________________");
            VariableKeywordDemo();

            Console.WriteLine("\n____________________________TASK 3________________________________");
            SystemArrayMethodsDemo();

            var personalInfoAnonymous = new { name = "Creed", surname = "Bratton", age = 58, salary = 13450.50 };
            Console.WriteLine("\n____________________________TASK 4________________________________");
            Tasks_1_4.AnonymousTypeDemo(personalInfoAnonymous);
            Console.WriteLine("------------------------------------------------------------------");
            Tasks_1_4.AnonymousTypeDemo_2(personalInfoAnonymous);

            Console.WriteLine("\n____________________________TASK 5________________________________");
            Tasks_5_6.DrawCard("Ryszard", "Rys", "X", 2, 20);
            Console.Write("\n\n\n");
            Tasks_5_6.DrawCard("Ryszard", minCardWidth: 40, symbol: "-");
            Console.Write("\n\n\n");
            Tasks_5_6.DrawCard(paddingWidth: 4, minCardWidth: -5, secondLine: "Curry", symbol: "X", firstLine: "Haskell");
            Console.Write("\n\n\n");
            Tasks_5_6.DrawCard(firstLine: "Bjarne", "Stroustrup", symbol: "#", minCardWidth: 50, paddingWidth: 2);

            Console.WriteLine("\n____________________________TASK 6________________________________\n");

            var results = Tasks_5_6.CountMyTypes(5.5, "Some text", "Some", 3, 4, 5, -6.7, 10);
            PrintParams(5.5, "Some text", "Some", 3, 4, 5, -6.7, 10);
            PrintTask6Results(results);

            results = Tasks_5_6.CountMyTypes(2, -2, 4, 0.0);
            PrintParams(2, -2, 4, 0.0);
            PrintTask6Results(results);

            results = Tasks_5_6.CountMyTypes(-5, "", "ABCDE", "ABCD", 12, 16);
            PrintParams(-5, "", "ABCDE", "ABCD", 12, 16);
            PrintTask6Results(results);
        }

        static void PrintTask6Results((int, int, int, int) resultsTuple)
        {
            (int evenInts, int positiveDubles, int properStrings, int others) = resultsTuple;
            Console.WriteLine("\nParams statistics");
            Console.WriteLine($"        [Even integers]: {evenInts}");
            Console.WriteLine($"     [Positive doubles]: {positiveDubles}");
            Console.WriteLine($"[Strings longer than 5]: {properStrings}");
            Console.WriteLine($"               [Others]: {others}\n");
        }

        static void PrintParams(params object[] args)
        {
            foreach (object arg in args) Console.Write(arg + "  |  ");
        }

        static void VariableKeywordDemo()
        {
            double @class = 123.321;
            Console.WriteLine($"Value of the 'class' variable: {@class}");
        }

        static void SystemArrayMethodsDemo()
        {
            Console.WriteLine("Demo of the System.Array methods:\n");
            int[] array = { 32, 157, -5, 26, 13, 43, 98, -12, -17, 0, 77 };

            // Method no 1: Clone()
            int[] unsortedArray = (int[]) array.Clone();
            PrintArray(ref unsortedArray, "Unsorted array at the beginning: ");

            // Method no 2: Sort()
            Array.Sort(array);
            PrintArray(ref array, "Sorted array: ");

            // Method no 3: BinarySearch()
            /*
             * If value is not found and value is less than one or more elements in array,
             * the negative number returned is the bitwise complement of the index of the
             * first element that is larger than value. If value is not found and value is
             * greater than all elements in array, the negative number returned is the bitwise
             * complement of (the index of the last element plus 1).
             * 
             */
            int itemIndex = Array.BinarySearch(array, 26);
            Console.WriteLine($"\nNumber 26 is at the index: {itemIndex}");
            itemIndex = Array.BinarySearch(array, 27);
            Console.WriteLine($"Number 27 is at the index: {itemIndex}\n");

            // Methods no 4 and 5
            // How to create an arry that will store only 5 largest numbers?
            // Combine two methods on the sorted array: Reverse() + Resize()
            Array.Reverse(array);
            Array.Resize(ref array, 5);
            Array.Reverse(array);
            PrintArray(ref array, "Top 5 largest numbers from the array: ");

        }

        static void PrintArray(ref int[] arrayToBePrinted, string header)
        {
            Console.Write(header);
            for (int i = 0; i < arrayToBePrinted.Length; i++)
            {
                if (i !=  arrayToBePrinted.Length - 1)
                    Console.Write($"{arrayToBePrinted[i]}, ");
                else
                    Console.Write($"{arrayToBePrinted[i]}\n");
            }
        }
    }
}
