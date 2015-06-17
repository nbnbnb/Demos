using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Refactory
{
    public class Movie
    {
        public const int CHILDRENS = 2;
        public const int REGULAR = 0;
        public const int NEW_RELEASE = 1;

        private string _title;
        private Price _price;

        public Movie(string title, int priceCode)
        {
            _title = title;
            setPriceCode(priceCode);
        }

        public int getPriceCode()
        {
            return _price.GetPriceCode();
        }

        public void setPriceCode(int arg)
        {
            switch (arg)
            {
                case REGULAR:
                    _price = new RegularPrice();
                    break;
                case CHILDRENS:
                    _price = new ChildrensPrice();
                    break;
                case NEW_RELEASE:
                    _price = new NewReleasePrice();
                    break;
                default:
                    throw new ArgumentException("Incorrect Price Code");
            }
        }

        public string getTitle()
        {
            return _title;
        }

        public double GetCharge(int daysRented)
        {
            return _price.GetCharge(daysRented);
        }

        public int GetFrequentRenterPoints(int daysRented)
        {
            return _price.GetFrequentRenterPoints(daysRented);
        }
    }
}
