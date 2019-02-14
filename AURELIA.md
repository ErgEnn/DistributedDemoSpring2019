#Aurelia

Install required JS tooling

Download and install Node (LTS version)
Node installs also NPM - Node Package Manager

update NPM (if you get complaints about access rights, use sudo)
~~~
> npm install npm@latest -g
~~~

Download and install latest TypeScript compiler
~~~
> npm install -g typescript
~~~

Install Aurelia CLI
~~~
> npm install aurelia-cli -g
~~~


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

App code is in src folder
Modify main.ts to startup from our own custom router page - this provides as with main navigation.

~~~
aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName('main-router')));
~~~

Due to html case-insensitivity we have this pattern between classes and file names:  
file: my-super-utility-thing.ts class: MySuperUtilityThing

Add html/ts files for main-router  (class MainRouter)  
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

Open browser (should be port 8080) and enjoy!

To be continued...