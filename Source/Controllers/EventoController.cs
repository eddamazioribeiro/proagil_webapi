using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProAgil.WebAPI.Data;
using ProAgil.WebAPI.Model;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class EventoController : ControllerBase
    {
        public readonly DataContext _context;
        public readonly IEnumerable<Evento> _eventos;

        public EventoController(DataContext context)
        {
            _context = context;
            _eventos = _context.Eventos.ToList();
        }

        [HttpGet("eventos")]
        public IActionResult Get()
        {
            try
            {
                var result = _eventos.ToList();
                
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
        public Evento GetEventoById(string id)
        {
            int.TryParse(id, out int EventoId);
            return _eventos.FirstOrDefault(x => x.EventoId == EventoId);
        }
    }
}
