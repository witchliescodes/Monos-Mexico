import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Ilogin } from 'src/app/interfaces/ilogin';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-conectarse',
  templateUrl: './conectarse.component.html',
  styleUrls: ['./conectarse.component.css']
})
export class ConectarseComponent {

  email: string = '';
  password: string = '';

  constructor(
    private router: Router,
    private authService: AuthService) { }

  login() {
    let request: Ilogin = {
      email: this.email,
      password: this.password
    };

    this.email = '';
    this.password = '';
    this.authService.login(request).subscribe(auth => {
      console.log('login', auth);
      
      if (auth.isAuth) {
        this.router.navigate(['/']);

      } else {

      }
    }, _error => {
      console.error(_error);
    });

  }
}
