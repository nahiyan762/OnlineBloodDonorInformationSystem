using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace BloodBankModels
{
    public class DBSeeder : DropCreateDatabaseIfModelChanges<BloodBankDbContext>
    {
        protected override void Seed(BloodBankDbContext context)
        {
             Login lo = new Login()
             {
                  user_name = "admin",
                  password = "admin",
                  email = "admin@aiub.edu",
                  dob = DateTime.ParseExact("1994-11-24", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                  contact = "01948714646",
                  type = "admin"
             };
                
             BloodGroup bg1 = new BloodGroup()
             {
                 blood_name = "A+"
             };
             BloodGroup bg2 = new BloodGroup()
             {
                 blood_name = "A-"
             };
             BloodGroup bg3 = new BloodGroup()
             {
                 blood_name = "B+"
             };
             BloodGroup bg4 = new BloodGroup()
             {
                 blood_name = "B-"
             };
             BloodGroup bg5 = new BloodGroup()
             {
                 blood_name = "O+"
             };
             BloodGroup bg6 = new BloodGroup()
             {
                 blood_name = "O-"
             };
             BloodGroup bg7 = new BloodGroup()
             {
                 blood_name = "AB+"
             };
             BloodGroup bg8 = new BloodGroup()
             {
                 blood_name = "AB-"
             };

             context.Logins.Add(lo);
            
             context.Bloods.Add(bg1);
             context.Bloods.Add(bg2);
             context.Bloods.Add(bg3);
             context.Bloods.Add(bg4);
             context.Bloods.Add(bg5);
             context.Bloods.Add(bg6);
             context.Bloods.Add(bg7);
             context.Bloods.Add(bg8);

             context.SaveChanges();

             base.Seed(context);
        }
    }
}
