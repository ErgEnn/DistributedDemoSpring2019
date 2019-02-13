using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ContactType : BaseEntity
    {
        [MaxLength(32)]
        [MinLength(1)]
        [Required]
        public string ContactTypeValue { get; set; }


        public ICollection<Contact> Contacts { get; set; }
    }
}