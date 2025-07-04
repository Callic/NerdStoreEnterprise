﻿using NSE.WebApp.MVC.Extensions;
using System.Text.Json;

namespace NSE.WebApp.MVC.Services
{
    public abstract class Service
    {
        public bool TratarRespostaErro(HttpResponseMessage httpResponse)
        {
            switch ((int) httpResponse.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case >=500:
                    throw new CustomHttpResponseException(httpResponse.StatusCode);
                case 400:
                    return false;
            }

            httpResponse.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<T>(content, opt)!;
        }

    }
}
