using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace List_07
{
    internal class Task_4
    {
        private class StockData
        {
            public string NasdaqName { get; set; }
            public double NasdaqPrice { get; set; }
            public double CurrencyRatioToUSD { get; private set; }
            public string CurrencyShortname { get; private set; }
            public StockData(string nasdaqName, double nasdaqPrice, double currencyRatioToUSD, string currencyShortname)
            {
                this.NasdaqName = nasdaqName;
                this.NasdaqPrice = nasdaqPrice;
                this.CurrencyRatioToUSD = currencyRatioToUSD;
                this.CurrencyShortname = currencyShortname;
            }

            public string ChangeCurrency(double newCurrencyRatioToUSD, string newCurrencyShortname)
            {
                string changeResult = $"Old value: {NasdaqPrice} {CurrencyShortname} --> New value: ";

                NasdaqPrice = CalculatePriceInAnotherCurrency(newCurrencyRatioToUSD);
                CurrencyRatioToUSD = newCurrencyRatioToUSD;
                CurrencyShortname = newCurrencyShortname;
                changeResult += $"{NasdaqPrice} {CurrencyShortname}";

                return changeResult;
            }

            public double CalculatePriceInAnotherCurrency(double anotherCurrencyRatioToUSD)
            {
                double recalculateRatio = (CurrencyRatioToUSD / anotherCurrencyRatioToUSD);
                return NasdaqPrice * recalculateRatio;
            }

            public override string ToString()
            {
                return $"{NasdaqName}: {NasdaqPrice} {CurrencyShortname}";
            }
        }

        public static void ReflectionDemo ()
        {
            object stockNvidia = Activator.CreateInstance(typeof(StockData), new object[] { "NVDA", 467.19, 1.0, "USD" });
            object stockIntel = Activator.CreateInstance(typeof(StockData), new object[] { "INTC", 43.74, 1.0, "USD" });

            Console.WriteLine("Created stock items:");
            Console.WriteLine(stockNvidia);
            Console.WriteLine(stockIntel);

            string methodName = "ChangeCurrency";
            Type[] methodArgumentsTypes = { typeof(double), typeof(string) };
            MethodInfo returnedMethodInfo = stockNvidia.GetType().GetMethod(methodName, methodArgumentsTypes);
            object[] argumentsToBePassed = { 0.25, "PLN" };
            object methodInvokeResult = returnedMethodInfo.Invoke(stockNvidia, argumentsToBePassed);

            Console.WriteLine($"\nResult of the {methodName} method: {methodInvokeResult}");

            methodName = "CalculatePriceInAnotherCurrency";
            Type[] methodArgumentsTypes2 = { typeof(double) };
            returnedMethodInfo = stockIntel.GetType().GetMethod(methodName, methodArgumentsTypes2);
            argumentsToBePassed = new object[] { 1.09 };
            methodInvokeResult = returnedMethodInfo.Invoke(stockIntel, argumentsToBePassed);

            Console.WriteLine($"\nResult of the {methodName} method: {((double)methodInvokeResult).ToString("F2")} EUR");
        }

    }
}
