using System;

namespace Personnel_accounting
{
    class Program
    {
        static void Main(string[] args)
        {
            bool programRun = true;
            string[] fullNames = new string[0];
            string[] posts = new string[0];
            string command;

            while (programRun)
            {
                Console.WriteLine("Commands:\n1 - Add profile\n2 - All profiles\n3 - Delete profile\n4 - Search\n5 - Exit");

                command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        AddProfile(ref fullNames, ref posts);
                        break;
                    case "2":
                        ShowAllProfiles(fullNames, posts);
                        break;
                    case "3":
                        ShowAllProfiles(fullNames, posts);
                        DeleteProfile(ref fullNames, ref posts);
                        ShowAllProfiles(fullNames, posts);
                        break;
                    case "4":
                        SearchProfileByName(fullNames, posts);
                        break;
                    case "5":
                        programRun = false;
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
        }
        private static void AddProfile(ref string[] fullNames, ref string[] posts)
        {
            Console.WriteLine("Enter full name:");
            string newName = Console.ReadLine();

            Console.WriteLine("Enter post:");
            string newPost = Console.ReadLine();

            fullNames = ExtendArray(fullNames, newName);

            posts = ExtendArray(posts, newPost);
        }

        private static void DeleteProfile(ref string[] fullNames, ref string[] posts)
        {
            Console.WriteLine("Type profile's index to delete it:\n");

            string profileIndex = Console.ReadLine();
            int index = Convert.ToInt32(profileIndex) - 1;

            fullNames = ReduceArray(fullNames, index);

            posts = ReduceArray(posts, index);
        }

        private static string[] ExtendArray(string[] array, string newName)
        {
            string[] enlargedArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
            {
                enlargedArray[i] = array[i];
            }

            //Вообще непонятно, почему ниже бесполезное действие, я же просто назначаю в расширенный 
            //массив новое значение и этот массив возвращаю в метод AddProfile, где его сразу приравниваю к основному массиву, fullNames или posts...
            //Имеется в виду, что новое значение нужно назначать в методе AddProfile?? тогда кода больше получается...
            enlargedArray[array.Length] = newName;

            return enlargedArray;
        }
        private static void SearchProfileByName(string[] fullNames, string[] posts)
        {
            Console.WriteLine("Enter full name");

            string searchName = Console.ReadLine();
            string searchPost = "";
            bool isFound = false;
            int index = 0;

            for (int i = 0; i < fullNames.Length; i++)
            {
                if (fullNames[i] == searchName)
                {
                    searchPost = posts[i];
                    isFound = true;
                    index = i + 1;
                    break;
                }
            }
            if (isFound)
            {
                Console.WriteLine($"Result:\n\n" +
                $"{index}. {searchName} - {searchPost}");
            }
            else
            {
                Console.WriteLine("Can't find profile!");
            }
        }

        private static string[] ReduceArray(string[] array, int index)
        {
            string[] reducedArray = new string[array.Length - 1];

            for (int i = 0; i < reducedArray.Length; i++)
            {
                if (i < index)
                {
                    reducedArray[i] = array[i];
                }
                else
                {
                    reducedArray[i] = array[i + 1];
                }
            }

            return reducedArray;
        }

        private static void ShowAllProfiles(string[] fullNames, string[] posts)
        {
            for (int i = 0; i < fullNames.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {fullNames[i]} - {posts[i]}");
            }
        }

    }
}
