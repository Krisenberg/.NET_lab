using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List_06
{
    class Tasks_1_4
    {
        public static void TupleDemo((string, string, int, double) personalInfoTuple)
        {
            (string name1, string surname1, int age1, double salary1) = personalInfoTuple;
            Console.WriteLine("Method no 1");
            Console.WriteLine($"Name: {name1}, Surname: {surname1}, Age: {age1}, Salary: {salary1}\n");

            var (name2, surname2, age2, salary2) = personalInfoTuple;
            Console.WriteLine("Method no 2");
            Console.WriteLine($"Name: {name2}, Surname: {surname2}, Age: {age2}, Salary: {salary2}\n");

            Console.WriteLine("Method no 3");
            Console.WriteLine($"Name: {personalInfoTuple.Item1}, Surname: {personalInfoTuple.Item2}, " +
                              $"Age: {personalInfoTuple.Item3}, Salary: {personalInfoTuple.Item4}\n");
        }

        // From the documentation:
        // dynamic - Represents an object whose operations will be resolved at runtime.
        public static void AnonymousTypeDemo(dynamic anonymousInfo)
        {
            Console.WriteLine("Anonymous type as a parameter. This function takes a 'dynamic' parameter.");
            Console.WriteLine($"Name: {anonymousInfo.name}, Surname: {anonymousInfo.surname}, " +
                              $"Age: {anonymousInfo.age}, Salary: {anonymousInfo.salary}\n");
        }

        // From the Microsoft documentation:
        // To pass an anonymous type, or a collection that contains anonymous types, as an
        // argument to a method, you can declare the parameter as type object. However, using
        // object for anonymous types defeats the purpose of strong typing.
        public static void AnonymousTypeDemo_2(object anonymousInfo)
        {
            var dummyAnonymousType = new { name = "_", surname = "_", age = 0, salary = 0.0 };
            dummyAnonymousType = Cast(dummyAnonymousType, anonymousInfo);
            Console.WriteLine("Anonymous type as a parameter. This function takes an 'object' parameter." +
                              "Then some casting magic happens.");
            Console.WriteLine($"Name: {dummyAnonymousType.name}, Surname: {dummyAnonymousType.surname}, " +
                              $"Age: {dummyAnonymousType.age}, Salary: {dummyAnonymousType.salary}\n");
        }

        private static T Cast<T>(T typeHolder, object x)
        {
            return (T)x;
        }
    }
}
