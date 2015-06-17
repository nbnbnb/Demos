using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess
{
    internal class CustomBuilder
    {
        private static void TargetMultipleProviders()
        {
            var sql_model = GetBuilder().Build(
            new DbProviderInfo("System.Data.SqlClient", "2008"))
            .Compile();
            var ce_model = GetBuilder().Build(
            new DbProviderInfo("System.Data.SqlServerCe.4.0", "4.0"))
            .Compile();
            var sql_cstr = @"Server=.\SQLEXPRESS;
            Database=DataAccess.BreakAwayContext;
            Trusted_Connection=true";


        }

        private static DbModelBuilder GetBuilder()
        {
            var builder = new DbModelBuilder();
            builder.Entity<EdmMetadata>().ToTable("EdmMetadata");
            builder.Entity<Activity>();
            builder.Entity<Destination>();
            builder.Entity<Hostel>();
            builder.Entity<InternetSpecial>();
            builder.Entity<Lodging>();
            builder.Entity<Person>();
            builder.Entity<PersonPhoto>();
            builder.Entity<Reservation>();
            builder.Entity<Resort>();
            builder.Entity<Trip>();
            builder.ComplexType<Address>();
            builder.ComplexType<Measurement>();

            return builder;
        }
    }
}
