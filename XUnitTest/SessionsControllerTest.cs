using BuyingTicketCore;
using BuyingTicketCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System;
using System.Text;
using System.Text.Json;
using Xunit;
using System.Net.Http.Headers;
using System.Net;

namespace XUnitTest
{
    public class SessionsControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public SessionsControllerTest()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
            _client.DefaultRequestHeaders.Add("ContentType", "application/json");

            // Не сделал тестирование с помощью moc, потому что тестирую именно подключение к базе данных. 
            // И тестирование функционала
        }

        [Fact]
        public async void GetAndDeleteSessions()
        {
            var response = await _client.GetAsync("/api/SessionModels");
            var sessions = JArray.Parse(await response.Content.ReadAsStringAsync());

            foreach (var session in sessions)
            {
                Assert.NotNull(session.Value<string>("room"));
                Assert.NotNull(session.Value<int>("numberSeats"));
                Assert.NotNull(session.Value<string>("nameFilm"));
                Assert.NotNull(session.Value<double>("priceTicket"));
                Assert.NotNull(session.Value<string>("room"));
                Assert.NotNull(session.Value<JArray>("tickets"));

                JObject jObject = new JObject();
                jObject["id"] = session.Value<string>("id");

                var responseDel = await _client.PostAsync("/api/SessionModels/Delete/" + session.Value<string>("id"),
                    new StringContent("", Encoding.UTF8, "application/json"));
                Assert.Equal(responseDel.StatusCode, HttpStatusCode.OK);
            }

        }

        [Fact]
        public async void CreateAndChangeSession()
        {
            string nameFilm = "Крутой и еще круче";
            string room = "Vip комната";
            int numberSeats = 15;
            DateTime startTime = new DateTime(2020, 10, 28, 15, 0, 0);
            double priceTicket = 125.8;

            JObject jObject = new JObject();
            jObject.Add("nameFilm", nameFilm);
            jObject.Add("numberSeats", numberSeats);
            jObject.Add("priceTicket", priceTicket);
            jObject.Add("startFilm", startTime);
            jObject.Add("room", room);

            var response = await _client.PostAsync("/api/SessionModels/Create", 
                new StringContent(jObject.ToString(), Encoding.UTF8, "application/json"));
            var session = JObject.Parse(await response.Content.ReadAsStringAsync());

            Assert.NotEqual(session.Value<int>("status"), 415);

            Assert.Equal(session.Value<string>("room"), room);
            Assert.Equal(session.Value<int>("numberSeats"), numberSeats);
            Assert.Equal(session.Value<string>("nameFilm"), nameFilm);
            Assert.Equal(session.Value<double>("priceTicket"), priceTicket);
            Assert.Equal(session.Value<DateTime>("startFilm"), startTime);
            Assert.Equal(session.Value<JArray>("tickets").Count, 0);

            nameFilm = "Новое название";
            room = "Vip2 комната";
            numberSeats = 10;
            startTime = new DateTime(2020, 10, 25, 17, 0, 0);
            priceTicket = 1225.8;

            session["nameFilm"] = nameFilm;
            session["numberSeats"] = numberSeats;
            session["priceTicket"] = priceTicket;
            session["startFilm"] = startTime;
            session["room"] = room;

            var responseUpd = await _client.PostAsync("/api/SessionModels/" + session.Value<string>("id"), 
                new StringContent(session.ToString(), Encoding.UTF8, "application/json"));
            var sessionUpd = JObject.Parse(await responseUpd.Content.ReadAsStringAsync());

            Assert.Equal(sessionUpd.Value<string>("room"), room);
            Assert.Equal(sessionUpd.Value<int>("numberSeats"), numberSeats);
            Assert.Equal(sessionUpd.Value<string>("nameFilm"), nameFilm);
            Assert.Equal(sessionUpd.Value<double>("priceTicket"), priceTicket);
            Assert.Equal(sessionUpd.Value<DateTime>("startFilm"), startTime);
            Assert.Equal(sessionUpd.Value<JArray>("tickets").Count, 0);
        }


        [Fact]
        public async void BuyAndDeleteTicket()
        {
            var response = await _client.GetAsync("/api/SessionModels");
            Assert.Equal(response.StatusCode, HttpStatusCode.OK);
            var sessions = JArray.Parse(await response.Content.ReadAsStringAsync());

            foreach (var session in sessions)
            {
                Assert.NotNull(session.Value<string>("room"));
                Assert.NotNull(session.Value<int>("numberSeats"));
                Assert.NotNull(session.Value<string>("nameFilm"));
                Assert.NotNull(session.Value<double>("priceTicket"));
                Assert.NotNull(session.Value<string>("room"));
                Assert.NotNull(session.Value<JArray>("tickets"));

                var countOccupiedSeats = 0;
                foreach(JObject ticket in session.Value<JArray>("tickets"))
                {
                    countOccupiedSeats += ticket.Value<int>("countTickets");
                }

                JObject jObject = new JObject();

                jObject["sessionId"] = session.Value<string>("id");
                jObject["countTickets"] = session.Value<int>("numberSeats") - countOccupiedSeats + 1;

                var responseBuyFalse = await _client.PostAsync("/api/Ticket",
                    new StringContent(jObject.ToString(), Encoding.UTF8, "application/json"));
                Assert.NotEqual(responseBuyFalse.StatusCode, HttpStatusCode.OK);


                jObject["countTickets"] = session.Value<int>("numberSeats") - countOccupiedSeats - 1;
                if (jObject.Value<int>("countTickets") <= 0)
                {
                    continue;
                }

                var responseBuyTrue = await _client.PostAsync("/api/Ticket",
                    new StringContent(jObject.ToString(), Encoding.UTF8, "application/json"));
                Assert.Equal(responseBuyTrue.StatusCode, HttpStatusCode.OK);
                var ticketJobject = JObject.Parse(await responseBuyTrue.Content.ReadAsStringAsync());

                var responseDelete = await _client.PostAsync("/api/Ticket/Delete/" + ticketJobject.Value<string>("id"),
                    new StringContent("", Encoding.UTF8, "application/json"));
                Assert.Equal(responseDelete.StatusCode, HttpStatusCode.OK);
            }
        }
    }
}
