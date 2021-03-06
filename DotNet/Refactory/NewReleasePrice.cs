﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Refactory
{
    public class NewReleasePrice : Price
    {
        public override int GetPriceCode()
        {
            return Movie.NEW_RELEASE;
        }

        public override double GetCharge(int daysRented)
        {
            return daysRented * 3;
        }

    }
}
