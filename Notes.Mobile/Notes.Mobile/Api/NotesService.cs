using Newtonsoft.Json;
using Notes.Contract.Queries;
using Notes.Mobile.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Notes.Mobile.Infrastructure;
using System.Linq;
using Notes.Contract.Commands;

namespace Notes.Mobile.Api
{
    public class NotesService : INotesService
    {
#if hardware
        private const string BaseUrl = "http://192.168.5.18/Notes.WebApi/";
#else
        private const string BaseUrl = "http://192.168.227.128/Notes.WebApi/";
#endif
        private HttpClient client;

        public NotesService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<Note>> GetNotes()
        {
            var uri = new Uri(BaseUrl + "notes");

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetNotesQueryResult>(content);
                return result.Notes.Select(x => x.MapTo<Mobile.Model.Note>()).ToList();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception("API exception: " + message);
            }
        }

        public async Task<Note> Save(Note note)
        {
            var uri = new Uri(BaseUrl + "notes");
            var command = new SaveNoteCommand()
            {
                Note = note.MapTo<Contract.Model.Note>()
            };

            var request = new StringContent(JsonConvert.SerializeObject(command));
            request.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(uri, request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SaveNoteCommandResult>(content);
                return result.Note.MapTo<Note>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception("API exception: " + message);
            }
        }

        public async Task<Note> Get(Guid noteId)
        {
            throw new NotImplementedException();
        }
    }
}