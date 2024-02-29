using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebApp_Desafio_FrontEnd.ApiClients.Desafio_API
{
    public class BaseApiClient<T> : BaseClient
    {
        private const string tokenAutenticacao = "AEEFC184-9F62-4B3E-BB93-BE42BF0FFA36";
        private readonly string apiUrl;

        public BaseApiClient()
        {
            this.apiUrl = "https://localhost:44388/";
        }

        protected List<T> Listar(string endpoint)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", tokenAutenticacao }
            };

            var response = base.Get($"{apiUrl}{endpoint}", null, headers);
            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        protected T Obter(string endpoint, int id)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", tokenAutenticacao }
            };

            var query = new Dictionary<string, object>()
            {
                { "id", id }
            };

            var response = base.Get($"{apiUrl}{endpoint}", query, headers);
            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);
            return JsonConvert.DeserializeObject<T>(json);
        }

        protected bool Gravar(string endpoint, T body)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", tokenAutenticacao }
            };

            var response = base.Post($"{apiUrl}{endpoint}", body, headers);
            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);
            return JsonConvert.DeserializeObject<bool>(json);
        }

        protected bool Excluir(string endpoint, int id)
        {
            var headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", tokenAutenticacao }
            };

            var query = new Dictionary<string, object>()
            {
                { "id", id }
            };

            var response = base.Delete($"{apiUrl}{endpoint}", query, headers);
            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);
            return JsonConvert.DeserializeObject<bool>(json);
        }
    }
}
