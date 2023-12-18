using EF___LINQ.Data;
using EF___LINQ.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace EF___LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (NorthwindContext context = new NorthwindContext())
            {

                while (true) { 
                Console.WriteLine("Välj '1' för att visa alla kunder");
                Console.WriteLine("Välj '2' för att välja en kund");
                Console.WriteLine("Välj '3' för att skapa en ny kund");
                Console.WriteLine("Välj '4' för att avsluta programmet");
                Console.Write("Välj här: ");
                
                string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("Välj '1' för stigande ordning");
                            Console.WriteLine("Välj '2' för fallande ordning");
                            Console.Write("Välj här: ");
                            string userChoice = Console.ReadLine();

                            IQueryable<Customer> sortedQuery = null;
                            var query = context.Customers.Include(c => c.Orders);


                            if (userChoice == "1")
                            {
                                sortedQuery = query.OrderBy(c => c.CompanyName);
                            }

                            else if (userChoice == "2")
                            {
                                sortedQuery = query.OrderByDescending(c => c.CompanyName);
                            }

                            if (sortedQuery != null)
                            {
                                foreach (var customer in sortedQuery)
                                {
                                    Console.WriteLine($"Company name: {customer.CompanyName}\nCountry: {customer.Country}\nRegion: {customer.Region}\nPhone Number: {customer.Phone}\n{customer.Orders.Count}");
                                    Console.WriteLine();
                                }
                            }

                            else
                            {
                                Console.WriteLine("Fel input, försök igen!");
                            }
                            break;

                        case "2":
                            Console.Write("Ange kundens CustomerID för att visa information: ");
                            string selectedCustomer = Console.ReadLine();
                            string capitalizeSelectedCustomer = selectedCustomer.ToUpper();


                            var customers = context.Orders
                                .Where(o => o.CustomerId == capitalizeSelectedCustomer)
                                .Select(o => new
                                {
                                    o.Customer.CompanyName,
                                    o.Customer.ContactName,
                                    o.Customer.ContactTitle,
                                    o.Customer.Country,
                                    o.Customer.Phone,
                                    o.Customer.Region,
                                    o.Customer.PostalCode,
                                    o.Customer.Address,
                                    o.Customer.City,
                                    o.Customer.Fax,
                                    orderSum = o.OrderDetails
                                        .Sum(od => od.Quantity),
                                })
                                .ToList();
                            foreach (var customer in customers)
                            {
                                Console.WriteLine($"Company Name: {customer.CompanyName}");
                                Console.WriteLine($"Contact Name: {customer.ContactName}");
                                Console.WriteLine($"Contact Title: {customer.ContactTitle}");
                                Console.WriteLine($"Address: {customer.Address}");
                                Console.WriteLine($"City: {customer.City}");
                                Console.WriteLine($"Region: {customer.Region}");
                                Console.WriteLine($"Postal Code: {customer.PostalCode}");
                                Console.WriteLine($"Country: {customer.Country}");
                                Console.WriteLine($"Phone: {customer.Phone}");
                                Console.WriteLine($"Fax: {customer.Fax}");
                                Console.WriteLine($"Total Orders: {customer.orderSum}");

                            }
                            break;

                        case "3":

                            Customer createCustomer = new Customer();

                            Console.WriteLine("Enter contact name: ");
                            choice = Console.ReadLine();
                            createCustomer.ContactName = string.IsNullOrWhiteSpace(choice) ? null : choice;

                            Console.WriteLine("Enter company name: ");
                            choice = Console.ReadLine();
                            createCustomer.CompanyName = string.IsNullOrWhiteSpace(choice) ? null : choice;

                            Console.WriteLine("Enter country: ");
                            choice = Console.ReadLine();
                            createCustomer.Country = string.IsNullOrWhiteSpace(choice) ? null : choice;

                            Console.WriteLine("Enter city: ");
                            choice = Console.ReadLine();
                            createCustomer.City = string.IsNullOrWhiteSpace(choice) ? null : choice;

                            Console.WriteLine("Enter adress: ");
                            choice = Console.ReadLine();
                            createCustomer.Address = string.IsNullOrWhiteSpace(choice) ? null : choice;

                            Console.WriteLine("Enter region: ");
                            choice = Console.ReadLine();
                            createCustomer.Region = string.IsNullOrWhiteSpace(choice) ? null : choice;

                            Console.WriteLine("Enter postalcode: ");
                            choice = Console.ReadLine();
                            createCustomer.PostalCode = string.IsNullOrWhiteSpace(choice) ? null : choice;

                            Console.WriteLine("Enter phone: ");
                            choice = Console.ReadLine();
                            createCustomer.Phone = string.IsNullOrWhiteSpace(choice) ? null : choice;

                            Console.WriteLine("Enter contact title: ");
                            choice = Console.ReadLine();
                            createCustomer.ContactTitle = string.IsNullOrWhiteSpace(choice) ? null : choice;

                            Console.WriteLine("Enter fax: ");
                            choice = Console.ReadLine();
                            createCustomer.Fax = string.IsNullOrWhiteSpace(choice) ? null : choice;

                            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                            var stringChars = new char[5];
                            var random = new Random();

                            for (int i = 0; i < stringChars.Length; i++)
                            {
                                stringChars[i] = chars[random.Next(chars.Length)];
                            }

                            createCustomer.CustomerId = new String(stringChars);

                            context.Customers.Add(createCustomer);
                            context.SaveChanges();

                            Console.WriteLine("Customer added successfully.");
                            Console.WriteLine();
                            break;

                        case "4":
                            return;

                        default:
                            Console.WriteLine("Fel värde, försök igen!");
                            break;
                    }
                }
            }
        }
    }
}