﻿using System.Data.Entity;
using Model;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace DataAccess
{
    public class BreakAwayContext : DbContext
    {
        public BreakAwayContext() : base("BreakAwayConsoleApp")
        {
            /*
            ((IObjectContextAdapter)this).ObjectContext
                .ObjectMaterialized += ObjectContext_ObjectMaterialized;
                **/
        }

        void ObjectContext_ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            var entity = e.Entity as IObjectWithState;
            if (entity != null)
            {
                entity.State = State.Unchanged;
            }
        }

        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Lodging> Lodgings { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Activity> Activities { get; set; }
    }
}