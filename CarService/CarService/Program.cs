using System;
using System.Collections.Generic;

namespace CarService
{
    class Program
    {
        static void Main(string[] args)
        {
            CarService carService = new CarService();
            bool isRun = true;

            while (isRun)
            {
                carService.ShowWarehouse();

                carService.TakeNewCar();
                carService.ShowServiceRequired();

                Console.WriteLine("Enter to service car, Esc to deny");
                bool isChoosing = true;
                while (isChoosing)
                {
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.Enter:
                            carService.ChooseDetailsForService();
                            carService.MakeService();
                            isChoosing = false;
                            break;
                        case ConsoleKey.Escape:
                            carService.DenyClient();
                            isChoosing = false;
                            break;
                        default:
                            break;
                    }
                }

                Console.ReadKey();
                Console.Clear();
            }

        }
    }

    class CarService
    {
        private static Random _rand = new Random();
        private List<int> _warehouse = new List<int>();
        private Dictionary<Details, int> _priceList = new Dictionary<Details, int>();
        private Breakage _carInService;
        private List<int> _detailsToUse = new List<int>();
        private int _bill;
        private int _penalty;

        public int Money { get; private set; }

        public CarService()
        {
            for (int i = 0; i < 5; i++)
            {
                _priceList.Add((Details)i, _rand.Next(2000, 10000));
            }

            int detailsAmount = _rand.Next(10, 20);
            for (int i = 0; i < detailsAmount; i++)
            {
                _warehouse.Add(_rand.Next(0, 5));
            }
            _warehouse.Sort();
        }

        public void ChooseDetailsForService()
        {
            Console.WriteLine("Type a detail index from warehouse to use it.\n" +
                "Type 0 to stop choosing");

            bool inProcces = true;

            while (inProcces)
            {
                int detailIndex = ReadInt();
                if (detailIndex != 0)
                {
                    _detailsToUse.Add(_warehouse[detailIndex - 1]);
                    Console.WriteLine((Details)_warehouse[detailIndex - 1]);

                }
                else
                {
                    inProcces = false;
                }
            }

            foreach (var detail in _detailsToUse)
            {
                _warehouse.Remove(detail);
            }
        }

        public void DenyClient()
        {
            Money -= 1000;
        }

        public void MakeService()
        {
            _bill = 0;
            _penalty = 0;
            foreach (var detail in _carInService.GetDetailsRequired())
            {
                if (_detailsToUse.Contains((int)detail))
                {
                    _bill += _priceList[detail];
                    _detailsToUse.Remove((int)detail);
                }
                else
                {
                    _penalty += _priceList[detail];
                }
            }
            Console.WriteLine(_bill + " || " + _penalty);
            Money += _bill - _penalty;
        }

        public void ShowServiceRequired()
        {
            Console.Write("Full service required ");
            for (int i = 0; i < _carInService.GetDetailsRequired().Length; i++)
            {
                Console.Write($"{_carInService.GetDetailsRequired()[i]}, ");
            }
            Console.CursorLeft--;
            Console.CursorLeft--;
            Console.WriteLine(".");
        }

        public void ShowWarehouse()
        {
            int warehouseLabelPosition = 50;
            for (int i = 0; i < 21; i++)
            {
                Console.SetCursorPosition(warehouseLabelPosition, i);
                Console.WriteLine("               ");
            }
            Console.SetCursorPosition(warehouseLabelPosition, 0);
            Console.WriteLine("Warehouse");

            for (int i = 0; i < _warehouse.Count; i++)
            {
                Console.SetCursorPosition(warehouseLabelPosition, i + 1);
                Console.WriteLine(i + 1 + " - " + (Details)_warehouse[i]);
            }

            Console.SetCursorPosition(0, 0);
        }

        public void TakeNewCar()
        {
            _carInService = new Breakage();
            Console.WriteLine($"New car arrived.");
        }

        private int ReadInt()
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

    class Breakage
    {
        private static Random _random = new Random();
        private List<Details> DetailsRequired = new List<Details>();

        public Breakage()
        {
            int detailsAmount = _random.Next(1, 4);
            for (int i = 1; i <= detailsAmount; i++)
            {
                DetailsRequired.Add((Details)_random.Next(0, 5));
            }
        }

        public Details[] GetDetailsRequired()
        {
            Details[] detailsRequired = new Details[DetailsRequired.Count];

            for (int i = 0; i < DetailsRequired.Count; i++)
            {
                detailsRequired[i] = DetailsRequired[i];
            }
            return detailsRequired;
        }
    }

    enum Details
    {
        detail1,
        detail2,
        detail3,
        detail4,
        detail5
    }
}