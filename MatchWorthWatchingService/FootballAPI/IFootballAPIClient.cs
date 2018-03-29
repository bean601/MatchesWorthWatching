using MatchWorthWatchingService.FootballAPI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchWorthWatchingService.FootballAPI
{
	public interface IFootballAPIClient
	{
		MatchContract GetMatch(int matchId);
		List<MatchContract> GetMatches(int competitionId, string startDate, string endDate);
		MatchCommentaryContract GetMatchCommentary(int competitionId, int matchId);
	}
}
