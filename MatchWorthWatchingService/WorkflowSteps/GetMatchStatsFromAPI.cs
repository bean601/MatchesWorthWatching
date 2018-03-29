using MatchWorthWatchingService.Common;
using MatchWorthWatchingService.FootballAPI;
using MatchWorthWatchingService.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchWorthWatchingService.WorkflowSteps
{
	public class GetMatchStatsFromAPI
	{
		private ILogger _logger;
		private IFootballAPIClient _api;

		public GetMatchStatsFromAPI(ILogger logger, IFootballAPIClient api)
		{
			_logger = logger;
			_api = api;
		}

		public MatchStatsEntity Execute(MatchEntity matchToCheck)
		{
			MatchStatsEntity matchStats = null;

			try
			{
				_logger.LogMessage(string.Format("{0} execution started - {1}", this.GetType().Name, DateTime.Now));
				_logger.LogMessage(string.Format("Calling API to get match stats for [{0}] vs [{1}] - {2}",
					matchToCheck.HomeTeam,
					matchToCheck.AwayTeam,
					DateTime.Now));

				var stats = _api.GetMatchCommentary(matchToCheck.CompetitionId, matchToCheck.MatchId);

				if (stats != null
					&& stats.match_stats != null
					&& stats.match_stats.localteam != null
					&& stats.match_stats.localteam.Any()
					&& stats.match_stats.visitorteam != null
					&& stats.match_stats.visitorteam.Any())
				{
					//TODO: implement a better mapper
					matchStats = new MatchStatsEntity
					{
						AwayCorners = stats.match_stats.visitorteam[0].corners,
						AwayPossesion = StringHelpers.ParsePossessionTime(stats.match_stats.visitorteam[0].possesiontime),
						AwayRedCards = stats.match_stats.visitorteam[0].redcards,
						AwaySaves = stats.match_stats.visitorteam[0].saves,
						AwayScore = StringHelpers.ParseAwayScore(matchToCheck.Score),
						AwayShots = stats.match_stats.visitorteam[0].shots_total,
						AwayShotsOnGoal = stats.match_stats.visitorteam[0].shots_onGoal,
						AwayYellowCards = stats.match_stats.visitorteam[0].yellowcards,
						HomeCorners = stats.match_stats.localteam[0].corners,
						HomePossesion = StringHelpers.ParsePossessionTime(stats.match_stats.localteam[0].possesiontime),
						HomeRedCards = stats.match_stats.localteam[0].redcards,
						HomeSaves = stats.match_stats.localteam[0].saves,
						HomeScore = StringHelpers.ParseHomeScore(matchToCheck.Score),
						HomeShots = stats.match_stats.localteam[0].shots_total,
						HomeShotsOnGoal = stats.match_stats.localteam[0].shots_onGoal,
						HomeYellowCards = stats.match_stats.localteam[0].yellowcards,
					};
				}

				_logger.LogMessage(string.Format("{0} finished successfully - {1}", this.GetType().Name, DateTime.Now));
			}
			catch (Exception ex)
			{
				_logger.LogMessage(string.Format("{0} error occurred - {1}", this.GetType().Name, ex.Message), true);
			}

			return matchStats;
		}
	}
}
