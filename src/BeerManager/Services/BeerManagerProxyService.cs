using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace BeerManager.Services
{
    public class BeerManagerProxyService : IBeerManagerProxyService
    {
        private const string Url = "http://api.brewerydb.com/v2/beers";
        private const string Key = "58d7136df833e48279ecfb4bfc1503b2";

        public async Task<string> GetBeers(string order, bool reverse, string name, int page)
        {
            var parameters = new Dictionary<string, string> ();
            parameters.Add("key", Key);
            parameters.Add("order", order);
            parameters.Add("p", page.ToString());
            parameters.Add("sort", reverse ? "DESC" : "ASC");
            if (!String.IsNullOrEmpty(name))
            {
                parameters.Add("name", name);
            }

            var uri = QueryHelpers.AddQueryString(Url, parameters);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return "asd";
                }
            }
        }
    }
}