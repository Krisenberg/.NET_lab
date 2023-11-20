using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List_06
{
    class Tasks_5_6
    {
        public static void DrawCard(string firstLine,  string secondLine = "Contact: XYZ@abc.com",
                                    string symbol = "-", int paddingWidth = 3, int minCardWidth = 28)
        {
            int neededMinWidth = Math.Max(firstLine.Length, secondLine.Length) + 2 * (1 + paddingWidth);
            int actualWidth = Math.Max(neededMinWidth, minCardWidth);

            for (int i = 0; i < (2 + 2*paddingWidth); i++)
            {
                if (i < paddingWidth || (1 + 2 * paddingWidth) - i < paddingWidth)
                {
                    for (int j = 0; j < actualWidth; j++)
                    {
                        Console.Write(symbol);
                    }
                }
                else
                {
                    int j = 0;
                    int textStartIndex = i == paddingWidth ? (actualWidth/2 - firstLine.Length/2) : (actualWidth / 2 - secondLine.Length / 2);
                    while (j < actualWidth)
                    {
                        if (j < paddingWidth || (actualWidth - 1) - j < paddingWidth)
                        {
                            Console.Write(symbol);
                            j++;
                        }
                        else
                        {
                            if (j == textStartIndex)
                            {
                                string textToWrite = i == paddingWidth ? firstLine : secondLine;
                                Console.Write(textToWrite);
                                j += textToWrite.Length;
                            }
                            else
                            {
                                Console.Write(" ");
                                j++;
                            }
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        public static (int, int, int, int) CountMyTypes (params object[] objects)
        {
            int evenInts = 0;
            int positiveDubles = 0;
            int properStrings = 0;
            int others = 0;

            foreach (object obj in objects)
            {
                switch (obj)
                {
                    case int intObj when intObj % 2 == 0:
                        evenInts++;
                        break;
                    case double doubleObj when doubleObj > 0:
                        positiveDubles++;
                        break;
                    case string stringObj when stringObj.Length >= 5:
                        properStrings++;
                        break;
                    default:
                        others++;
                        break;
                }
            }
            return (evenInts, positiveDubles, properStrings, others);
        }
    }
}
