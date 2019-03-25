import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction} from "aurelia-router";
import {IContactType} from "../interfaces/IContactType";
import {ContacttypesService} from "../services/contacttypes-service";
import {BaseService} from "../services/base-service";

export var log = LogManager.getLogger('Identity.Logout');

// automatically inject dependencies declared as private constructor parameters
// will be accessible as this.<variablename> in class
@autoinject
export class Logout {

  private contactTypes: IContactType[] = [];

  constructor(
    private contacttypesService: ContacttypesService
  ) {
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
