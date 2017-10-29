
import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';


import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';


//Import Model
import { User } from '../model/user';

@Injectable()
export class UserService {
   
    baseUrl: string = "http://riderqc-api.azurewebsites.net"; //dev
    //baseUrl: string = "http://localhost:50800"; //local
    constructor(public http: Http) { }

    //Web Services


    getUser(username:string): Observable<User>
    {
        return this.http.get(`${this.baseUrl}/user?username=${username}`)
            .map(res => res.json());
        
    }


    getUsers(): Observable<User[]> {
        return this.http.get(`${this.baseUrl}/user/list`, { headers: this.getHeaders() })
            .map(res => res.json());
    }


    Login(username: string, password: string){
     
        return this.http.get(`${this.baseUrl}/login`, { headers: this.getHeadersAUTH("Basic " + btoa(username + ":" + password)) })
            .map((response: Response) => {
                if (response.status == 200) {
                    return response.json();
                }
               

            });
    }
    

    register(jsonUser) {
        let body = JSON.stringify(jsonUser);
        let headers = new Headers({
            'Content-Type': 'application/json',
            'username': btoa(jsonUser.Username),
            'password': jsonUser.Password
        });
        let options = new RequestOptions({ headers: headers });
        return this.http.post(`${this.baseUrl}/user/register`, body, options)
            .map((response: Response) => {
                                return response;
                            }).catch(this.handleError);
            
    }
    logoff()
    {
        localStorage.removeItem("username");
        localStorage.removeItem("token");
    }


    //Fonctions autres

    private getHeaders() {
        let headers = new Headers();
        headers.append('Accept', 'application/json');
        return headers;
    }


    private handleError(error: Response) {
        console.error(error.json());
        return Observable.throw(error.json().Message || 'Server error');
    }

    private getHeadersAUTH(userBtoa: string) {
        let headers = new Headers();
        headers.append("Authorization", userBtoa);
        return headers;
    }
}
