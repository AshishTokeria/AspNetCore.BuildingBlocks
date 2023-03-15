using IdentityModel.Client;

namespace AspNetCore.BuildingBlocks.Authentication.Client
{
    public abstract class BaseAuthenticationHandler : DelegatingHandler
    {
        private readonly IIdentityServerTokenReceiver _tokenReceiver;

        protected BaseAuthenticationHandler(IIdentityServerTokenReceiver tokenReceiver, HttpClientHandler handler) : base(handler) 
        {
            _tokenReceiver = tokenReceiver ?? throw new ArgumentException(nameof(tokenReceiver));
        }

        protected BaseAuthenticationHandler(IIdentityServerTokenReceiver tokenReceiver)
        {
            _tokenReceiver = tokenReceiver ?? throw new ArgumentException(nameof(tokenReceiver));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token = await _tokenReceiver.GetTokenAsync().ConfigureAwait(false);
            request.SetBearerToken(token);
            return await base.SendAsync(request,cancellationToken).ConfigureAwait(false);
        }
    }
}
