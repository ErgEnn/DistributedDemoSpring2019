using System.ComponentModel.DataAnnotations;
using PublicApi.v2.DTO.Identity;

namespace PublicApi.v2.DTO
{
    public class Person
    {

        public int Id { get; set; }
        
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
        
        //public ICollection<Contact> Contacts { get; set; }

        
        public string FirstLastName => FirstName + " " + LastName;
    }
}