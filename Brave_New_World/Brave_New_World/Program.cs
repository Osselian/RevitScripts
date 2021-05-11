using System;
using System.Threading.Tasks;
using System.IO;

namespace Brave_New_World
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] map = ReadMap("Map");
            
            DrawMap(map);
            DrawCreatures(map);
            PlaceCharacter(map);

            while (true)
            {

            }
            Console.ReadKey();
        }

        static void DrawMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.ForegroundColor = SetMapSymbolColor(map[i, j]);
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }
        static void PlaceCharacter(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 'Y')
                    {
                        Console.SetCursorPosition(j, i);
                    }
                }
                
            }
        }
        static void DrawCreatures(char[,] map)
        {
            string npc = ReadNPC("NPC_SAD");
            string enemy = ReadEnemy("Crab");

            for (int i = 0; i < map.GetLength(0); i++)
            {
                int index = 0;
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if(map[i,j] == 'c')
                    {
                        char symbol = GetSymbol(npc, ref index);

                        Console.SetCursorPosition(j, i);
                        Console.ForegroundColor = SetCharacterSymbolColor(symbol);
                        Console.Write(symbol);
                        index ++;
                    }
                    else if (map[i, j] == 'е')
                    {
                        char symbol = GetSymbol(enemy, ref index);

                        Console.SetCursorPosition(j, i);
                        Console.ForegroundColor = SetCharacterSymbolColor(symbol);
                        Console.Write(symbol);
                        index++;
                    }

                }
                Console.WriteLine();
            }
        }

        static char GetSymbol(string obj, ref int index)
        {
            if (index >= obj.Length)
            {
                index = 0;
            }
            return obj[index];
        }

        static char[,] ReadMap(string mapName)
        {
            string[] newFile = File.ReadAllLines($"Maps/{mapName}.txt");
            char[,] map = new char[newFile.Length, newFile[0].Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = newFile[i][j];
                }
            }

            return map;
        }

        static string ReadNPC(string npcName)
        {
            string npc = File.ReadAllText($"NPC/{npcName}.txt");

            return npc;
        }

        static string ReadEnemy(string enemyName)
        {
            string enemy = File.ReadAllText($"Enemies/{enemyName}.txt");

            return enemy;
        }

        static ConsoleColor SetMapSymbolColor(char symbol)
        {
            ConsoleColor symbolColor = ConsoleColor.White;
            switch (symbol)
            {
                case '#':
                    symbolColor = ConsoleColor.DarkGreen;
                    break;
                case 'н':
                    symbolColor = ConsoleColor.DarkGray;
                    break;
                case '.':
                    symbolColor = ConsoleColor.Green;
                    break;
                case '[':
                    symbolColor = ConsoleColor.DarkYellow;
                    break;
                case ']':
                    symbolColor = ConsoleColor.DarkYellow;
                    break;
                case '?':
                    symbolColor = ConsoleColor.Yellow;
                    break;
                case '/':
                    symbolColor = ConsoleColor.DarkRed;
                    break;
                case '\\':
                    symbolColor = ConsoleColor.DarkRed;
                    break;
                case '(':
                    symbolColor = ConsoleColor.Green;
                    break;
                case ')':
                    symbolColor = ConsoleColor.Green;
                    break;
                case '~':
                    symbolColor = ConsoleColor.Green;
                    break;
            }
            return symbolColor;
        }

        static ConsoleColor SetCharacterSymbolColor(char symbol)
        {
            ConsoleColor symbolColor = ConsoleColor.White;
            switch (symbol)
            {
                
                case '/':
                    symbolColor = ConsoleColor.Red;
                    break;
                case '\\':
                    symbolColor = ConsoleColor.Red;
                    break;
                case 'i':
                    symbolColor = ConsoleColor.Red;
                    break;
                case '{':
                    symbolColor = ConsoleColor.Red;
                    break;
                case '}':
                    symbolColor = ConsoleColor.Red;
                    break;
                case 'i':
                    symbolColor = ConsoleColor.Red;
                    break;
                case '(':
                    symbolColor = ConsoleColor.Yellow;
                    break;
                case ')':
                    symbolColor = ConsoleColor.Yellow;
                    break;
                case '^':
                    symbolColor = ConsoleColor.Yellow;
                    break;
            }
            return symbolColor;
        }
    }
}
