using System.ComponentModel.DataAnnotations;

namespace PublicApi.v1.DTO
{
    public class Contact
    {

        public int Id { get; set; }
        
        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string ContactValue { get; set; }


        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int ContactTypeId { get; set; }
        public ContactType ContactType { get; set; }
    }
}