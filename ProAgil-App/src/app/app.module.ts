import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

// Incluido para consumir WebApi (Foi incluido la em baixo)
import { HttpClientModule } from '@angular/common/http';


// foi necessario incluir para que funcione a
// interpolacao [(ngModel)] no arquivo
// eventos.component.html
import { FormsModule } from '@angular/forms';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';
import { NavComponent } from './nav/nav.component';

// HttpClientModule inclido!
@NgModule({
   declarations: [
      AppComponent,
      EventosComponent,
      NavComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      // Responsavel por permitir a comunicacao HTTP para WebApi
      // É um modulo nativo.
      HttpClientModule,

      // foi necessario incluir para que funcione a
      // interpolacao [(ngModel)] no arquivo
      // eventos.component.html
      FormsModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
