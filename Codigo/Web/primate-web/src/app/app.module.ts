import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './componentes/header/header.component';
import { FooterComponent } from './componentes/footer/footer.component';
import { HomeComponent } from './componentes/home/home.component';
import { PrimatesComponent } from './componentes/primates/primates.component';
import { GrupoComponent } from './componentes/grupo/grupo.component';
import { RegistroComponent } from './componentes/registro/registro.component';
import { ConectarseComponent } from './componentes/conectarse/conectarse.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
//import { AlianzasComponent } from './alianzas/alianzas.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    PrimatesComponent,
    GrupoComponent,
    RegistroComponent,
    ConectarseComponent,
    //AlianzasComponent
  ],
  imports:
    [BrowserModule,
      AppRoutingModule,
      FormsModule,
      ReactiveFormsModule,
      HttpClientModule
    ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }
