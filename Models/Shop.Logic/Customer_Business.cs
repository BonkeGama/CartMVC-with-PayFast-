using CartMvc.Models;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CartMvc.Shop.Logic
{
    public class Customer_Business
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public List<Patient> all()
        {
            return db.Patients.ToList();
        }
        public bool add(Patient model)
        {
            try
            {
                db.Patients.Add(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool edit(Patient model)
        {
            try
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public Patient find_by_id(int? id)
        {
            return db.Patients.Find(id);
        }

        public string getGender(string id_num)
        {
            if (Convert.ToInt16(id_num.Substring(7, 1)) >= 5)
                return "Male";
            else
                return "Female";
        }
    }
}
