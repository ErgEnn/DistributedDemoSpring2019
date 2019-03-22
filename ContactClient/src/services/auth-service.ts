import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from 'aurelia-fetch-client';
import {IBaseEntity} from "../interfaces/IBaseEntity";
import {AppConfig} from "../app-config";

export var log = LogManager.getLogger('AuthService');

@autoinject
export class AuthService {

  private serviceHttpClient: HttpClient;
  private serviceAppConfig: AppConfig;

  constructor(
    httpClient: HttpClient,
    appConfig: AppConfig,
  ) {
    log.debug('constructor');
    this.serviceHttpClient = httpClient;
    this.serviceAppConfig = appConfig;
  }

  // create a new entity
  register(email: string, password:string): Promise<Response> {
    let url = this.serviceAppConfig.apiUrl + "/account/register";

    
    let body = {
      email: email,
      password: password
    };
    log.debug('register', url, body);

    return this.serviceHttpClient.post(url, JSON.stringify(body), {cache: 'no-store'}).then(
      response => {
        log.debug('response', response);
        return response;
      }
    );

  }
  
}
