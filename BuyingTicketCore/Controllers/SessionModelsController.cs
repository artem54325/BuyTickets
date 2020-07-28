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
    public class SessionModelsController : ControllerBase
    {
        private readonly PostgresContext _context;

        public SessionModelsController(PostgresContext context)
        {
            _context = context;
        }

        ///<summary>
        /// Получить все сеансы
        ///</summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /
        /// </remarks>
        /// <returns>Все сеансы</returns>
        /// <response code="200">Все сеансы</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionModel>>> GetSessions()
        {
            return await _context.Sessions.Include(a=>a.Tickets).ToListAsync();
        }


        ///<summary>
        /// Обновить сеанс
        ///</summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /
        /// </remarks>
        /// <returns>Текущий сеанс</returns>
        /// <response code="200">Текущий сеанс</response>
        /// <response code="400">Не найден текущий id</response>
        /// <response code="404">Ошибка обновления бд</response>
        [HttpPost("{id}")]
        public async Task<ActionResult<SessionModel>> PutSessionModel(string id, [FromBody] SessionModel sessionModel)
        {
            if (id != sessionModel.Id)
            {
                return BadRequest();
            }

            SessionModel oldModel = await _context.Sessions.FirstOrDefaultAsync(a => a.Id.Equals(id));
            oldModel.Img = sessionModel.Img;
            oldModel.PriceTicket = sessionModel.PriceTicket;
            oldModel.Room = sessionModel.Room;
            oldModel.NumberSeats = sessionModel.NumberSeats;
            oldModel.StartFilm = sessionModel.StartFilm;
            oldModel.NameFilm = sessionModel.NameFilm;

            _context.Sessions.Update(oldModel);

            //_context.Entry(sessionModel).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return sessionModel;
        }

        ///<summary>
        /// Создать сеанс
        ///</summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /Create
        /// </remarks>
        /// <returns>Текущий сеанс</returns>
        /// <response code="200">Текущий сеанс</response>
        /// <response code="400">Не найден текущий id</response>
        /// <response code="404">Ошибка обновления бд</response>
        [HttpPost("Create")]
        public async Task<ActionResult<SessionModel>> PostSessionModel([FromBody] SessionModelView sessionModel)
        {
            SessionModel model = new SessionModel
            {
                Id = Guid.NewGuid().ToString(),
                NameFilm = sessionModel.NameFilm,
                NumberSeats = sessionModel.NumberSeats,
                PriceTicket = sessionModel.PriceTicket,
                StartFilm = sessionModel.StartFilm,
                Room = sessionModel.Room,
                Img = sessionModel.Img,
                Tickets = new List<TicketModel>()
            };
            _context.Sessions.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SessionModelExists(model.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(model);
        }

        // DELETE: api/SessionModels/Delete/5
        [HttpPost("Delete/{id}")]
        public async Task<ActionResult<SessionModel>> DeleteSessionModel(string id)
        {
            var sessionModel = await _context.Sessions.Include(a=>a.Tickets)
                .FirstOrDefaultAsync(a=>a.Id.Equals(id));
            if (sessionModel == null)
            {
                return NotFound();
            }

            //_context.Sessions.Remove(sessionModel);
            _context.Entry(sessionModel).State = EntityState.Deleted;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                var str = e.Message;
                return StatusCode(400);
            }

            return sessionModel;
        }

        private bool SessionModelExists(string id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }
    }
}
