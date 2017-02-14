using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodBankModels
{
    public class BloodGroup
    {
        [Key]
        public int blood_id { get; set; }
        public string blood_name { get; set; }

        public List<Donor> Donors { get; set; }
    }
}
