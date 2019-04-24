using System.Collections.Generic;
using Contracts.DAL.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser :  IdentityUser<int>, IDomainEntity // PK type is int
    {
        // add relationships and data fields you need
        public ICollection<Person> Persons { get; set; }
    }
}