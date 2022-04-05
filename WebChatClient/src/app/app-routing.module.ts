import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MessagesComponent } from './chat/messages/messages.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
    {
      path: '',
      pathMatch: 'full',
      redirectTo: 'home',
    },
    {
      path: 'home',
      loadChildren: () => import('./login/login.module').then((m) => m.LoginModule),
    },
    {
      path: 'register',
      loadChildren: () => import('./register/register.module').then((m) => m.RegisterModule),
    },
    {
      path: 'chat',
      loadChildren: () =>
        import('./chat/chat.module').then((m) => m.ChatModule),
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
