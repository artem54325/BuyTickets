using BuyingTicketCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyingTicketCore.Helpers
{
    public class PutData
    {
        public List<SessionModel> sessionModels = new List<SessionModel>();
        public List<TicketModel> ticketModels = new List<TicketModel>();

        public PutData()
        {
            // Сеансы
            sessionModels.Add(new SessionModel
            {
                Id = Guid.NewGuid().ToString(),
                NameFilm = "Хранители",
                Img = "https://st.kp.yandex.net/im/poster/1/8/0/kinopoisk.ru-Watchmen-1800556.jpg",
                StartFilm = new DateTime(2020, 8, 28, 15, 0, 0),
                NumberSeats = 10,
                PriceTicket = 200,
                Room = "Первый зал"
            });
            sessionModels.Add(new SessionModel
            {
                Id = Guid.NewGuid().ToString(),
                NameFilm = "Хранители",
                Img = "https://st.kp.yandex.net/im/poster/1/8/0/kinopoisk.ru-Watchmen-1800556.jpg",
                StartFilm = new DateTime(2020, 8, 28, 15, 0, 0),
                NumberSeats = 20,
                PriceTicket = 200,
                Room = "Второй зал"
            });
            sessionModels.Add(new SessionModel
            {
                Id = Guid.NewGuid().ToString(),
                NameFilm = "Третий лишний",
                Img = "https://i.artfile.ru/1920x1200_673933_%5Bwww.ArtFile.ru%5D.jpg",
                StartFilm = new DateTime(2020, 8, 28, 17, 0, 0),
                NumberSeats = 5,
                PriceTicket = 200,
                Room = "VIP зал"
            });

            // Билеты
            ticketModels.Add(new TicketModel
            {
                Id = Guid.NewGuid().ToString(),
                CountTickets = 5,
                Price = sessionModels[0].PriceTicket * 5,
                DateBuy = DateTime.Now,
                SessionId = sessionModels[0].Id,
                Session = sessionModels[0]
            });
            ticketModels.Add(new TicketModel
            {
                Id = Guid.NewGuid().ToString(),
                CountTickets = 3,
                Price = sessionModels[1].PriceTicket * 3,
                DateBuy = DateTime.Now,
                SessionId = sessionModels[1].Id,
                Session = sessionModels[1]
            });
            ticketModels.Add(new TicketModel
            {
                Id = Guid.NewGuid().ToString(),
                CountTickets = 3,
                Price = sessionModels[2].PriceTicket * 3,
                DateBuy = DateTime.Now,
                SessionId = sessionModels[2].Id,
                Session = sessionModels[2]
            });
            ticketModels.Add(new TicketModel
            {
                Id = Guid.NewGuid().ToString(),
                CountTickets = 3,
                Price = sessionModels[2].PriceTicket * 3,
                DateBuy = DateTime.Now,
                SessionId = sessionModels[2].Id,
                Session = sessionModels[2]
            });
        }
    }
}
