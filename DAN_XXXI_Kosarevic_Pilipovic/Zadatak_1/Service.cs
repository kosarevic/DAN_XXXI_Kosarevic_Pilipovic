using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Models;

namespace Zadatak_1
{
    class Service
    {

        public void FindOne()
        {
            bool done = false;
            while (!done)
            {
                Console.WriteLine("To cancel press ~ at any moment\n");
                Console.WriteLine("Please insert identification number of your order.\n");
                string input;
                bool success = int.TryParse(input = Console.ReadLine(), out int OrderId);

                if (input == "~") break;

                Console.WriteLine();

                if (success)
                {
                    Zadatak_1_Entities cmd = new Zadatak_1_Entities();
                    var order = cmd.tblOrders.FirstOrDefault(o => o.OrderId == OrderId);
                    if (order != null)
                    {

                        foreach (var i in cmd.tblOrderMenus)
                        {
                            if (i.OrderId == order.OrderId)
                            {
                                var menu = cmd.tblMenus.FirstOrDefault(m => m.MenuId == i.MenuId);
                                Console.WriteLine(menu.Meal + " " + menu.Price);
                                done = true;
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine("Total price: " + order.Price);
                    }
                    else
                    {
                        Console.WriteLine("Non existant order. Please try again.\n");
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input, please try again.\n");
                }
            }
        }

        public void FindAll()
        {
            Zadatak_1_Entities cmd = new Zadatak_1_Entities();

            foreach (var order in cmd.tblOrders)
            {
                Console.WriteLine("Order Id: " + order.OrderId + "\n");
                foreach (var i in cmd.tblOrderMenus)
                {
                    if (order.OrderId == i.OrderId)
                    {
                        var menu = cmd.tblMenus.FirstOrDefault(m => m.MenuId == i.MenuId);
                        Console.WriteLine(menu.Meal + " " + menu.Price);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Total price: " + order.Price);
                Console.WriteLine("------------------");
            }
        }

        public void EditOrder()
        {
            bool done = false;
            while (!done)
            {
                Zadatak_1_Entities cmd = new Zadatak_1_Entities();
                Console.WriteLine("To cancel press ~ at any moment\n");
                Console.WriteLine("Insert id of the order you whant to edit.\n");
                string input;
                bool success = int.TryParse(input = Console.ReadLine(), out int OrderId);

                if (input == "~") break;

                Console.WriteLine();

                if (success)
                {
                    var order = cmd.tblOrders.FirstOrDefault(o => o.OrderId == OrderId);

                    if (order != null)
                    {
                        foreach (var i in cmd.tblOrderMenus)
                        {
                            if (i.OrderId == order.OrderId)
                            {
                                cmd.tblOrderMenus.Remove(i);
                            }
                        }
                        List<int> menus = new List<int>();
                        while (true)
                        {
                            Console.WriteLine("Select new meals from the menu.\n");
                            Console.WriteLine("If you are done, press x.\n");

                            foreach (var menu in cmd.tblMenus)
                            {
                                Console.WriteLine(menu.MenuId + ". " + menu.Meal + " " + menu.Price);
                            }
                            Console.WriteLine();
                            bool success1 = int.TryParse(input = Console.ReadLine(), out int result);

                            if (input == "~") { done = true; break; }

                            Console.WriteLine();
                            bool found = false;

                            if (success1 || input == "x")
                            {
                                foreach (var menu in cmd.tblMenus)
                                {
                                    if (result == menu.MenuId)
                                    {
                                        menus.Add(menu.MenuId);
                                        found = true;
                                    }
                                }

                                if (!found && input != "x")
                                {
                                    Console.WriteLine("Non existant meal, please try again.\n");
                                    continue;
                                }

                                if (input == "x")
                                {
                                    int? price = 0;
                                    foreach (int i in menus)
                                    {
                                        tblOrderMenu t = new tblOrderMenu();
                                        t.OrderId = order.OrderId;
                                        t.MenuId = i;

                                        cmd.tblOrderMenus.Add(t);
                                        cmd.SaveChanges();
                                    }

                                    foreach (var i in cmd.tblOrderMenus)
                                    {
                                        if (order.OrderId == i.OrderId)
                                        {
                                            foreach (var menu in cmd.tblMenus)
                                            {
                                                if (i.MenuId == menu.MenuId)
                                                {
                                                    price += menu.Price;
                                                }
                                            }
                                        }
                                    }

                                    order.Price = price;
                                    done = true;
                                    cmd.SaveChanges();
                                    Console.WriteLine("Update successfull.");
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input, please try again.\n");
                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine("Non existant order. Please try again.\n");
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input, please try again.\n");
                }
            }
        }

        public void CreateOrder()
        {
            List<tblMenu> menu = new List<tblMenu>();
            List<int> melasId = new List<int>();
            using (Zadatak_1_Entities db = new Zadatak_1_Entities())
            {
                menu = db.tblMenus.Where(x => x.MenuId > 0).ToList();
            }
            melasId = Order(menu);
            if (melasId.Count == 0)
            {
                return;
            }
            int price = 0;
            foreach (int id in melasId)
            {
                if (id == 1)
                {
                    price += 100;
                }
                else
                {
                    price += 200;
                }
            }
            tblOrder order = new tblOrder();
            order.Price = price;
            using (Zadatak_1_Entities db = new Zadatak_1_Entities())
            {
                try
                {
                    db.tblOrders.Add(order);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong...");
                    Console.WriteLine(ex.Message.ToString());
                }
                tblOrder newOrder = db.tblOrders.Where(x => x.Price == order.Price).FirstOrDefault();

                Console.WriteLine("Ordered Successfully! Order Id: {0} Price: {1}$", newOrder.OrderId, newOrder.Price);
                foreach (int id in melasId)
                {
                    tblOrderMenu orderMenu = new tblOrderMenu();
                    orderMenu.OrderId = newOrder.OrderId;
                    orderMenu.MenuId = id;
                    try
                    {
                        db.tblOrderMenus.Add(orderMenu);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Something went wrong...");
                        Console.WriteLine(ex.Message.ToString());
                    }
                }

            }

        }

        public static List<int> Order(List<tblMenu> menu)
        {
            string menuItem = "";
            List<int> allMenuIds = new List<int>();
            do
            {
                Console.WriteLine("Enter Number of the meal that you want to order, Enter 0 when you are over");
                foreach (tblMenu meni in menu)
                {
                    Console.WriteLine("{0}) {1} {2}$", meni.MenuId, meni.Meal, meni.Price);
                }
                menuItem = Console.ReadLine();
                if (int.TryParse(menuItem, out int menuItemId))
                {
                    if (menuItemId > 2)
                    {
                        Console.WriteLine("Please enter one of the numbers from food menu...");
                        continue;
                    }
                    if (menuItemId != 0)
                    {
                        allMenuIds.Add(menuItemId);
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a number");
                    continue;
                }
            }
            while (menuItem != "0");
            return allMenuIds;
        }

        public void DeleteOrder()
        {
            List<tblOrderMenu> orderMenues = new List<tblOrderMenu>();
            Console.WriteLine("Enter Id of your Order: <Enter 0 to go back>");
            string idString = Console.ReadLine();
            if (idString == "0")
            {
                return;
            }
            if (int.TryParse(idString, out int id))
            {
                using (Zadatak_1_Entities db = new Zadatak_1_Entities())
                {
                    List<tblOrder> orders = new List<tblOrder>();
                    orders = db.tblOrders.ToList();
                    int count = 0;
                    foreach (tblOrder order in orders)
                    {
                        if (order.OrderId != id)
                        {
                            continue;
                        }
                        else
                        {
                            count++;
                        }
                    }
                    if (count == 0)
                    {
                        Console.WriteLine("There is no Order with that ID");
                        return;
                    }
                    if (db.tblOrderMenus.Any())
                    {
                        try
                        {
                            orderMenues = db.tblOrderMenus.Where(x => x.OrderId == id).ToList();

                        }
                        catch
                        {
                            Console.WriteLine("There is no Orders with that Id");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no any Orders yet..");
                    }
                    for (int i = 0; i < orderMenues.Count; i++)
                    {
                        try
                        {
                            db.tblOrderMenus.Remove(orderMenues[i]);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Something went wrong...");
                            Console.WriteLine(ex.Message.ToString());
                        }

                    }
                    try
                    {
                        tblOrder order = db.tblOrders.Where(x => x.OrderId == id).FirstOrDefault();
                        db.tblOrders.Remove(order);
                        db.SaveChanges();
                        Console.WriteLine("Order Deleted Successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Something went wrong...");
                        Console.WriteLine(ex.Message.ToString());
                    }
                }
            }
            else
            {
                Console.WriteLine("Please enter a number");
                DeleteOrder();
            }
        }

    }
}
