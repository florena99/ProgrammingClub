using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgrammingClub.Models
{
    public class MembershipTypeModel
    {
        public Guid IDMembershipType { get; set; }

        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(50, ErrorMessage ="Name is too long(max 50)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(100, ErrorMessage ="Description is too long(max 100)")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Mandatory field")]
        [Range(1,120,ErrorMessage = "Please write a number between 1 and 120 months")]
        public int SubscriptionLengthInMonths { get; set; }
    }
}