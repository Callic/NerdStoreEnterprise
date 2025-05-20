using System.Net.Http.Headers;

namespace NSE.WebApp.MVC.Extensions
{
    public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IUser _user;

        public HttpClientAuthorizationDelegatingHandler(IUser user)
        {
            _user = user;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorizationHeader = _user.ObterHttpContext().Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                request.Headers.Add("Authorization", new List<string> { authorizationHeader! });
            }

            if (_user.EstaAutenticado())
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _user.ObterUserToken());
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
