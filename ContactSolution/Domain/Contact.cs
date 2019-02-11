using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Contact : BaseEntity
    {
        
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