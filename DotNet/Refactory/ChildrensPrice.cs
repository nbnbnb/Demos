using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Refactory
{
    public class ChildrensPrice : Price
    {

        public override int GetPriceCode()
        {
            return Movie.CHILDRENS;
        }

        public override double GetCharge(int daysRented)
        {
            double result = 1.5;
            if (daysRented > 3)
            {
                result += (daysRented - 3) * 1.5;
            }
            return result;
        }

        public override int GetFrequentRenterPoints(int daysRented)
        {
            return 2 + daysRented;
        }


    }
}
