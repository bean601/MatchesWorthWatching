using MatchWorthWatchingService.Common;
using MatchWorthWatchingService.FootballAPI;
using MatchWorthWatchingService.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchWorthWatchingService.WorkflowSteps
{
	public class GetMatchesFromAPI
	{
		private ILogger _logger;
		private IFootballAPIClient _api;

		public GetMatchesFromAPI(ILogger logger, IFootballAPIClient api)
		{
			_logger = logger;
			_api = api;
		}

		public Queue<MatchEntity> Execute(int competitionId, DateTime startDate, DateTime endDate)
		{
			Queue<MatchEntity> matchesToProcess = new Queue<MatchEntity>();

			try
			{
				_logger.LogMessage(string.Format("{0} execution started - {1}", this.GetType().Name, DateTime.Now));

				//pull the day's matches from the API
				var matchesForTheDay = _api.GetMatches(competitionId,
					startDate.ToShortDateString(),
					endDate.ToShortDateString());

				if (matchesForTheDay != null && matchesForTheDay.Any())
				{
					matchesToProcess = new Queue<MatchEntity>();
				}

				//TODO: implementing a better mapper
				foreach (var match in matchesForTheDay)
				{
					matchesToProcess.Enqueue(new MatchEntity
						{
							CompetitionId = competitionId,
							HomeTeam = match.localteam_name,
							AwayTeam = match.visitorteam_name,
							MatchId = match.id,
							Season = match.season,
							StartTime = StringHelpers.CombineUKFormattedDateWithTime(match.formatted_date, match.time),
							TweetSent = false,
							Score = match.ft_score //can be null if the match hasn't ended up the commentary API doesn't have score for some reason
						});
				}

				_logger.LogMessage(string.Format("API for day's matches successfully called returning {0} matches - {1}", matchesToProcess != null ? matchesToProcess.Count : 0, DateTime.Now));
				_logger.LogMessage(string.Format("{0} finished successfully - {1}", this.GetType().Name, DateTime.Now));
			}
			catch (Exception ex)
			{
				_logger.LogMessage(string.Format("{0} error occurred - {1}", this.GetType().Name, ex.Message), true);
			}

			return matchesToProcess;
		}
	}
}
