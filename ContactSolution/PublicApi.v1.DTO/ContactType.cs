using System.ComponentModel.DataAnnotations;

namespace PublicApi.v1.DTO
{
    public class ContactType
    {
        public int Id { get; set; }
        
        [MaxLength(32)]
        [MinLength(1)]
        [Required]
        public string ContactTypeValue { get; set; }


        //public ICollection<Contact> Contacts { get; set; }
    }
}