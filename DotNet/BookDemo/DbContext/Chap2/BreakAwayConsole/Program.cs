using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using DataAccess;
using Model;
using System.Collections.Specialized;

namespace BreakAwayConsole
{



    class Program
    {
        static void Main(string[] args)
        {

            Database.SetInitializer(new InitializeBagaDatabaseWithSeedData());

            // Call the latest example method here

            // NOTE: Some examples will change data in the database. Ensure that you only call the 
            //       latest example method. The InitializeBagaDatabaseWithSeedData database initializer 
            //       (registered above) will take care of resetting the database before each run.

            //PrintAllDestinations();
            TestSaveDestinationAndLodgings();
            Console.ReadKey();
        }


        private static void SaveDestinationGraph(Destination destination)
        {
            using (var context = new BreakAwayContext())
            {
                context.Destinations.Add(destination);

                foreach (var entry in context.ChangeTracker.Entries<IObjectWithState>())
                {
                    IObjectWithState stateInfo = entry.Entity;
                    entry.State = ConvertState(stateInfo.State);
                }

                context.SaveChanges();
            }
        }

        private static void ApplyChange<TEntity>(TEntity root)
            where TEntity : class,IObjectWithState
        {
            using (var context = new BreakAwayContext())
            {
                context.Set<TEntity>().Add(root);

                foreach (var entry in context.ChangeTracker.Entries<IObjectWithState>())
                {
                    IObjectWithState stateInfo = entry.Entity;
                    entry.State = ConvertState(stateInfo.State);

                    entry.get
                }

                context.SaveChanges();
            }
        }

        private static EntityState ConvertState(State state)
        {
            switch (state)
            {
                case State.Added:
                    return EntityState.Added;
                case State.Modified:
                    return EntityState.Modified;
                case State.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Unchanged;
            }
        }

        // Add example methods here

        private static void PrintAllDestinations()
        {
            using (var context = new BreakAwayContext())
            {
                context.Database.Log = Console.Write;

                var hawaii = (from d in context.Destinations
                              where d.Name == "Hawaii"
                              select d).Single();

                var davesDump = (from l in context.Lodgings
                                 where l.Name == "Dave's Dump"
                                 select l).Single();

                //context.Entry(davesDump)
                //    .Reference(l => l.Destination)
                //    .Load();

                hawaii.Lodgings.Add(davesDump);

                Console.WriteLine("Before DetectChanges: {0}",
                    davesDump.Destination.Name);

                Console.WriteLine("After DetectChanges: {0}",
                    davesDump.Destination.Name);
            }
        }

        private static void TestSaveDestinationAndLodgings()
        {
            Destination canyon;
            using (var context = new BreakAwayContext())
            {
                canyon = (from d in context.Destinations.Include(m => m.Lodgings)
                          where d.Name == "Grand Canyon"
                          select d).Single();
            }

            canyon.Lodgings.Add(new Lodging
            {
                Name = "Big Canyon Lodge"
            });

            var firstLodging = canyon.Lodgings.ElementAt(0);
            firstLodging.Name = "New Name Holiday Park";

            var secondLodging = canyon.Lodgings.ElementAt(1);
            var deletedLodgings = new List<Lodging>();
            canyon.Lodgings.Remove(secondLodging);
            deletedLodgings.Add(secondLodging);

            SaveDestinationAndLodgings(canyon, deletedLodgings);
        }

        private static void SaveDestinationAndLodgings(
            Destination destination, List<Lodging> deletedLodgings)
        {
            using (var context = new BreakAwayContext())
            {
                context.Database.Log = Console.WriteLine;

                context.Destinations.Add(destination);

                if (destination.DestinationId > 0)
                {
                    context.Entry(destination).State = EntityState.Modified;
                }

                foreach (var lodging in destination.Lodgings)
                {
                    if (lodging.LodgingId > 0)
                    {
                        context.Entry(lodging).State = EntityState.Modified;
                    }
                }

                foreach (var loding in deletedLodgings)
                {
                    context.Entry(loding).State = EntityState.Deleted;
                }

                context.SaveChanges();
            }
        }
    }
}
