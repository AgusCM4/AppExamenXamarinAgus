using AppExamenXamarinAgus.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AppExamenXamarinAgus.Services
{
    public class ServiceApiSeries
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiSeries(IConfiguration configuration)
        {
            this.UrlApi = configuration["ApiUrls:ApiDepartamentos"];
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri uri = new Uri(this.UrlApi + request);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);                
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Serie>> GetSeriesAsync()
        {
            string request = "/api/series";
            List<Serie> series = await this.CallApiAsync<List<Serie>>(request);
            return series;
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "/api/personajes";
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task<Serie> FindSerieAsync(int id)
        {
            string request = "/api/series/" + id;
            Serie serie = await this.CallApiAsync<Serie>(request);
            return serie;
        }

        private async Task<int> GetMaxIdPersonaje()
        {
            List<Personaje> personajes = await this.GetPersonajesAsync();
            return personajes.Max(x => x.IdPersonaje) + 1;
        }

        public async Task<List<Personaje>> GetPersonajesSeriesAsync(int idserie)
        {
            string request = "/api/series/personajesserie/" + idserie;
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task InsertPersonajeAsync(string nombre, int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje dept = new Personaje
                {
                    IdPersonaje = await this.GetMaxIdPersonaje(),
                    Nombre = nombre,
                    Imagen = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/65/Pac-Man_Cutscene.svg/283px-Pac-Man_Cutscene.svg.png",
                    IdSerie=idserie
                };
                string json = JsonConvert.SerializeObject(dept);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                string request = "/api/personajes";
                Uri uri = new Uri(this.UrlApi + request);
                await client.PostAsync(uri, content);
            }
        }

        public async Task ModificarPersonajeAsync(int idpersonaje, int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string request = "/api/personajes/"+idpersonaje+"/"+idserie;
                Uri uri = new Uri(this.UrlApi + request);
                await client.PutAsync(uri,null);
            }
        }

    }
}
