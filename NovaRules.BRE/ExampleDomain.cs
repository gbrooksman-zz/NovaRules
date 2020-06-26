using System;
using System.Collections.Generic;
using System.Text;

namespace NovaRules.BRE
{
        public class Customer
        {
            public string Name { get; private set; }
            public bool IsPreferred { get; set; }

            public Customer(string name)
            {
                Name = name;
            }

            public void NotifyAboutDiscount()
            {
                Console.WriteLine("Customer {0} was notified about a discount", Name);
            }
        }

        public class Order
        {
            public int Id { get; private set; }
            public Customer Customer { get; private set; }
            public int Quantity { get; private set; }
            public double UnitPrice { get; private set; }
            public double PercentDiscount { get; private set; }
            public bool IsDiscounted { get { return PercentDiscount > 0; } }
            public bool IsOpen { get; private set; }

            public double Price
            {
                get { return UnitPrice * Quantity * (1.0 - PercentDiscount / 100.0); }
            }

            public Order(int id, Customer customer, int quantity, double unitPrice)
            {
                Id = id;
                Customer = customer;
                Quantity = quantity;
                UnitPrice = unitPrice;
                IsOpen = true;
                PercentDiscount = 0;
            }

            public void ApplyDiscount(double percentDiscount)
            {
                PercentDiscount = percentDiscount;
            }
        }

}
