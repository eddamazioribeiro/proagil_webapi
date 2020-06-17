using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ProAgil.WebAPI.Data;
using ProAgil.WebAPI.Model;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class EventoController : ControllerBase
    {
        public readonly DataContext _context;

        public EventoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("eventos")]
        public async Task<IActionResult> RetornaTodosEventos()
        {
            try
            {
                var result = await _context.Eventos.ToListAsync();
                
                return Ok(result);
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao recuperar as informações do banco de dados\n"
                    + ex.InnerException);
            }            
        }

        [HttpGet("evento/{id}")]
        public async Task<IActionResult> RetornaEventoPorId(string id)
        {
            int.TryParse(id, out int EventoId);

            try
            {
                var result = await _context.Eventos.FirstOrDefaultAsync(x => x.EventoId == EventoId);;
                
                return Ok(result);
            }
            catch(System.Exception ex)
            {
                return this.StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Erro ao recuperar as informações do banco de dados\n"
                    + ex.InnerException);
            }              
        }
    }
}
