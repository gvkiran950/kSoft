using kSoft.Core.Contracts.Repository;
using kSoft.Core.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Polly;
using System.Diagnostics;

namespace kSoft.Core.Repository
{
    public class KSoftRepository : IKSoftRepository
    {
        public Task DeleteAsync(string uri, string authToken = null)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAsync<T>(string uri, string authToken = null)
        {
            try
            {
                string jsonResult = string.Empty;
                using (HttpClient httpClient = CreateHttpClient(authToken))
                {
                    var response = await Policy
                        .Handle<WebException>(ex => true)
                        .WaitAndRetryAsync
                        (
                            5,
                            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                        )
                        .ExecuteAsync(async () => await httpClient.GetAsync(uri));


                    // await httpClient.GetAsync(uri);

                    if (response.IsSuccessStatusCode)
                    {
                        jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var json = JsonConvert.DeserializeObject<T>(jsonResult);
                        return json;
                    }

                    if (response.StatusCode == HttpStatusCode.Forbidden ||
                        response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new ServiceAuthenticationException(jsonResult);
                    }

                    throw new CustomHttpRequestException(response.StatusCode, jsonResult);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> PostAsync<T>(string uri, T data, string authToken = null)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                string jsonResult = string.Empty;

                using (HttpClient httpClient = CreateHttpClient(authToken))
                {
                    var response = await Policy
                        .Handle<WebException>(ex => true)
                        .WaitAndRetryAsync
                        (
                            5,
                            retryAttempts => TimeSpan.FromSeconds(Math.Pow(2, retryAttempts))
                        )
                        .ExecuteAsync(async () => await httpClient.PostAsync(uri, content));
                        

                    if (response.IsSuccessStatusCode)
                    {
                        jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var json = JsonConvert.DeserializeObject<T>(jsonResult);
                        return json;
                    }

                    if (response.StatusCode == HttpStatusCode.Forbidden ||
                        response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new ServiceAuthenticationException(jsonResult);
                    }

                    throw new CustomHttpRequestException(response.StatusCode, jsonResult);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<R> PostAsync<T, R>(string uri, T data, string authToken = null)
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(uri);
                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                string jsonResult = string.Empty;
                var response = await httpClient.PostAsJsonAsync(uri, data);
                if (response.IsSuccessStatusCode)
                {
                    jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<R>(jsonResult);
                    return json;
                }

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new CustomHttpRequestException(response.StatusCode, jsonResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<T> PutAsync<T>(string uri, T data, string authToken = null)
        {
            throw new NotImplementedException();
        }

        private HttpClient CreateHttpClient(string authToken)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(authToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            }

            return httpClient;
        }
    }
}
