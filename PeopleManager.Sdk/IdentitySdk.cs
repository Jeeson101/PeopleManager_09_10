using System.Net.Http.Json;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;

namespace PeopleManager.Sdk
{
	public class IdentitySdk(IHttpClientFactory httpClientFactory)
	{
		private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

		public async Task<AuthenticationResult> SignIn(SignInRequest request)
		{
			var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");

			var route = "Identity/sign-in";
			var response = await httpClient.PostAsJsonAsync(route, request);

			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();
			if (result is null)
			{
				return new AuthenticationResult();
			}

			return result;
		}

		public async Task<AuthenticationResult> Register(RegisterRequest request)
		{
			var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");

			var route = "Identity/register";
			var response = await httpClient.PostAsJsonAsync(route, request);

			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();
			if (result is null)
			{
				return new AuthenticationResult();
			}

			return result;
		}
	}
}
