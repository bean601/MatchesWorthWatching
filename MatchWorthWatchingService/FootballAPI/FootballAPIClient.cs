using MatchWorthWatchingService.FootballAPI.Contracts;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MatchWorthWatchingService.Common;
using RestSharp.Deserializers;

namespace MatchWorthWatchingService.FootballAPI
{
	public class FootballAPIClient : IFootballAPIClient
	{
		private string GetAuthToken()
		{
			return ConfigurationManager.AppSettings["FootballAPIAuthToken"];
		}

		private string GetAPIEndpoint()
		{
			return ConfigurationManager.AppSettings["FootballAPIUrl"];
		}

		public MatchContract GetMatch(int matchId)
		{
			var match = new MatchContract();

			var client = new RestClient(GetAPIEndpoint());
			var request = new RestRequest("matches/{matchId}?Authorization={authToken}", Method.GET);
			request.AddParameter("authToken", GetAuthToken(), ParameterType.UrlSegment);
			request.AddParameter("matchId", matchId, ParameterType.UrlSegment);

			var response = client.Execute(request);

			if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
			{
				var deserializedResponse = new RestResponse
				{
					Content = response.Content
				};

				match = new JsonDeserializer().Deserialize<MatchContract>(deserializedResponse);
			}

			return match;
		}

		public List<MatchContract> GetMatches(int competitionId, string startDate, string endDate)
		{
			var matches = new List<MatchContract>();

			var client = new RestClient(GetAPIEndpoint());
			var request = new RestRequest("matches?Authorization={authToken}", Method.GET);
			request.AddParameter("authToken", GetAuthToken(), ParameterType.UrlSegment);
			request.AddParameter("comp_id", competitionId);
			request.AddParameter("from_date", startDate);
			request.AddParameter("to_date", endDate);

			var response = client.Execute(request);

			if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
			{
				var deserializedResponse = new RestResponse
				{
					Content = response.Content
				};

				matches = new JsonDeserializer().Deserialize<List<MatchContract>>(deserializedResponse);
			}

			return matches;
		}

		public MatchCommentaryContract GetMatchCommentary(int competitionId, int matchId)
		{
			var matchCommentary = new MatchCommentaryContract();

			var client = new RestClient(GetAPIEndpoint());
			var request = new RestRequest("commentaries/{matchId}?Authorization={authToken}", Method.GET);
			request.AddParameter("authToken", GetAuthToken(), ParameterType.UrlSegment);
			request.AddParameter("matchId", matchId, ParameterType.UrlSegment);
			request.AddParameter("comp_id", competitionId);

			var response = client.Execute(request);

			if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
			{
				var deserializedResponse = new RestResponse
				{
					Content = response.Content
				};

				matchCommentary = new JsonDeserializer().Deserialize<MatchCommentaryContract>(deserializedResponse);
			}

			return matchCommentary;
		}
	}
}
