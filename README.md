# ASP.NET CORE Web & Distributed Demo - Spring 2019


Demo app keeps track of persons and contacts. Every App user has their own data.

AppUser 1-----01< Person 1-----01< Contact >10------1 ContactType

AppUser can also be connected to Role (those tables are provided by IdentityDbContext)

Role 1-----01< UserRole >10-----1 AppUser

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


Add domain classes, set up your relations
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

    public int ContactTypeId { get; set; } //this in not nullable int - so it is required
    public ContactType ContactType { get; set; }

    public int PersonId { get; set; }
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

    
    [MaxLength(64)]
    [MinLength(1)]
    [Required]
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
dotnet ef migrations add InitialDbCreation --project DAL --startup-project WebApp
~~~

Apply migration
~~~
dotnet ef database update --project DAL --startup-project WebApp
~~~

If you want restart:
delete Migrations folder from DAL project and delete DB:
~~~
dotnet ef database delete --project DAL --startup-project WebApp
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




##Aurelia

Create Aurelia app inside base folder (next to ASP.NET solution)

~~~
au new ClientApp
~~~

Project Configuration

    Name: ContactClient
    Platform: Web
    Bundler: Webpack
    Loader: None
    Transpiler: TypeScript
    Markup Processor: None
    CSS Processor: None
    Unit Test Runner: None
    Integration Test Runner: None
    Editor: Visual Studio Code
    Features: None
    
~~~
cd ClientApp
au run
~~~


Modify tsconfig.json, and add to front:
~~~
{
  "noImplicitAny": true,
  "strictNullChecks": true,
~~~


Install into ClientApp
~~~
npm install jquery -S
npm install @types/jquery -S
npm install bootstrap -S
npm install @types/bootstrap -S
npm install popper.js -S
~~~

App code is in \src folder
Modify main.ts to start from our own custom router page - this provides as with main navigation.

~~~
aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName('main-router')));
~~~


Add html/ts files for main-router
main-router.ts (define one initial default route/page - home)
~~~
import {PLATFORM, LogManager} from "aurelia-framework";
import {RouterConfiguration, Router} from "aurelia-router";

export var log = LogManager.getLogger('MainRouter');

export class MainRouter {
  
  public router: Router;
  
  constructor(){
    log.debug('constructor');
  }
  
  configureRouter(config: RouterConfiguration, router: Router):void {
    log.debug('configureRouter');
    
    this.router = router;
    config.title = "Contact App - Aurelia";
    config.map(
      [
        {route: ['', 'home'], name: 'home', moduleId: PLATFORM.moduleName('home'), nav: true, title: 'Home'}
      ]  
    );
    
  } 
  
}
~~~

main-router.html
~~~
<template>
      <!-- content will be rendered here-->
      <router-view></router-view>
</template>
~~~


Add bootstrap layout to main-router.html
~~~
<template>
  <!-- import css -->
  <require from="bootstrap/dist/css/bootstrap.min.css"></require>
  
  <header>
    <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
      <div class="container">
        <a class="navbar-brand">WebApp</a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
          <ul class="navbar-nav flex-grow-1">
            <li class="nav-item">
              <a class="nav-link text-dark" >Home</a>
            </li>
            <li class="nav-item">
              <a class="nav-link text-dark" >Persons</a>
            </li>
            <li class="nav-item">
              <a class="nav-link text-dark" >Contacts</a>
            </li>
            <li class="nav-item">
              <a class="nav-link text-dark" >ContactTypes</a>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  </header>

  <div class="container">
    <main role="main" class="pb-3">
      <!-- content will be rendered here-->
      <router-view></router-view>
    </main>
  </div>

  <footer class="border-top footer text-muted">
    <div class="container">
      &copy; 2019 - WebApp 
    </div>
  </footer>  
  
</template>
~~~


Add the home.html & home.ts

home.ts
~~~
import {LogManager, View} from "aurelia-framework";
import {RouteConfig, NavigationInstruction} from "aurelia-router";

export var log = LogManager.getLogger('Home');

export class Home {

  constructor() {
    log.debug('constructor');
  }

  // ============ View LifeCycle events ==============
  created(owningView: View, myView: View) {
    log.debug('created');
  }

  bind(bindingContext: Object, overrideContext: Object) {
    log.debug('bind');
  }

  attached() {
    log.debug('attached');
  }

  detached() {
    log.debug('detached');
  }

  unbind() {
    log.debug('unbind');
  }

  // ============= Router Events =============
  canActivate(params: any, routerConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    log.debug('canActivate');
  }

  activate(params: any, routerConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    log.debug('activate');
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
~~~

home.html
~~~
<template>
  Welcome!
</template>
~~~


Run from inside aurelia project folder
~~~
au run
~~~

Open browser and enjoy!