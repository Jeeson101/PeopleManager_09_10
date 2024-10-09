using System.Net;

namespace PeopleManager.Ui.Mvc.Handlers
{
	public class RedirectToSignInHandler : DelegatingHandler
	{
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var response = await base.SendAsync(request, cancellationToken);

			if (response.StatusCode == HttpStatusCode.Forbidden ||
			    response.StatusCode == HttpStatusCode.Unauthorized)
			{
				//response.Headers.Location = new Uri("/Account/SignIn");
			}

			return response;
		}

	}
}
