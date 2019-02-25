import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ContacttypesService} from "../services/contacttypes-service";
import {IContactType} from "../interfaces/IContactType";

export var log = LogManager.getLogger('ContactTypes.Delete');

@autoinject
export class Delete {

  private contactType: IContactType;
  
  constructor(
    private router: Router,
    private contacttypesService: ContacttypesService
  ) {
    log.debug('constructor');
  }

  // ============ View Methods ==============

  submit():void{
    this.contacttypesService.delete(this.contactType.id).then(response => {
      if (response.status == 200){
        this.router.navigateToRoute("contacttypesIndex");
      } else {
        log.debug('response', response);
      }
    });
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
    log.debug('activate', params);
    this.contacttypesService.fetch(params.id).then(
      contactType => {
        log.debug('contactType', contactType);
        this.contactType = contactType;
      }
    );

  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
