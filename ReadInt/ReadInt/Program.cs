using System;

namespace ReadInt2
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseInput();
        }

        private static void ParseInput()
        {
            int number = 0;
            bool isParsed = false;

            while (isParsed == false)
            {
                Console.WriteLine("Enter an integer to parse:");

                string userInput = Console.ReadLine();
                isParsed = int.TryParse(userInput, out number);
                if (isParsed == false)
                {
                    Console.WriteLine("Something goes wrong, try again!");
                }
            }
            Console.WriteLine(number);
        }
    }
}
