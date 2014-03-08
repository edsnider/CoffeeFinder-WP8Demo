using CoffeeFinder.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeFinder.Services
{
    public class FoursquareService
    {
        #region Foursquare API client props

        private readonly string _CLIENT_ID = "";
        private readonly string _CLIENT_SECRET = "";

        #endregion

        public async Task<VenuesResponse> GetVenues(string query, string lat, string lon)
        {
            var v = DateTime.Now.ToString("yyyyMMdd");

            var ll = string.Format("{0},{1}", lat, lon);

            var url = string.Format(@"https://api.foursquare.com/v2/venues/search?ll={0}&query={1}&client_id={2}&client_secret={3}&v={4}", ll, query, _CLIENT_ID, _CLIENT_SECRET, v);

            var client = new HttpClient();
            var response = await client.GetStringAsync(url);

            JObject o = JObject.Parse(response);

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<VenuesResponse>(o["response"].ToString()));
        }
    }
}
