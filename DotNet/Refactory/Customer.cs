using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Refactory
{
    public class Customer
    {
        private string _name;
        private List<Rental> _rentals = new List<Rental>();

        public Customer(string name)
        {
            this._name = name;
        }

        public void AddRental(Rental arg)
        {
            _rentals.Add(arg);
        }

        public string getName()
        {
            return _name;
        }

        public string Statement()
        {
            string result = "Rental Record for " + getName() + "\n";

            foreach (var rental in _rentals)
            {
                result += rental.getMovie().getTitle() + " " + rental.GetChareg().ToString() + "\n";
            }

            result += "Amount owed is " + GetTotalCharge().ToString() + "\n";
            result += "You earned " + GetTotalFrequentRenterPoints().ToString() + " frequent renter points";

            return result;
        }

        public double GetTotalCharge()
        {
            double result = 0;
            foreach (var rental in _rentals)
            {
                result += rental.GetChareg();
            }
            return result;
        }

        public int GetTotalFrequentRenterPoints()
        {
            int result = 0;
            foreach (var rental in _rentals)
            {
                result += rental.GetFrequentRenterPoints();
            }
            return result;
        }
    }
}
