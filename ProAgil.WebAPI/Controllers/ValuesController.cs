using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Para metodos Assincronos
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //Criado para pegar retorno do contexto
        public readonly ProAgilContext _context;
        public ValuesController(ProAgilContext context) {

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
                var Results = await _context.Eventos.FirstOrDefaultAsync(x => x.Id == id);

                return Ok(Results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        
        }
          

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
