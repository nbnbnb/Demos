using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Refactory
{
    public class Rental
    {
        private Movie _movie;

        private int _daysRented;

        public Rental(Movie movie, int daysRented)
        {
            _movie = movie;
            _daysRented = daysRented;
        }

        public Movie getMovie()
        {
            return _movie;
        }

        public int getDaysRented()
        {
            return _daysRented;
        }

        public string getTitle()
        {
            return _movie.getTitle();
        }

        public double GetChareg()
        {
            return _movie.GetCharge(_daysRented);
        }

        public int GetFrequentRenterPoints()
        {
            return _movie.GetFrequentRenterPoints(_daysRented);
        }
    }
}
