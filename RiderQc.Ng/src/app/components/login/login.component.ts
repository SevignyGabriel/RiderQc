import { Component, OnInit } from '@angular/core';
import { Router} from '@angular/router';

import { AuthService } from "angular4-social-login";
import { UserService } from '../../services/user.service';
import { FacebookLoginProvider, GoogleLoginProvider } from "angular4-social-login";

import { SocialUser } from "angular4-social-login";
import { User } from '../../model/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [AuthService, UserService]
})


export class LoginComponent implements OnInit {

  private socialUser: SocialUser;
  private loggedIn: boolean;

  user: User;
  token: string;
  err: string;

  constructor(private authService: AuthService, public userService: UserService, private router: Router) { }

  signInWithGoogle(): void {
    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }

  signInWithFB(): void {
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }

  signOut(): void {
    this.authService.signOut();
  }

  ngOnInit() {
    this.user = new User("1", "", "");
    this.authService.authState.subscribe((socialUser) => {
     this.socialUser = socialUser;
     this.loggedIn = (socialUser != null);
   });
  }

  /*Comment Utiliser le login :
      Commencer par valider les cookies :
       nous avons 2 cookies :
        -"token" sert pour mettre dans le header avec le tag Authorization
        -"username" sert pour récupérer les donneés autres du User via la fonction getUser(username:string) du UserService
     1- Ajouter un user dans votre composant
     2- aller récupérer ses données via UserService
     3- récupérer le token dans le cookie


    */

    Login() {
        console.log(this.user.Username + " " + this.user.Password)
        this.userService.Login(this.user.Username, this.user.Password).subscribe(
            (token) => {


                this.setCookie(token.Token, this.user.Username);
                this.router.navigate(['/']);
            },

            (err) => {
                if (err.status == 401) {
                    this.err = "Username or Password is incorrect";
                }
                if (err.status == 500) {
                    this.err = "Fatal Error";
                }
            }
        );
    }


    //fonction pour Onchange sur le champs de message d'erreur'
    change() {
        this.err = "";
    }

    private setCookie(token: string, username: string) {
        localStorage.setItem("token", "Bearer " + token);
        localStorage.setItem("username", username);

    }

    private removeCookie() {
        localStorage.removeItem("token");
        localStorage.removeItem("username");

    }
}



 








