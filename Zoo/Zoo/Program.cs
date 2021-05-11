using System;
using System.Collections.Generic;

namespace Zoo
{
    class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();
            bool isRun = true;
            while (isRun)
            {
                Console.WriteLine("Welcome to the zoo. Choose enclosure to look at.\n" +
                                "Zero to end");
                zoo.ShowAviaries();

                int aviaryNuber = ReadInt();

                if (aviaryNuber > 0)
                {
                    zoo.ShowAviaryInfo(aviaryNuber);
                    Console.ReadKey();
                }
                else if (aviaryNuber == 0)
                {
                    isRun = false;
                }

                Console.Clear();
            }
        }

        static int ReadInt()
        {
            bool isCorrect = false;
            int number = -1;
            while (isCorrect == false)
            {
                Console.WriteLine("Enter the number!");
                isCorrect = int.TryParse(Console.ReadLine(), out int result);
                if (isCorrect == false)
                {
                    Console.WriteLine("Incorrect input!");
                }
                number = result;
            }
            return number;
        }
    }

    class Zoo
    {
        private Random _rand = new Random();
        private List<Aviary> _aviaries = new List<Aviary>();

        public Zoo()
        {
            for (int i = 0; i < 4; i++)
            {
                _aviaries.Add(new Aviary((Animals)i, _rand.Next(1, 10), i));
            }
        }

        public void ShowAviaries()
        {
            for (int i = 0; i < _aviaries.Count; i++)
            {
                Console.WriteLine($"Aviary №{i + 1}");
            }
        }

        public void ShowAviaryInfo(int aviaryNumber)
        {
            if (aviaryNumber <= _aviaries.Count)
            {
                _aviaries[aviaryNumber - 1].ShowInfo();
            }
            else
            {
                Console.WriteLine("Incorrect input");
                Console.ReadKey();
            }
        }
    }

    class Aviary
    {
        private static Random _rand = new Random();

        public Animals AnimalsType { get; private set; }
        public TypicalSounds Sounds { get; private set; }
        public int AnimalsAmount { get; private set; }
        public int MaleAmount { get; private set; }
        public int FemaleAmount { get; private set; }
        public int Number { get; private set; }

        public Aviary(Animals animal, int animalsAmount, int number)
        {
            AnimalsType = animal;
            switch (animal)
            {
                case Animals.Tiger:
                    Sounds = TypicalSounds.Roaring;
                    break;
                case Animals.Monkey:
                    Sounds = TypicalSounds.Shouting;
                    break;
                case Animals.Bear:
                    Sounds = TypicalSounds.Roaring;
                    break;
                case Animals.Giraffe:
                    Sounds = TypicalSounds.None;
                    break;
                default:
                    break;
            }
            AnimalsAmount = animalsAmount;

            MaleAmount = _rand.Next(0, AnimalsAmount);
            FemaleAmount = AnimalsAmount - MaleAmount;
            Number = number;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"This is enclosure with {AnimalsAmount} {AnimalsType}s.\n" +
                $"{MaleAmount} of it is male, {FemaleAmount} - female\n" +
                $"They sounds {Sounds}");
        }
    }

    enum Animals
    {
        Tiger,
        Monkey,
        Bear,
        Giraffe
    }

    enum Gender
    {
        Male,
        Female
    }

    enum TypicalSounds
    {
        Roaring,
        Shouting,
        None
    }
}
