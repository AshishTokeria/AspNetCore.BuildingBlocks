using IdentityModel.Client;

namespace AspNetCore.BuildingBlocks.Authentication.Client
{
    public class BaseIdentityServerTokenReceiver : IIdentityServerTokenReceiver
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly HttpClient _httpClient;
        private readonly string _identityUrl;
        private readonly string _scopeName;
        private TokenResponse _accessToken;
        private DateTime _accesstokenExpiresIn;

        public BaseIdentityServerTokenReceiver(string identityUrl, string clientSecret, 
            string clientId, string scopeName, HttpClient httpClient)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _httpClient = httpClient;
            _identityUrl = identityUrl;
            _scopeName = scopeName;
        }

        public async Task<string> GetTokenAsync()
        {
            return await FetchTokenIfNeeded().ConfigureAwait(false);
        }

        private async Task<string> FetchTokenIfNeeded()
        {
            if(_accessToken == null || HasTokenExpired())
            {
                ClientCredentialsTokenRequest tokenRequest= new ClientCredentialsTokenRequest
                {
                    Address = new Uri(new Uri(_identityUrl), "connect/token").ToString(),
                    ClientId = _clientId,
                    ClientSecret = _clientSecret,
                    Scope = _scopeName
                };

                _accessToken = await _httpClient
                    .RequestClientCredentialsTokenAsync(tokenRequest)
                    .ConfigureAwait(false);

                _accesstokenExpiresIn = DateTime.UtcNow.AddSeconds(_accessToken.ExpiresIn).AddSeconds(-30);
            }

            return _accessToken.AccessToken;
        }

        public bool HasTokenExpired()
        {
            return DateTime.UtcNow >= _accesstokenExpiresIn;
        }
    }
}
