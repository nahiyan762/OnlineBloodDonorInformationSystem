using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web.Mvc;


namespace BloodBankModels
{
    public class Login
    {
        [Key]
        public int user_id { get; set; }


        [Required(ErrorMessage = "Name Must Not Be Empty")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Name Must Be Only In Characters")]
        public string user_name { get; set; }


        [Required(ErrorMessage = "Password Must Not Be Empty")]
        public string password { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Confirm Password Must Not Be Empty")]
        [System.ComponentModel.DataAnnotations.Compare("password",ErrorMessage ="Password Did Not Matched")]
        public string confirm_password { get; set; }

        [Required(ErrorMessage = "Email Must Not Be Empty")]
        [RegularExpression(@"([\w\.]+)@([\w\.]+)\.(\w+)", ErrorMessage = "Email Address Not Correct")]
        public string email { get; set; }


        [Required(ErrorMessage = "Date Must Not Be Empty")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dob { get; set; }


        [Required(ErrorMessage = "Contact Must Not Be Empty")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Contact Number Should Be Only In Numbers")]
        public string contact { get; set; }

        [Required]
        public string type { get; set; }
    }
}
