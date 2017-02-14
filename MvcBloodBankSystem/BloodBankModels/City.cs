using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodBankModels
{
    public class City
    {
        [Key]
        public int city_id { get; set; }
        [Required(ErrorMessage ="City Name Must Be Empty")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "City Name Must Be Only In Characters")]
        public string city_name { get; set; }
        public List<Area> Areas { get; set; }
        public List<Donor> Donors { get; set; }

    }
}
