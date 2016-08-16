using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Mobile.Api
{
    public abstract class ApiServiceBase
    {
#if hardware
        private const string BaseUrl = "http://192.168.5.18/Notes.WebApi/";
#else
        private const string BaseUrl = "http://192.168.227.128/Notes.WebApi/";
#endif
        private HttpClient client;

        protected ApiServiceBase()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.MaxResponseContentBufferSize = 256000;
        }

        protected async Task<T> Get<T>(string path)
        {
            try
            {
                var uri = new Uri(BaseUrl + path);

                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(content);
                    return result;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception("API exception: " + message);
                }
            }
            catch (Exception ex)
            {
                //todo handle
                throw;
            }
        }

        protected async Task<TResult> Post<TCommand, TResult>(string path, TCommand command)
        {
            try { 
            var uri = new Uri(BaseUrl + path);

            var request = new StringContent(JsonConvert.SerializeObject(command));
            request.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(uri, request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TResult>(content);
                return result;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception("API exception: " + message);
                }
            }
            catch (Exception ex)
            {
                //todo handle
                throw;
            }
        }

        protected async Task Post<TCommand>(string path, TCommand command)
        {
            try
            {
                var uri = new Uri(BaseUrl + path);

                var request = new StringContent(JsonConvert.SerializeObject(command));
                request.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(uri, request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception("API exception: " + message);
                }
            }
            catch (Exception ex)
            {
                //todo handle
                throw;
            }
        }

        protected async Task<TResult> Put<TCommand, TResult>(string path, TCommand command)
        {
            try
            {
                var uri = new Uri(BaseUrl + path);

                var request = new StringContent(JsonConvert.SerializeObject(command));
                request.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PutAsync(uri, request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<TResult>(content);
                    return result;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception("API exception: " + message);
                }
            }catch(Exception ex)
            {
                //todo handle
                throw;
            }
        }

        protected async Task Put<TCommand>(string path, TCommand command)
        {
            try
            {
                var uri = new Uri(BaseUrl + path);

                var request = new StringContent(JsonConvert.SerializeObject(command));
                request.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PutAsync(uri, request);
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception("API exception: " + message);
                }
            }catch(Exception ex)
            {
                //todo
                throw;
            }
        }



    }
}
