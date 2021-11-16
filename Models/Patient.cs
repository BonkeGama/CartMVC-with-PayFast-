using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace CartMvc.Models
{
    public class Patient
    {
        [Key]
        public string PatientID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [MyBirthDateValidation(ErrorMessage = "You cannot be from the Future!")]
        public DateTime BirthDate { get; set; }

        public string UserName { get; set; }

        [Required]
        public Enums.Sex Sex { get; set; }

        public string address { get; set; }
        [Display(Name = "Block User")]
        public Boolean Blocked { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public virtual List<AppointmentModel> Appointments { get; set; }
    }
}