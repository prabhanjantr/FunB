import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { Observable, observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ajax } from 'rxjs/ajax';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    'Access-Control-Allow-Origin': 'http://localhost:4200/',
'Access-Control-Allow-Credentials': 'true',
'Access-Control-Allow-Methods': 'GET, POST, DELETE, PUT',
'Access-Control-Allow-Headers': 'Content-Type' ,
  })
};


@Injectable({
  providedIn: 'root'
})
export class LoginService {
  jwtHelper = new JwtHelperService();
private _registerurl = environment.api + environment.register;
private _loginurl = environment.api+environment.login;
decodedToken: any; 

  constructor(private http: HttpClient)  {}
  RegisterUser(formData: any):Observable<any>
  {
    return this.http.post(this._registerurl,formData,httpOptions) ; 
    //return this.http.post("http://localhost:64542/api/Account/Register",formData,httpOptions);
  }
  LoginUser(formData:any):Observable<any>
  {
    return this.http.post(this._loginurl,formData,httpOptions)
    .pipe(
      map((response: any) => {
        const user = response;        
        if (user) {

          
            this.decodedToken = this.jwtHelper.decodeToken(user);
            localStorage.setItem('token', user);   
            alert('complete' + this.decodedToken)         
          }
          else {
            throw user.results.message;
          }
        
      })
    );
  }

}
