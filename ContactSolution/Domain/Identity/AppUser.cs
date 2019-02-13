using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser :  IdentityUser<int> // PK type is int
    {
        // add relationships and data fields you need
        public ICollection<Person> Persons { get; set; }
    }
}