using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain
{
    public class Person : DomainEntity
    {
        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string LastName { get; set; }


        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        
        public ICollection<Contact> Contacts { get; set; }

        
    }
}