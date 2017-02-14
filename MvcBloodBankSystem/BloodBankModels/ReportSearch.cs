using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodBankModels
{
    public class ReportSearch
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string blood { get; set; }

        [Required]
        public string city { get; set; }

        [Required]
        public string area { get; set; }

        [Required]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date_now { get; set; }
    }
}
