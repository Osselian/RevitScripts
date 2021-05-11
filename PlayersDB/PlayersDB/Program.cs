using System;
using System.Collections.Generic;

namespace PlayersDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Database playersDatabase = new Database();

            playersDatabase.AddPlayer("Bob", 2);
            playersDatabase.AddPlayer("Ash", 5);
            playersDatabase.AddPlayer("Loo", 9);

            playersDatabase.BanAtIndex(2);

            playersDatabase.UnbanAtIndex(2);

            playersDatabase.DeleteByIndex(2);
            
        }
    }

    class Database
    {
        private List<Player> _players;

        public Database()
        {
            _players = new List<Player>();
        }

        public void AddPlayer(string name, int level)
        {
            int index = _players.Count;
            Player player = new Player(index, name, level);
            _players.Add(player);
        }

        public void BanAtIndex(int index)
        {
            if (index < _players.Count && index >= 0)
            {
                _players[index].Ban();
            }
            else
            {
                Console.WriteLine("Не найдено игрока по заданному индексу");
            }
        }

        public void UnbanAtIndex(int index)
        {
            if (index < _players.Count && index >= 0)
            {
                _players[index].Unban();
            }
            else
            {
                Console.WriteLine("Не найдено игрока по заданному индексу");
            }
        }

        public void DeleteByIndex(int index)
        {
            if (index < _players.Count && index >= 0)
            {
                _players.RemoveAt(index);

                for (int i = index; i < _players.Count; i++)
                {
                    _players[i].SetIndex(i);
                }
            }
            else
            {
                Console.WriteLine("Не найдено игрока по заданному индексу");
            }
        }
    }

    class Player
    {
        public int Index { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; }
        public bool IsBanned { get; private set; }

        public Player(int index, string name, int level, bool isBanned = false)
        {
            Index = index;
            Name = name;
            Level = level;
            IsBanned = isBanned;
        }

        public void Ban()
        {
            IsBanned = true;
        }

        public void Unban()
        {
            IsBanned = false;
        }

        public void SetIndex(int index)
        {
            Index = index;
        }
    }
}
