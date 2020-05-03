using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebAPI.Dtos
{
    public class RedeSocialDto
    {
        // O Ef quando nao encontra id em um update ele se comporta como insert
        // por este motivo foi colocado o Id.
        public int Id { get; set; }
        [Required (ErrorMessage="O Campo {0} é Obrigatório")]
        public string Nome {get; set;}
        [Required (ErrorMessage="O Campo {0} é Obrigatório")]
        public string URL {get; set;}
    }
}