import { Component, OnInit , TemplateRef} from '@angular/core';
// import { HttpClient } from '@angular/common/http';  // Usado pelo "constructor(private http: HttpClient) { }""
// import { error } from '@angular/compiler/src/util';

import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';
// import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal/ngx-bootstrap-modal';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/ngx-bootstrap-modal';
import { Template } from '@angular/compiler/src/render3/r3_ast';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
  // Lembre-se que tem 3 formas de usar Procurar a Dica a respeito!!
  // providers:[EventoService] // Quando nao é colocado no Servico ou Modulo
})
export class EventosComponent implements OnInit {



  eventosFiltrados: Evento[]; // eventosFiltrados: any = [];
  eventos: Evento[]; // eventos: any;
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  modalRef: BsModalRef;

  // tslint:disable-next-line: variable-name
  _filtroLista: string;

  // ======
  // Foi necessario incluir para que funcione a
  // interpolacao [(ngModel)] no arquivo
  // eventos.component.html
  // filtroLista = '';
  // Graças ao paramentro a baixo foi possivel injetar dontro do arquivo
  // app.module.ts
  // trecho:
  // imports: [ ... HttpClientModule ...]
  // Obs: Parei de usar devido ao uso do Service em  "_services\evento.service.ts"
  //
  // constructor(private http: HttpClient) { }
  // ======

    // "eventoService" é nome dado
    // "EventoService" é de onde vem, neste caso "_services\evento.service.ts"
    // la ele esta como 'root' e por isso foi possivel injetar aqui
    // constructor(private eventoService: EventoService) { }

    constructor(
      private eventoService: EventoService
    , private modalService: BsModalService
    ) { }

  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }



  ngOnInit() {
    this.getEventos();
    // this.eventosFiltrados = this.eventos;//Assim
  }



  filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  // filtrarEventos(filtrarPor: string): any {
  //   filtrarPor = filtrarPor.toLocaleLowerCase();
  //   return this.eventos.filter(
  //     evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
  //   );
  // }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }



  getEventos() {
    this.eventoService.getAllEvento().subscribe(
      // tslint:disable-next-line: variable-name
      (_eventos: Evento[]) => {
      this.eventos  = _eventos;
      this.eventosFiltrados = this.eventos;
      console.log(_eventos);
    // tslint:disable-next-line: no-shadowed-variable
    }, error => {
      console.log(error);
    });
  }

  // Sem o "Observable" em "evento.service.ts"
  // getEventos() {
  //   this.eventoService.getEvento().subscribe(response => {
  //     this.eventos  = response;
  //     console.log(response);
  //   // tslint:disable-next-line: no-shadowed-variable
  //   }, error => {
  //     console.log(error);
  //   });
  // }


  // O Problema de criar desta forma a chamada de Eventos:
  // Seria melhor para ter um unico lugar para poder chamar
  // Por isso criamos a pasta "_services"
  // Desta forma teriamos replicar o codigo.
  // ".http" parou de funcionar devido ao construtor comentado
  // Usa o construtor "constructor(private http: HttpClient) { }"
  /*
  getEventos() {
    this.http.get('http://localhost:5000/api/values').subscribe(response => {
      this.eventos  = response;
      console.log(response);
    // tslint:disable-next-line: no-shadowed-variable
    }, error => {
      console.log(error);
    });
  }
  */
  // ====================
  // Codigo antigo!
  // ====================
  // eventos: any = [
  //   {
  //     EventoId: 1,
  //     Tema: 'Angular',
  //     Local: 'Belo Horizonte'
  //   },
  //   {
  //     EventoId: 2,
  //     Tema: '.NET Core',
  //     Local: 'São Paulo'
  //   },
  //   {
  //     EventoId: 3,
  //     Tema: 'Angular e .NET Core',
  //     Local: 'Rio de Janeiro'
  //   }

  // ];

  // constructor() { }

  // ngOnInit() {
  // }

}
