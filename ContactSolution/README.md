# Distributed Demo Spring 2019

~~~
dotnet tool install --global dotnet-aspnet-codegenerator
~~~
or
~~~
dotnet tool update --global dotnet-aspnet-codegenerator
~~~

Run from solution folder:

Add db migration
~~~
dotnet ef migrations add InitialDbCreation --project DAL --startup-project WebApp
~~~

Apply migration
~~~
dotnet ef database update --project DAL --startup-project WebApp
~~~

If you want to delete the DB
~~~
dotnet ef database delete --project DAL --startup-project WebApp
~~~


Install to WebApp
Microsoft.VisualStudio.Web.CodeGeneration.Design

Run in WebApp folder (stop running project first)

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
