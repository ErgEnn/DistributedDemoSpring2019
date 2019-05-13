# ASP.NET CORE Web & Distributed Demo - Spring 2019


Demo app keeps track of persons and contacts. Every App user has their own data.

AppUser 1-----01< Person 1-----01< Contact >10------1 ContactType

AppUser can also be connected to Role (those tables are provided by IdentityDbContext)

Role 1-----01< UserRole >10-----1 AppUser

#### ERD notations used
Ascii notation for ERD  
01< - zero or one or many  (ICollection)
1 - one (ie mandatory FK)  
10 - 1 or zero (nullable FK)

Notation for relationship is at the end of line futher away from principal entity

AppUser ----01< Person - Appuser can have 0-1-many Persons  
AppUser 1------ Person - Person always has an Appuser (ie Person always has an owner)

### Steps to recreate solution (or get inspiration)

Create folder where to keep all your different parts of application.  
This will be commited to the git.

Inside this folder create a new ASP.NET Core Web project/solution.  
Type Asp.NET Web App (Model-View-Controller), Auth: Individual Authentication (Named WebApp in most demo solutions.)  
Solution name should be different (ContactSolution here).  

Delete Data folder from WebApp.

Add DAL project (Class library, Net Standard 2.0)  
Add Domain project (Class library, Net Standard 2.0)

Create Identity directory inside Domain project.
Define custom user and role for Identity (in Domain/Identity) - specify int as PK type
~~~
namespace Domain.Identity
{
    public class AppRole : IdentityRole<int> // PK type is int
    {
        
    }
}

namespace Domain.Identity
{
    public class AppUser :  IdentityUser<int> // PK type is int
    {
        // add relationships and data fields you need
        public List<Person> Persons { get; set; }
    }
}
~~~


Add domain classes, set up your relations. Dont forget FK's, be careful with their exact names!
~~~
public abstract class BaseEntity
{
    public int Id { get; set; }
}
    
public class Contact : BaseEntity
{
    [MaxLength(32)]
    [MinLength(1)]
    [Required]

    public string ContactValue { get; set; }

    public int ContactTypeId { get; set; } //FK - this in not nullable int - so it is required relationship
    public ContactType ContactType { get; set; }

    public int PersonId { get; set; } // FK, naming pattern <Class>Id
    public Person Person { get; set; }
}
    
public class ContactType : BaseEntity
{
    [MaxLength(64)]
    [MinLength(1)]
    [Required]
    public string ContactTypeValue { get; set; }

    public List<Contact> Contacts { get; set; }
}

public class Person : BaseEntity
{
    // PK is from BaseEntity

    [MaxLength(64)]
    [MinLength(1)]
    [Required]
    public string FirstName { get; set; }

    [MaxLength(64)]
    [MinLength(1)]
    [Required]
    public string LastName { get; set; }

    public List<Contact> Contacts { get; set; }

    
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    
    
    public string FirstLastName => FirstName + " " + LastName;
    public string LastFirstName => LastName + " " + FirstName;
}    
~~~


Set up AppDbContext (in DAL project), derived from IdentityDbContext, using your own AppUser/AppRole and int as PK type
~~~
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
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // disable cascade delete
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
~~~


Install (or update) aspnet codegenerators.
~~~
dotnet tool install --global dotnet-aspnet-codegenerator
~~~
or
~~~
dotnet tool update --global dotnet-aspnet-codegenerator
~~~

Run from asp.net solution folder:

Add db migration
~~~
dotnet ef migrations add InitialDbCreation --project DAL.App.EF --startup-project WebApp
~~~

Apply migration
~~~
dotnet ef database update --project DAL.App.EF --startup-project WebApp
~~~

If you want restart:
delete Migrations folder from DAL project and drop the DB:
~~~
dotnet ef database drop --project DAL.App.EF --startup-project WebApp
~~~



Install to WebApp for controller scaffolding
Microsoft.VisualStudio.Web.CodeGeneration.Design

Run in WebApp folder (stop running project first!)

Generate MVC controllers
~~~
dotnet aspnet-codegenerator controller -name PersonsController -actions -m Person -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ContactsController -actions -m Contact -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ContactTypesController -actions -m ContactType -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~

Generate Rest controllers
~~~
dotnet aspnet-codegenerator controller -name PersonsController -actions -m Person -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ContactsController -actions -m Contact -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ContactTypesController -actions -m ContactType -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
~~~

For JS client app (Aurelia) instructions, look at AURELIA.md


## Security in ASP.NET Core (MVC, Rest/WebApi later)

ASP.NET Core Identity provides as with pretty good authentication/authorization support.
Quick overview - UI is provided out of the box, based on Razor pages. 
In controllers we can use [Authorize] and [AllowAnonymous] attributes - on top of controller and/or some action method.
And in controller code we can use User object for all identity operations.

Relax identity password requirements (add to startup.cs - ConfigureServices )
~~~
// TODO: Remove in production
services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireNonAlphanumeric = false;

});
~~~

Change all IdentityUser references to AppUser:
in Startup.cs and in Views/Shared/_ValidationScriptsPartial.cshtml.


Add extension method for easy logged in user Id retrieveal:
~~~
public static class IdentityExtensions
{
    public static int GetUserId(this ClaimsPrincipal principal)
    {
        return GetUserId<int>(principal);
    }

    public static TKey GetUserId<TKey>(this ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }

        string userId = principal.FindFirst(ClaimTypes.NameIdentifier).Value;

        if (typeof(TKey) == typeof(int))
        {
            if (!int.TryParse(userId, out _))
            {
                throw new ArgumentException("ClaimsPrincipal NameIdentifier is not of type int!");
            }
        }

        return (TKey) Convert.ChangeType(userId, typeof(TKey));

        // this is tiny bit slower, but handles GUID type also
        return (TKey) TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(userId);

    }
}
~~~

Fix controllers security issues! For example, Persons controller should only allow operations based on currently logged in user.
So you have to control request correctness on every step.


So:
Add [Authorize] on top of controller.
~~~
[Authorize]
public class PersonsController : Controller
{
~~~

Index - only return data belonging to the logged in user.
~~~
// GET: Persons
public async Task<IActionResult> Index()
{
    var persons = await _context.Persons
        .Include(p => p.AppUser)
        .Where(p => p.AppUserId == User.GetUserId()).ToListAsync();
    return View(persons);
}
~~~

Details - only return data belonging to the logged in user.

Create GET - don't include AppUser dropdown, new person shall automatically be connected to the current user in POST

Create POST - attach correct AppUserId

Edit GET - only allow editing of record if it belongs to logged in user

Edit POST - only allow db save, when you are changing data on record which belongs to logged in user (its easy to modify post data)

Delete GET - only show data, if it belong to logged in user

Delete POST - only allow db deletion, when you are deleting record which belongs to logged in user (its easy to modify post data)



#Docker
~~~
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY BLL.App/*.csproj ./BLL.App/
COPY BLL.App.DTO/*.csproj ./BLL.App.DTO/
COPY Contracts.BLL.App/*.csproj ./Contracts.BLL.App/
COPY Contracts.BLL.App/*.csproj ./Contracts.BLL.App/
COPY Contracts.DAL.App/*.csproj ./Contracts.DAL.App/
COPY DAL.App.DTO/*.csproj ./DAL.App.DTO/
COPY DAL.App.EF/*.csproj ./DAL.App.EF/
COPY Domain/*.csproj ./Domain/
COPY PublicApi.v1/*.csproj ./PublicApi.v1/
COPY PublicApi.v1.DTO/*.csproj ./PublicApi.v1.DTO/
COPY PublicApi.v2/*.csproj ./PublicApi.v2/
COPY PublicApi.v2.DTO/*.csproj ./PublicApi.v2.DTO/
COPY Resources/*.csproj ./Resources/
COPY WebApp/*.csproj ./WebApp/
RUN dotnet restore

# copy everything else and build app

COPY BLL.App/. ./BLL.App/
COPY BLL.App.DTO/. ./BLL.App.DTO/
COPY Contracts.BLL.App/. ./Contracts.BLL.App/
COPY Contracts.BLL.App/. ./Contracts.BLL.App/
COPY Contracts.DAL.App/. ./Contracts.DAL.App/
COPY DAL.App.DTO/. ./DAL.App.DTO/
COPY DAL.App.EF/. ./DAL.App.EF/
COPY Domain/. ./Domain/
COPY PublicApi.v1/. ./PublicApi.v1/
COPY PublicApi.v1.DTO/. ./PublicApi.v1.DTO/
COPY PublicApi.v2/. ./PublicApi.v2/
COPY PublicApi.v2.DTO/. ./PublicApi.v2.DTO/
COPY Resources/. ./Resources/
COPY WebApp/. ./WebApp/
WORKDIR /app/WebApp
RUN dotnet publish -c Release -o out
~~~


~~~
docker build -t webapp .
docker run --name webapp_docker --rm -it -p 8000:80 webapp
~~~

