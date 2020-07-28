using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuyingTicketCore.Database;
using BuyingTicketCore.Models;

namespace BuyingTicketCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly PostgresContext _context;

        public TicketController(PostgresContext context)
        {
            _context = context;
        }

        ///<summary>
        /// Получение всех купленных билетов
        ///</summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /
        /// </remarks>
        /// <returns>Купленные билеты</returns>
        /// <response code="200">Купленные билеты</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketModel>>> GetTickets()
        {
            return await _context.Tickets.ToListAsync();
        }

        ///<summary>
        /// Купить новый купленный билет
        ///</summary>
        /// <remarks>
        /// Sample request:
        ///     Post /
        /// </remarks>
        /// <returns>Купить билет</returns>
        /// <response code="200">Возвращается текущий билет</response>
        /// <response code="400">Ошибка заполнения данных</response>  
        /// <response code="406">Не хватает кол-во мест</response>  
        [HttpPost]
        public async Task<ActionResult<TicketModel>> PostTicketModel([FromBody] TicketModelView ticketModelView)
        {
            if(ticketModelView.CountTickets == null || ticketModelView.CountTickets <= 0 || ticketModelView.SessionId == null) 
            {
                return StatusCode(400);
            }
            SessionModel session = await _context.Sessions.Include(a=>a.Tickets).FirstOrDefaultAsync(a => a.Id.Equals(ticketModelView.SessionId));
            if (session == null)
            {
                return StatusCode(400);
            }
            int countNowSeats = 0;
            foreach(var ticketSeat in session.Tickets)
            {
                countNowSeats += ticketSeat.CountTickets;
            }
            if (countNowSeats + ticketModelView.CountTickets > session.NumberSeats)
            {
                return StatusCode(406);
            }

            TicketModel ticket = new TicketModel
            {
                Id = Guid.NewGuid().ToString(),
                DateBuy = DateTime.Now,
                Price = session.PriceTicket * ticketModelView.CountTickets,
                SessionId = ticketModelView.SessionId,
                CountTickets = ticketModelView.CountTickets,
            };
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return Ok(ticket);
        }

        ///<summary>
        /// Удалить купленные билеты
        ///</summary>
        /// <remarks>
        /// Sample request:
        ///     Post /Delete/{id}
        /// </remarks>
        /// <returns>Возвращается удаленный билет</returns>
        /// <response code="200">Возвращается текущий билет</response>
        /// <response code="404">Если такой билет не найден при удалении</response>  
        [HttpPost("Delete/{id}")]
        public async Task<ActionResult<TicketModel>> DeleteTicketModel(string id)
        {
            var ticketModel = await _context.Tickets.FirstOrDefaultAsync(a=>a.Id.Equals(id));
            if (ticketModel == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticketModel);
            await _context.SaveChangesAsync();

            return ticketModel;
        }
    }
}
