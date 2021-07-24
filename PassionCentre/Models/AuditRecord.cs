using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PassionCentre.Models
{
    public class AuditRecord
    {
        [Key]
        public int Audit_ID { get; set; }

        [Required]
        [Display(Name = "Audit Action")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please enter a valid action.")]
        public string AuditActionType { get; set; }
        // Could be  Login Success /Failure/ Logout, Create, Delete, View, Update

        [Required]
        [Display(Name = "Performed By")]
        [RegularExpression("^[a-zA-Z._@+-]*$", ErrorMessage = "Please enter a valid Username.")]
        public string Username { get; set; }
        //Logged in user performing the action

        [Display(Name = "Date Stamp")]
        [DataType(DataType.Date)]
        public DateTime DateStamp { get; set; }
        //Date when the event occurred

        [Display(Name = "Time Stamp")]
        [DataType(DataType.Time)]
        public string TimeStamp { get; set; }
        //Time when the event occurred

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter a valid ID.")]
        [Display(Name = "Record ID")]
        public int KeyCourseFieldID { get; set; }
        //Store the ID of course record that is affected

        [Required]
        [RegularExpression("^[0-9.:]*$", ErrorMessage = "Please enter a valid IP Address.")]
        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }
        //Store the IP address of the user who edited
    }
}
