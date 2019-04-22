using System.Collections.Generic;

namespace BLL.App.DTO.Domain
{
    public class AppUser
    {
        public int Id { get; set; }
        public ICollection<Person> Persons { get; set; }

    }
}