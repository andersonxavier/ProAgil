/* ======= */
/* MODULOS */
/* ======= */
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

// Incluido para consumir WebApi (Foi incluido la em baixo)
import { HttpClientModule } from '@angular/common/http';

// foi necessario incluir para que funcione a
// interpolacao [(ngModel)] no arquivo
// eventos.component.html
import { FormsModule } from '@angular/forms';

//  Demonstrado no curso entretanto o caminha para o angular 9 nao funciona!
//  import { BsDropdownModule, TooltipModule, ModalModule } from 'ngx/bootstrap';
// Ver imports no site do "ngx" em : https://valor-software.com/ngx-bootstrap/#/
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AppRoutingModule } from './app-routing.module';


import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

/* ======== */
/* SERVICOS */
/* ======== */
import { EventoService } from './_services/evento.service';

/* =========== */
/* COMPONENTES */
/* =========== */
import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';
import { NavComponent } from './nav/nav.component';

/* ===== */
/* PIPES */
/* ===== */
import { DateTimeFormatPipePipe } from './_helps/DateTimeFormatPipe.pipe';

// HttpClientModule inclido!
@NgModule({
   declarations: [
      AppComponent,
      EventosComponent,
      NavComponent,
      // Usado para formatar Data
      // Usando Pipe em "src\app\_helps\DateTimeFormatPipe.pipe.ts"
      // usando contantes em  "src\app\util\DateTimeFormatPipe.pipe.ts"
      DateTimeFormatPipePipe
   ],
   imports: [
      BrowserModule,

      // forRoot --> Significa que pode ser usado em qualquer lugar do Angular.
      // Caso n queria que fique para tudo colocar imports em cada arquivo .ts
      BsDropdownModule.forRoot(),
      TooltipModule.forRoot(),
      ModalModule.forRoot(),

      AppRoutingModule,
      // Responsavel por permitir a comunicacao HTTP para WebApi
      // É um modulo nativo.
      // Permite injetar por exemplo:
      //  "Contructor(private http: HttpCliente) {}" no arquivo "eventos.component.ts"
      HttpClientModule,

      // foi necessario incluir para que funcione a
      // interpolacao [(ngModel)] no arquivo
      // eventos.component.html
      FormsModule,

      // Necessario para que funcione o "dropdown" e o "dropdownToggle"
      // no arquivo "nav.component.html"
      // Nao esquecer os imports neste mesmo arquivo!
      BrowserAnimationsModule
   ],
   providers: [
      // Lembre-se que tem 3 formas de usar Procurar a Dica a respeito!!
      EventoService // --> Faz referencia ao serviço "evento.service.ts"!!!
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
