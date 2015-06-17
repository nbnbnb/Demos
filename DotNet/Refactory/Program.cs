using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Refactory
{
    class Program
    {
        static void Main(string[] args)
        {
            Movie movie = new Movie("ASP.NET MVC", 2);
            Rental rental = new Rental(movie, 4);
            Customer custom = new Customer("ZhangJin");

            custom.AddRental(rental);

            Console.WriteLine(custom.Statement());
        }
    }
}
