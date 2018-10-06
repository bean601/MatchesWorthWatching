using MatchWorthWatchingService.Common;
using MatchWorthWatchingService.FootballAPI;
using MatchWorthWatchingService.Logging;
using System;

namespace MatchWorthWatchingService.WorkflowSteps
{
	public class GetMatchScoreFromAPI
	{
		private ILogger _logger;
		private IFootballAPIClient _api;

		public GetMatchScoreFromAPI(ILogger logger, IFootballAPIClient api)
		{
			_logger = logger;
			_api = api;
		}

		public MatchEntity Execute(MatchEntity matchToCheck)
		{
			MatchEntity match = matchToCheck;

			try
			{
				_logger.LogMessage(string.Format("{0} execution started - {1}", this.GetType().Name, DateTime.Now));
				_logger.LogMessage(string.Format("Calling API to get match full time score for *[{0}]* vs *[{1}]* - {2}",
					match.HomeTeam,
					match.AwayTeam,
					DateTime.Now));

				var stats = _api.GetMatch(match.MatchId);

				if (stats != null && stats.status.ToUpper() == "FT" && stats.ft_score != null)
				{
					match.Score = stats.ft_score;
				}

				_logger.LogMessage(string.Format("{0} finished successfully - {1}", this.GetType().Name, DateTime.Now));
			}
			catch (Exception ex)
			{
				_logger.LogMessage(string.Format("{0} error occurred - {1}", this.GetType().Name, ex.Message), true);
			}

			return match;
		}
	}
}
