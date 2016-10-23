using Newtonsoft.Json;
using Notes.Mobile.Events;
using Notes.Mobile.Exceptions;
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
        private class AuthTokenUpdatedEventArgs
        {
            public string AuthToken { get; set; }
        }
        private delegate void AuthTokenUpdatedEventHanlder(AuthTokenUpdatedEventArgs args);


#if hardware
        private const string BaseUrl = "http://192.168.5.18/Notes.WebApi/";
#else
        private const string BaseUrl = "http://192.168.227.128/Notes.WebApi/";
#endif
        private HttpClient client;

        private static string _authToken;

        private static event AuthTokenUpdatedEventHanlder AuthTokenUpdated;

        protected ApiServiceBase()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            AuthTokenUpdated += ApiServiceBase_AuthTokenUpdated;

            if (_authToken != null)
            {
                SetAuthToken(_authToken);
            }

            client.MaxResponseContentBufferSize = 256000;
        }

        private void ApiServiceBase_AuthTokenUpdated(AuthTokenUpdatedEventArgs args)
        {
            SetAuthToken(args.AuthToken);
        }

        private void SetAuthToken(string authToken)
        {
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", authToken);
        }

        protected static void Authorize(string authToken)
        {
            _authToken = authToken;

            AuthTokenUpdated(new Api.ApiServiceBase.AuthTokenUpdatedEventArgs
            {
                AuthToken = authToken
            });
        }

        protected async Task HandleUnsuccessStatuCode(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.Unauthorized:
                    throw new UnauthorizedAccessException();
                    break;
                default:
                    var message = await response.Content.ReadAsStringAsync();
                    throw new NotesApiException(message, response.StatusCode);
            }
        }

        protected async Task<T> Get<T>(string path)
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
                await HandleUnsuccessStatuCode(response);
                return default(T);
            }

        }

        protected async Task Delete(string path)
        {
            try
            {
                var uri = new Uri(BaseUrl + path);

                var response = await client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
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
