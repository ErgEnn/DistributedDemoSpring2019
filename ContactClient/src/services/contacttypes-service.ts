import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from 'aurelia-fetch-client';
import {IContactType} from "../interfaces/IContactType";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";

export var log = LogManager.getLogger('ContacttypesService');

@autoinject
export class ContacttypesService extends BaseService<IContactType> {
  
  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'ContactTypes');
  }
  
}
