using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ContactType : DomainEntity
    {
        [MaxLength(32)]
        [MinLength(1)]
        [Required]
        public string ContactTypeValue { get; set; }


        public ICollection<Contact> Contacts { get; set; }
    }
}