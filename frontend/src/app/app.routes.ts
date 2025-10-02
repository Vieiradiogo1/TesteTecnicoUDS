import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'users', // <-- A rota que o navegador está tentando acessar
    loadComponent: () => import('./users/user-list/user-list.component').then(c => c.UserListComponent)
  },
  {
    path: 'register',
    loadComponent: () => import('./users/user-register/user-register.component').then(c => c.UserRegisterComponent)
  },
  {
    path: '',
    redirectTo: '/users', // Redireciona a página inicial para /users
    pathMatch: 'full'
  }
];