import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Iregister } from 'src/app/interfaces/iregister';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent {

  email: string = '';
  password: string = '';
  passwordrepeat: string = '';
  name: string = '';
  iAccept: boolean = false;

  constructor(private router: Router,
    private authService: AuthService) { }

  register() {

    let request: Iregister = {
      email: this.email,
      name: this.name,
      password: this.password
    };

    this.authService.register(request).subscribe(auth => {
      console.log('login', auth);

      if (auth.success) {
        this.router.navigate(['/conectarse']);

      } else {

      }
    }, _error => {
      console.error(_error);
    });

  }

  validaCampos(): boolean {
    let validate = this.iAccept && this.email.length > 0 && this.name.length > 0 && this.password.length > 0 && this.passwordrepeat.length > 0 && (this.password === this.passwordrepeat);
    console.log('validate', validate);

    return !validate;
  }

}
