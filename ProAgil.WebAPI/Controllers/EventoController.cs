using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;
using ProAgil.WebAPI.Dtos;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]

    //"ApiController" é Responsavel por erros de validação // Tonar Desnecessario o uso de "ModelState.IsValid"
    //Sem ele o codigo entende que os verbos passarao os parametros via querystring no browser. 
    //Ele tambem torna os Anotations de valição automaticos, nao necessitando do exemplo abaixo! 
    //lembrando que seria necessário fazer em cada metodo.
    /* Exemplo de como ficaria o post: (Para que nao seja por querystringe sim pelo corpo da requisicao)
        public async Task<IActionResult> Post([FromBody]EventoDto model) {
            if(!ModelState.IsValid)
                return this.StatusCode(StatusCodes.Status400BadRequest, ModelState);
            .
            .
            .
        }
    */
    [ApiController] 
    public class EventoController : ControllerBase
    {
        //IProAgilRepository é resolvido por injecao de dependencia contido no projeto "repository

        //O NoTracking esta sendo usado na camada Repository (Equivalente ao Nolock) !!!! 

        private readonly IProAgilRepository _repo;

        private readonly IMapper _mapper;

        // Sera injetado dentro da controller gracas ao "services.AddAutoMapper()" incluido em "EventoController.cs"
        public EventoController(IProAgilRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

                [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _repo.GetAllEventoAsync(true);

                var results = _mapper.Map<EventoDto[]>(eventos);
                // Outra forma de chamar o array de DTOs
                //var results = _mapper.Map<IEnumerable<EventoDto>>(eventos);

                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco Dados Falhou {ex.Message}");
            }
        }
    
        // [HttpGet]
        // public async Task<IActionResult> Get()
        // {
        //     try
        //     {
        //         var Result = await _repo.GetAllEventoAsync(true);

        //         return Ok(Result);
        //     }
        //     catch (System.Exception)
        //     {
        //         return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
        //     }
        // }

        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {
            try
            {
                //Usando DTO
                var evento = await _repo.GetEventoAsyncById(EventoId, true);

                // Evento para DTO !! <Lembre-se que existe o inverso!>
                var results = _mapper.Map<EventoDto>(evento);
                //-----------------------------------------------------------
                // Primeiro exemplo // Sem DTO
                //var eventos = await _repo.GetAllEventoAsync(true);
                //// var results = _mapper.Map<EventoDto>(eventos);
                //var results = _mapper.Map<IEnumerable<EventoDto>>(eventos);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var eventos = await _repo.GetAllEventoAsyncByTema(tema, true);
                //var Results = await _repo.GetAllEventoAsyncByTema(tema, true);

                var results = _mapper.Map<EventoDto[]>(eventos);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        // Exemplo com DTO Reverso!
        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {

                // É inverso para estou fazendo DTO para evento.
                var evento = _mapper.Map<Evento>(model);

                _repo.Add(model);

                if(await _repo.SaveChangesAsync())
                {   
                    //usando Dto // Da o match de evento para eventoDto
                    return Created($"/api/evento/{model.Id}", _mapper.Map<EventoDto>(evento));
                    //return Created($"/api/evento/{model.Id}", model);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Banco Dados Falhou {ex.Message}");
            }

            return BadRequest();
        }

        // Sem DTO
        // [HttpPost]
        // public async Task<IActionResult> Post(Evento model)
        // {
        //     try
        //     {
        //         _repo.Add(model);

        //         if(await _repo.SaveChangesAsync())
        //         {   
        //             return Created($"/api/evento/{model.Id}", model);
        //         }
        //     }
        //     catch (System.Exception)
        //     {
        //         return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
        //     }

        //     return BadRequest();
        // }


        [HttpPut]
        public async Task<IActionResult> Put(int EventoId, EventoDto model)
        {
            try
            {
                var evento = await _repo.GetEventoAsyncById(EventoId, false);
                if(evento == null) return NotFound();

                _mapper.Map(model, evento);

                _repo.Update(evento);

                if(await _repo.SaveChangesAsync())
                {   
                    //usando Dto // Da o match de evento para eventoDto
                    return Created($"/api/evento/{model.Id}", _mapper.Map<EventoDto>(evento));
                    //return Created($"/api/evento/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();
        }

        // Sem DTO !!
        // [HttpPut]
        // public async Task<IActionResult> Put(int EventoId, Evento model)
        // {
        //     try
        //     {
        //         var evento = await _repo.GetEventoAsyncById(EventoId, false);
        //         if(evento == null) return NotFound();

        //         _repo.Update(model);

        //         if(await _repo.SaveChangesAsync())
        //         {   
        //             return Created($"/api/evento/{model.Id}", model);
        //         }
        //     }
        //     catch (System.Exception)
        //     {
        //         return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
        //     }

        //     return BadRequest();
        // }


        [HttpDelete("{EventoId}")]
        public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {
                var evento = await _repo.GetEventoAsyncById(EventoId, false);
                if(evento == null) return NotFound();

                _repo.Delete(evento);

                if(await _repo.SaveChangesAsync())
                {   
                    return Ok();
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();
        }

    }
}