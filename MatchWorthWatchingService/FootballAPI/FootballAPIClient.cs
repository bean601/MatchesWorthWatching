﻿using MatchWorthWatchingService.FootballAPI.Contracts;
using RestSharp;
using System.Collections.Generic;
using System.Configuration;
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

			var request = new RestRequest("matches/{matchId}?Authorization={authToken}", Method.GET);
			request.AddParameter("matchId", matchId, ParameterType.UrlSegment);

			return ExecuteCall<MatchContract>(request);
		}

		public List<MatchContract> GetMatches(int competitionId, string startDate, string endDate)
		{
			var matches = new List<MatchContract>();

			var request = new RestRequest("matches?Authorization={authToken}", Method.GET);
			request.AddParameter("comp_id", competitionId);
			request.AddParameter("from_date", startDate);
			request.AddParameter("to_date", endDate);

			return ExecuteCall<List<MatchContract>>(request);
		}

		public MatchCommentaryContract GetMatchCommentary(int competitionId, int matchId)
		{
			var matchCommentary = new MatchCommentaryContract();

			var request = new RestRequest("commentaries/{matchId}?Authorization={authToken}", Method.GET);
			request.AddParameter("matchId", matchId, ParameterType.UrlSegment);
			request.AddParameter("comp_id", competitionId);

			return ExecuteCall<MatchCommentaryContract>(request);
		}

		private T ExecuteCall<T>(RestRequest request)
		{
			var client = new RestClient(GetAPIEndpoint());
			var returnObject = default(T);

			request.AddParameter("authToken", GetAuthToken(), ParameterType.UrlSegment);

			var response = client.Execute(request);

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var deserializedResponse = new RestResponse
				{
					Content = response.Content
				};

				returnObject = new JsonDeserializer().Deserialize<T>(deserializedResponse);
			}

			return returnObject;
		}
	}
}
