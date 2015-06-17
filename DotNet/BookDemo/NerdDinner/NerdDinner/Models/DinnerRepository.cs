using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Ajax;

namespace NerdDinner.Models
{
    public class DinnerRepository
    {
        private NerdDinnerEntities entities = new NerdDinnerEntities();

        //
        // Query Methon

        public IQueryable<Dinner> FindAllDinners()
        {
            return entities.Dinners;
        }

        public IQueryable<Dinner> FindUpcomingDinners()
        {
            return from dinner in entities.Dinners
                   where dinner.EventDate > DateTime.Now
                   orderby dinner.EventDate
                   select dinner;
        }

        public Dinner GetDinner(int id)
        {
            return entities.Dinners.FirstOrDefault(d => d.DinnerID == id);
        }

        //
        //  Insert/Delete Methods

        public void Add(Dinner dinner)
        {
            entities.AddObject("Dinners", dinner);
            //entities.AddToDinners(dinner);
        }

        public void Delete(Dinner dinner)
        {
            foreach (var rsvp in dinner.RSVPs)
            {
                entities.DeleteObject(dinner.RSVPs);
            }
            entities.DeleteObject(dinner);

        }

        //
        // Persistenct

        public void Save()
        {
            entities.SaveChanges();
        }

    }
}