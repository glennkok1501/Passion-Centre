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

        [Display(Name = "Audit Action")]
        public string AuditActionType { get; set; }
        // Could be  Login Success /Failure/ Logout, Create, Delete, View, Update

        [Display(Name = "Performed By")]
        public string Username { get; set; }
        //Logged in user performing the action

        [Display(Name = "Date Stamp")]
        [DataType(DataType.Date)]
        public DateTime DateStamp { get; set; }
        //Date when the event occurred

        [Display(Name = "Time Stamp")]
        public string TimeStamp { get; set; }
        //Time when the event occurred

        [Display(Name = "Course Record ID ")]
        public int KeyCourseFieldID { get; set; }
        //Store the ID of course record that is affected

        [Display(Name = "IP Address ")]
        public string IPAddress { get; set; }
        //Store the IP address of the user who edited
    }
}
