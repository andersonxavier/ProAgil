using System;
using System.Collections.Generic;

namespace ProAgil.Domain
{
    public class Evento
    {
        // public Evento(int eventoId, int id, string local, DateTime dataEvento, string tema, int qtdPessoas, string imagemUrl, string telefone, string lote) 
        // {
        //     //this.EventoId = eventoId;
        //     this.Id = id;
        //     this.Local = local;
        //     this.DataEvento = dataEvento;
        //     this.Tema = tema;
        //     this.QtdPessoas = qtdPessoas;
        //     this.ImagemUrl = imagemUrl;
        //     this.Telefone = telefone;
        //     this.Lote = lote;
               
        // }
        // public int EventoId {get; set;}
        public int Id {get; set;}
        public string Local {get; set;}
        public DateTime DataEvento {get; set;}
        public string Tema {get; set;}
        public int QtdPessoas {get; set;}
        public string ImagemURL {get; set;}
        public string Telefone {get; set;}
        public string Email { get; set; }
        public List<Lote> Lotes {get; set;}
        public List<RedeSocial> RedesSociais {get; set;}
        public List<PalestranteEvento> PalestranteEventos { get; set; }
    }
}