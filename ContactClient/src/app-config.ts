import {LogManager, autoinject} from "aurelia-framework";
import {IContactType} from "./interfaces/IContactType";

export var log = LogManager.getLogger('AppConfig');

@autoinject
export class AppConfig {
  
  public apiUrl = 'https://localhost:5001/api/';
  
  constructor() {
    log.debug('constructor');
  }

}
