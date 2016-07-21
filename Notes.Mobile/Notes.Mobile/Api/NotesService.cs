using Newtonsoft.Json;
using Notes.Mobile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Mobile.Api
{
    public class NotesService
    {
        private const string BaseUrl = "http://192.168.227.128/Notes.WebApi/";
        HttpClient client;

        public NotesService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            client.MaxResponseContentBufferSize = 256000;
        }

        public async  Task<List<Note>> GetNotes()
        {
            var uri = new Uri(BaseUrl + "notes");

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Note>>(content);
                return result;
            }else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception("API exception: " + message);
            }
        }

    }
}
