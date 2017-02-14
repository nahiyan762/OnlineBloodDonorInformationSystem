using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodBankModels
{
    public class Donor
    {
        [Key]
        public int donor_id { get; set; }
        public int user_id { get; set; }
        [Required(ErrorMessage = "Name Must Not Be Empty")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Name Must Be Only In Characters")]
        public string donor_name { get; set; }
        [Required(ErrorMessage = "Number Must Not Be Empty")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Contact Number Should Be Only In Numbers")]
        public string donor_number { get; set; }
        [Required(ErrorMessage = "Image Must Not Be Empty")]
        public string donor_image { get; set; }
        [Required(ErrorMessage = "Age Must Not Be Empty")]
        public int donor_age { get; set; }
        [Required(ErrorMessage = "Gender Must Not Be Empty")]
        public string donor_gender { get; set; }
        [Required(ErrorMessage = "Blood Group Must Not Be Empty")]
        public int blood_id { get; set; }

        [ForeignKey("blood_id")]
        public BloodGroup BloodId { get; set; }

        [Required(ErrorMessage = "City Must Not Be Empty")]
        public int city_id { get; set; }

        [ForeignKey("city_id")]
        public City CityId { get; set; }

        [Required(ErrorMessage = "Area Must Not Be Empty")]
        public int area_id { get; set; }

        [ForeignKey("area_id")]
        public Area AreaId { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime start_date { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime end_date { get; set; }

        [Required(ErrorMessage ="Comment Cant Be Empty")]
        public string comments { get; set; }

        [Required]
        public string status { get; set; }
    }
}
