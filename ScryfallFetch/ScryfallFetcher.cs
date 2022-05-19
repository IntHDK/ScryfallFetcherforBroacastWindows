using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace ScryfallFetch
{
    public class ScryfallFetcher : IDisposable
    {
        private const string ScryfallAPIPrefix = "https://api.scryfall.com/";
        private readonly HttpClient client;
        public ScryfallFetcher()
        {
            client = new HttpClient();
        }
        public void Dispose()
        {
            client.Dispose();
        }

        public async Task<CardObject?> FetchNamed(string keyword)
        {
            var uribuilder = new UriBuilder(ScryfallAPIPrefix);
            uribuilder.Path = "/cards/named";
            var querybuilder = HttpUtility.ParseQueryString(string.Empty);
            querybuilder["fuzzy"] = keyword;
            uribuilder.Query = querybuilder.ToString();
            string? fetchstr;
            ParentObject parent;
            try
            {
                fetchstr = await client.GetStringAsync(uribuilder.ToString());
                parent = JsonSerializer.Deserialize<ParentObject>(fetchstr);
            }
            catch
            {
                return null;
            }
            if ((parent ?? new CardObject()).Object == "card")
            {
                var result_card = JsonSerializer.Deserialize<CardObject>(fetchstr);
                return result_card;
            }
            else
            {
                return null;
            }
        }
    }

}
