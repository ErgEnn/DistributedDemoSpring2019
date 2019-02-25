import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from 'aurelia-fetch-client';
import {IContactType} from "../interfaces/IContactType";

export var log = LogManager.getLogger('ContacttypesService');

@autoinject
export class ContacttypesService {

  constructor(
    private httpClient: HttpClient
  ) {
    log.debug('constructor');
  }

  getAll(): Promise<IContactType[]> {
    // TODO: use config
    let url = 'https://localhost:5001/api/ContactTypes';

    return this.httpClient.fetch(url, {cache: 'no-store'})
      .then(response => {
        log.debug('resonse', response);
        return response.json();
      })
      .then(jsonData => {
        log.debug('jsonData', jsonData);
        return jsonData;
      }).catch(reason => {
        log.debug('catch reason', reason);
      });

  }
  
  
  create(entity: IContactType): Promise<Response> {
    let url = 'https://localhost:5001/api/ContactTypes';

    return this.httpClient.post(url, JSON.stringify(entity) ,{cache: 'no-store'}).then(
      response => {
        log.debug('response', response);
        return response;
      }
    );
    
  }

  // todo: rest crud
}
