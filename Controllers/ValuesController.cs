using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Para metodos Assincronos
using ProAgil.WebAPI.Data;
using ProAgil.WebAPI.Model;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //Criado para pegar retorno do contexto
        public readonly DataContext _context;
        public ValuesController(DataContext context) {

            _context = context;
        }


        //=================================================================================
        //Usando Action Result Consumindo do banco de dados de forma Asincrona
        //=================================================================================
        [HttpGet]
        //Sinaliza que o Metodo sera assincrono e a cada chamada cria-se uma thread.
        public async Task<ActionResult> Get()
        {
            try
            {
                //Await faz com que o ToLisAsync aguarde sem executar o resto do codigo
                //Ele espera ser recuperado do banco os dados e ai sim executa o resto.
                var Result = await _context.Eventos.ToListAsync();

                return Ok(Result);
            }
            catch (System.Exception)
            {
                //Ele retorna um status 500 para uma msg customizada
                //Se esse Catch for comentado, uma msg de erro amigavel sera exibida  
                //No arquitvo "StartUp.cs" é possivel tirar a msg de erro amigavel no techo:
                //app.UseDeveloperExceptionPage();  //Basta comenta-lo
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var Results = await _context.Eventos.FirstOrDefaultAsync(x => x.EventoId == id);

                return Ok(Results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        
        }
        

        //=================================================================================
        //Usando Action Result Consumindo do banco de dados de forma sincrona
        //=================================================================================
        // [HttpGet]
        // public ActionResult Get()
        //// public ActionResult<IEnumerable<Evento>> Get()
        // {
        //     try
        //     {
        //         var Result = _context.Eventos.ToList();

        //         return Ok(Result);
        //     }
        //     catch (System.Exception)
        //     {
        //         //Ele retorna um status 500 para uma msg customizada
        //         //Se esse Catch for comentado, uma msg de erro amigavel sera exibida  
        //         //No arquitvo "StartUp.cs" é possivel tirar a msg de erro amigavel no techo:
        //         //app.UseDeveloperExceptionPage();  //Basta comenta-lo
                
        //         return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
        //     }
        // }


        //===========================
        //Usando Action Result Basico
        //===========================
        // [HttpGet]
        // public ActionResult<IEnumerable<Evento>> Get()
        // {
        //     return _context.Eventos.ToList();
        // }

        // [HttpGet("{id}")]
        // public ActionResult<Evento> Get(int id)
        // {
        //     return _context.Eventos.FirstOrDefault(x => x.EventoId == id);
        // }

        //===========================
        //Codigo Mocado
        //===========================
        //Tesntando metodo!!
        // [HttpGet]
        // public ActionResult<IEnumerable<Evento>> Get()
        // {
        //     return new Evento[] {
        //         new Evento() {
        //             EventoId = 1,
        //             Tema = "Angular e .NET Core",
        //             Local = "Belo Horizonte",
        //             Lote = "1º Lote",
        //             QtdPessoas = 250,
        //             DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")
        //         },
        //         new Evento() {
        //             EventoId = 2,
        //             Tema = "Angular e Suas Novidades",
        //             Local = "São Paulo",
        //             Lote = "2º Lote",
        //             QtdPessoas = 350,
        //             DataEvento = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy")
        //         }

        //      };
        // }

          //Tesntando metodo!!
        // [HttpGet("{id}")]
        // public ActionResult<Evento> Get(int id)
        // {
        //     return new Evento[] {
        //         new Evento() {
        //             EventoId = 1,
        //             Tema = "Angular e .NET Core",
        //             Local = "Belo Horizonte",
        //             Lote = "1º Lote",
        //             QtdPessoas = 250,
        //             DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")
        //         },
        //         new Evento() {
        //             EventoId = 2,
        //             Tema = "Angular e Suas Novidades",
        //             Local = "São Paulo",
        //             Lote = "2º Lote",
        //             QtdPessoas = 350,
        //             DataEvento = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy")
        //         }

        //      }.FirstOrDefault(x => x.EventoId == id);
        // }


        //===========================
        //Codigo Nativo
        //===========================
        // GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2","value3" };
        //}

        // GET api/values/5
        // [HttpGet("{id}")]
        // public ActionResult<string> Get(int id)
        // {
        //     return "value";
        // }


      

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
