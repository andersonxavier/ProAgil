import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventos: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
    // this.eventosFiltrados = this.eventos;//Assim
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
