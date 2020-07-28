using BuyingTicketCore.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UserApplication.Helpers
{
    public class Request
    {
        private static string url = "https://localhost:44351";
        public static async Task<bool> SaveSession(SessionModelView session)
        {
            using (var client = new HttpClient())
            {
                var response = (await client.PostAsJsonAsync(url + "/api/SessionModels/Create", session));
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return false;
                }
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                SessionModel models = JsonConvert.DeserializeObject<SessionModel>(responseBody);
                return true;
            }
            return false;
        }
        public static async Task<bool> BuyTicket(string idSession, int countTicket)
        {
            using (var client = new HttpClient())
            {
                JObject ticket = new JObject();
                ticket.Add("sessionId", idSession);
                ticket.Add("countTickets", countTicket);
                var response = (await client.PostAsJsonAsync(url + "/api/Ticket", ticket));
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return false;
                }
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                TicketModel models = JsonConvert.DeserializeObject<TicketModel>(responseBody);
                return true;
            }
            return false;
        }
        public static async Task<List<SessionModel>> GetSession()
        {
            using (var client = new HttpClient())
            {
                var response = (await client.GetAsync(url + "/api/SessionModels"));
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                List<SessionModel> models = JsonConvert.DeserializeObject<List<SessionModel>>(responseBody);
                return models;
            }
            return null;
        }
    }
}
