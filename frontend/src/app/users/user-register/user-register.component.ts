import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-register',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent {
  registerForm = new FormGroup({
    name: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    passkey: new FormControl('', Validators.required)
  });

  constructor(
    private userService: UserService,
    private router: Router
  ) { }

  onSubmit(): void {
    // Verifica se o formulário é valido
    if (this.registerForm.valid) {
      this.userService.registerUser(this.registerForm.value).subscribe({
        next: (response) => {
          console.log('Usuário cadastrado com sucesso!', response);
          alert('Usuário cadastrado com sucesso!');
          this.router.navigate(['/users']);
        },
        error: (err) => {
          console.error('Erro ao cadastrar usuário!', err);
          
          alert(`Erro ao cadastrar: ${err.error?.message || 'Verifique os dados e tente novamente.'}`);
        }
      });
    }
  }
}