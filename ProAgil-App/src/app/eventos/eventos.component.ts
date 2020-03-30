import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  // tslint:disable-next-line: variable-name
  _filtroLista: string;

  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  eventosFiltrados: any = [];
  eventos: any;
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;

  // foi necessario incluir para que funcione a
  // interpolacao [(ngModel)] no arquivo
  // eventos.component.html
  // filtroLista = '';

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
    // this.eventosFiltrados = this.eventos;//Assim
  }

  filtrarEventos(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  getEventos() {
    this.http.get('http://localhost:5000/api/values').subscribe(response => {
      this.eventos  = response;
      console.log(response);
    // tslint:disable-next-line: no-shadowed-variable
    }, error => {
      console.log(error);
    });
  }


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
