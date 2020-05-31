using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProAgil.WebAPI.Data;
using ProAgil.WebAPI.Model;

namespace ProAgil.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        public readonly DataContext _context;
        public readonly IEnumerable<Evento> _eventos;

        public EventoController(DataContext context)
        {
            _context = context;
            _eventos = _context.Eventos.ToList();
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _eventos.ToList();
        }

        [HttpGet("{id}")]
        public Evento GetEventoById(string id)
        {
            int.TryParse(id, out int EventoId);
            return _eventos.FirstOrDefault(x => x.EventoId == EventoId);
        }
    }
}
