using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO
{
    public class ContactType
    {
        public int Id { get; set; }
        
        [MaxLength(32)]
        [MinLength(1)]
        [Required]
        public string ContactTypeValue { get; set; }


        public ICollection<Contact> Contacts { get; set; }
    }
}