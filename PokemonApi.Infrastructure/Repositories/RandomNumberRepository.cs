using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace PokemonApi.Infrastructure.Respositories
{
    public class RandomNumberRepository : IRandomNumberRepository
    {
        public async Task<int?> Get()
        {
            var url = "http://www.randomnumberapi.com/api/v1.0/random";
            var rClient = new RestClient(url);
            var restRequest = new RestRequest();
            restRequest.AddHeader("Accept", "*/*");
            var restResult = await rClient.ExecuteAsync(restRequest);

            if (restResult.StatusCode == HttpStatusCode.OK && restResult.Content != null)
            {
                var number = JsonConvert.DeserializeObject<List<int>>(restResult.Content)?.FirstOrDefault();
                return number;
            }

            return null;
        }
    }
}
