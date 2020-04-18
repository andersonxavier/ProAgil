import { Component, OnInit , TemplateRef} from '@angular/core';
// import { HttpClient } from '@angular/common/http';  // Usado pelo "constructor(private http: HttpClient) { }""
// import { error } from '@angular/compiler/src/util';

import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';
// import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal/ngx-bootstrap-modal';
import { BsModalService } from 'ngx-bootstrap/modal'; // Usado para ng-template em "eventos.component.html"
import { BsModalRef } from 'ngx-bootstrap/modal/ngx-bootstrap-modal'; // Usado para ng-template em "eventos.component.html"
import { Template } from '@angular/compiler/src/render3/r3_ast';
// Retirando o import "FormControl" Devido a nova forma de fazer sem usar o New
// import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';


// Usado para o DatePicker
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { templateJitUrl } from '@angular/compiler';
defineLocale('pt-br', ptBrLocale);

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

 
  evento: Evento;
  modoSalvar = 'post';

  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  // modalRef: BsModalRef;
  // Permite a utilização de forms no arquivo "eventos.component.html"
  registerForm: FormGroup;
  bodyDeletarEvento = '';

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
      private eventoService: EventoService // Usado para ng-template em "eventos.component.html"
    , private modalService: BsModalService // Usado para ng-template em "eventos.component.html"
    , private fb: FormBuilder // Usado para tornar o metodo validation() menos verboso
    , private localeService: BsLocaleService // Usado para o DatePicker
    ) {
      // usado no DatePicker
      this.localeService.use('pt-br');
    }

  get filtroLista(): string {
    // console.log('Entro no filtroLista --> ');
    // console.log(this._filtroLista);
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    // console.log('Entro no filtroLista com parametro string');
    // this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  editarEvento(evento: Evento, template: any) {
    this.modoSalvar = 'put';
    this.openModal(template);
    this.evento = evento;
    this.registerForm.patchValue(evento);
  }

  novoEnvento(template: any) {
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  excluirEvento(evento: Evento, template: any) {
    this.openModal(template); // ou // template.show();
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.tema}`;
  }

  confirmeDelete(template: any) {
    console.log('deletando');
    this.eventoService.deleteEvento(this.evento.id).subscribe(
      () => {
          template.hide();
          this.getEventos();
        }, error => {
          console.log(error);
        }
    );
  }

  // Primeira versao de como exibir um modal!!
  // openModal(template: TemplateRef<any>) {
  //   this.modalRef = this.modalService.show(template);
  // }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  ngOnInit() {

    // Carrega itens do Form em "eventos.component.html"
    // Se nao for carregado previamente os campo causa erro quando é renderizado!
    this.validation();

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


  validation() {
    this.registerForm = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      imagemURL: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  // Maneira Verbosa!!!!
  // validation() {
  //   this.registerForm = new FormGroup({
  //     // tslint:disable-next-line: new-parens
  //     tema: new FormControl('', 
  //       [Validators.required, Validators.minLength(4), Validators.maxLength(50)]),
  //     // tslint:disable-next-line: new-parens
  //     local: new FormControl('', Validators.required),
  //     // tslint:disable-next-line: new-parens
  //     dataEvento: new FormControl('', Validators.required),
  //     // tslint:disable-next-line: new-parens
  //     imagemURL: new FormControl('', Validators.required),
  //     // tslint:disable-next-line: new-parens
  //     qtdPessoas: new FormControl('', 
  //       [Validators.required, Validators.max(120000)]),
  //     // tslint:disable-next-line: new-parens
  //     telefone: new FormControl('', Validators.required),
  //     // tslint:disable-next-line: new-parens
  //     email: new FormControl('',
  //       [Validators.required, Validators.email])
  //   });
  // }



  salvarAlteracao(template: any) {
    if (this.registerForm.valid) {
      if (this.modoSalvar === 'post') {
        // Pega todos os valor (similar ao json e passar para a variavel evento.)
        this.evento = Object.assign({}, this.registerForm.value);
        // console.log(this.evento);
        this.eventoService.postEvento(this.evento).subscribe(
          (novoEvento: Evento) => {
            console.log('Entrou no metodo salvarAlteracao -> ');
            console.log(novoEvento);
            template.hide();
            this.getEventos();
          }, error => {
            console.log(error);
          }
        );
      } else {
        this.evento = Object.assign({}, this.registerForm.value);
        // console.log(this.evento);
        this.eventoService.putEvento(this.evento).subscribe(
          () => {
            template.hide();
            this.getEventos();
          }, error => {
            console.log(error);
          }
        );
      }
    }
  }


  getEventos() {
    this.eventoService.getAllEvento().subscribe(
      // tslint:disable-next-line: variable-name
      (_eventos: Evento[]) => {
      this.eventos  = _eventos;
      this.eventosFiltrados = this.eventos;
      console.log('Entrou no metodo getEventos -> ');
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
