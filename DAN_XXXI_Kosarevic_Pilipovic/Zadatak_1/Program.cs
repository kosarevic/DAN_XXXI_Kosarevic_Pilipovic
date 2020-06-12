using System;

namespace Zadatak_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string option = "";
            //Main manu of the application.
            do
            {
                Console.WriteLine("1. Create order.");
                Console.WriteLine("2. View order.");
                Console.WriteLine("3. View all orders.");
                Console.WriteLine("4. Update order.");
                Console.WriteLine("5. Delete order");
                Console.WriteLine("6. Exit");
                Console.WriteLine();
                Console.WriteLine("Chose an option:");
                option = Console.ReadLine();
                Console.WriteLine();

                switch (option)
                {
                    case "1":
                        Service s = new Service();
                        s.CreateOrder();
                        Console.WriteLine();
                        break;
                    case "2":
                        s = new Service();
                        s.FindOne();
                        Console.WriteLine();
                        break;
                    case "3":
                        s = new Service();
                        s.FindAll();
                        Console.WriteLine();
                        break;
                    case "4":
                        s = new Service();
                        s.EditOrder();
                        Console.WriteLine();
                        break;
                    case "5":
                        s = new Service();
                        s.DeleteOrder();
                        Console.WriteLine();
                        break;
                    case "6":
                        Console.WriteLine("Thank you for using application.");
                        break;
                    default:
                        Console.WriteLine("Incorect input, please try again.\n");
                        break;
                }

            } while (option != "6");

        }
    }
}
