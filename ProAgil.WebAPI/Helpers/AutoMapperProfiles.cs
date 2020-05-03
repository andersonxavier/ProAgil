using System.Linq;
using AutoMapper;
using ProAgil.Domain;
using ProAgil.WebAPI.Dtos;

namespace ProAgil.WebAPI.Helpers
{

    //Quando referenciar o AutoMapper la dentro do nosso
    //arquivo Startup.cs, o proprio automo mapper quando a
    //aplicacao estiver rodando, ele faz uma verificacao
    //na DLL Dotnet Core que foi gerada e se pergunta
    //"Eu tenho alguma arquivo de profile ?" entao
    //magicamente ele encontra nossa classe
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //CreateMap<(Para de Dominio), (Parte de visualizaçao)>();
            // Poderia ficar => CreateMap<(Algum domain), (Alguma View)>();
            CreateMap<Evento, EventoDto>()
                .ForMember(dest => dest.Palestrantes, opt => {
                    opt.MapFrom(src => src.PalestranteEventos.Select(x => x.Palestrante).ToList() );
                })
                .ReverseMap();


            //----------------------------------------
            // Formas de fazer DTO Reverso
            // entretanto é mais facil colocar o ".ReverseMap()'
            //----------------------------------------
            //Jeito 2
            // Mapeado de forma automatica (ele ja reconhece)
            //CreateMap<EventoDto, Evento>();
            
            //Jeito 1
            // // Foma inversa de fazer o DTO (Modo dificil!!!)
            // // é o inverso de  "CreateMap<Evento, EventoDto>()"
            // CreateMap<EventoDto, Evento>()
            //     .ForMember(dest => dest.PalestranteEventos.Select(x => x.Palestrante).ToList(), opt => {
            //        opt.MapFrom(dest => dest.Palestrantes);
            //     });

            CreateMap<Evento, PalestranteDto>()
                .ForMember(dest => dest.Eventos, opt => {
                    opt.MapFrom(src => src.PalestranteEventos.Select(x => x.Evento).ToList() );
                })
                .ReverseMap();
            


            //CreateMap<Palestrante, PalestranteDto>();
            CreateMap<Lote, LoteDto>();
            CreateMap<RedeSocial, RedeSocialDto>();
            //CreateMap<RedeSocialDto, RedeSocial>();
        }
        
    }
}