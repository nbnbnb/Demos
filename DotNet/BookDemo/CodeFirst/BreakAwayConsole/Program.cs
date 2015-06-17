﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DataAccess;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace BreakAwayConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<BreakAwayContext>());
            InsertDestination();
        }

        private static void InsertDestination()
        {
            var destination = new Destination
            {
                Country = "Indonesia",
                Description = "EcoTourism at its best in exquisite Bali",
                Name = "Bali"
            };

            

            using (var context = new BreakAwayContext())
            { 
                context.Destinations.Add(destination);
                context.SaveChanges();

            }
        }

    }
}
