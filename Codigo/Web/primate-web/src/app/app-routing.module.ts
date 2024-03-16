import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './componentes/home/home.component';
import { PrimatesComponent } from './componentes/primates/primates.component';
import { GrupoComponent } from './componentes/grupo/grupo.component';
import { RegistroComponent } from './componentes/registro/registro.component';
import { ConectarseComponent } from './componentes/conectarse/conectarse.component';
//import { AlianzasComponent } from './componentes/alianzas/alianzas.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'primates', component: PrimatesComponent },
  { path: 'grupo', component: GrupoComponent },
  { path: 'registro', component: RegistroComponent },
  { path: 'conectarse', component: ConectarseComponent },
  //{ path: 'alianzas', component: AlianzasComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
