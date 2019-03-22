import {LogManager} from "aurelia-framework";

export var log = LogManager.getLogger('State');

export class State {

  // Json Web Token - for auth
  public JWT : String | null = null;
  
  constructor() {
    log.debug('constructor');
  }

}
