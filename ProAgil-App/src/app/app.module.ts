import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

// Incluido para consumir WebApi (Foi incluido la em baixo)
import { HttpClientModule } from '@angular/common/http';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';

// HttpClientModule inclido!
@NgModule({
   declarations: [
      AppComponent,
      EventosComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      // Responsavel por permitir a comunicacao HTTP para WebApi
      // É um modulo nativo.
      HttpClientModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
