using MatchWorthWatchingService.FootballAPI.Contracts;
using System.Collections.Generic;

namespace MatchWorthWatchingService.FootballAPI
{
	public interface IFootballAPIClient
	{
		MatchContract GetMatch(int matchId);
		List<MatchContract> GetMatches(int competitionId, string startDate, string endDate);
		MatchCommentaryContract GetMatchCommentary(int competitionId, int matchId);
	}
}
