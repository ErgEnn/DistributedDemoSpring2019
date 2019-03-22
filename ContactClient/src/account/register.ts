import {LogManager, View} from "aurelia-framework";
import {RouteConfig, NavigationInstruction} from "aurelia-router";
import {AuthService} from "../services/auth-service";

export var log = LogManager.getLogger('Account.Register');

export class Delete {

  private email: string;
  private password: string;
  private confirmPassword: string;

  constructor(private authService: AuthService) {
    log.debug('constructor');
  }

  // ============ View methods ==============
  submit():void {
    log.debug('submit');
    
    if (this.password == this.confirmPassword && this.password.length >= 6 && this.email.length >= 1 ){

      this.authService.register(this.email, this.password).then(
        response => {
          log.debug("Response", response);
        }
      );

    } else {
      log.debug("bad data in post", this.password, this.confirmPassword, this.email);
    }
    
    
    /*
    this.contacttypesService.post(this.contactType).then(
      response => {
        if (response.status == 201){
          this.router.navigateToRoute("contacttypesIndex");
        } else {
          log.error('Error in response!', response);
        }
      }
    );
    */
    
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
