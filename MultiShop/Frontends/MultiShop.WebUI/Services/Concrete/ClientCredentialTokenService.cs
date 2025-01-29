using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;
using Microsoft.Extensions.Options;
using IdentityModel.Client;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using IdentityModel.AspNetCore.AccessTokenManagement;


namespace MultiShop.WebUI.Services.Concrete
{
	public class ClientCredentialTokenService : IClientCredentialTokenService
	{
		private readonly ServiceApiSettings _serviceApiSettings;
		private readonly HttpClient _httpClient;
		private readonly IClientAccessTokenCache _clientAccessTokenCache;
		private readonly ClientSettings _clientSettings;
		public ClientCredentialTokenService(IOptions<ServiceApiSettings> serviceApiSettings,
			IOptions<ClientSettings> clientSettings, HttpClient httpClient, IClientAccessTokenCache clientAccessTokenCache)
		{
			_serviceApiSettings = serviceApiSettings.Value;
			_clientSettings = clientSettings.Value;
			_httpClient = httpClient;
			_clientAccessTokenCache = clientAccessTokenCache;
		}

		public async Task<string> GetToken()
		{
			var token1 = await _clientAccessTokenCache.GetAsync("multishoptoken");
			if(token1 != null)
			{
				return token1.AccessToken;
			}

			var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest // http olduğu üçün https ləğv etdik
			{
				Address = _serviceApiSettings.IdentityServerUrl,
				Policy = new DiscoveryPolicy
				{
					RequireHttps = false
				}
			});

			var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
			{
				ClientId = _clientSettings.MultiShopVisitorClient.ClientId,
				ClientSecret = _clientSettings.MultiShopVisitorClient.ClientSecret,
				Address = discoveryEndPoint.TokenEndpoint
			};

			var token2 = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);
			await _clientAccessTokenCache.SetAsync("multishoptoken", token2.AccessToken, token2.ExpiresIn);
			return token2.AccessToken;
		}
	}
}
