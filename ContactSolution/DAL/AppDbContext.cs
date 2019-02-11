using System;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    // Force identity db context to use our AppUser and AppRole - with int as PK type
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        
        // TODO: What to do with Cascade Delete
        
        
    }
}