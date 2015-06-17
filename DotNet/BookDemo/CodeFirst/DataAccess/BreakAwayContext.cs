using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class BreakAwayContext : DbContext
    {
        public BreakAwayContext()
            : base()
        {
            // 如果没有指定连接字符串名称
            // 则默认的练级字符串名称为类名 BreakAwayContext
        }
        public DbSet<Destination> Destinations { get; set; }

        public DbSet<Lodging> Lodgings { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

 
            modelBuilder.Entity<Lodging>()
                .Property(m => m.Name).IsUnicode(false);

            modelBuilder.Entity<Trip>()
                .HasMany(t => t.Activities)
                .WithMany(a => a.Trips)
                .Map(c =>
                {
                    c.ToTable("TripActivities");
                    c.MapLeftKey("TripIdentifier");
                    c.MapRightKey("ActivityId");
                });

            // 将实体映射到多张物理表中
            // 不要漏掉任何属性
            modelBuilder.Entity<Destination>()
                .Map(m =>
                {
                    m.Properties(n => new
                    {
                        n.Name,
                        n.Country,
                        n.Description
                    });
                    m.ToTable("Locations");
                })
                .Map(m =>
                {
                    m.Properties(n => new { n.Photo });
                    m.ToTable("LocatioinPhotos");
                });
        }
    }
}
