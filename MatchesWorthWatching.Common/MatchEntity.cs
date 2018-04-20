using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchWorthWatchingService.Common
{
	/// <summary>
	/// competitionID int|matchID int|homeTeam string|awayTeam string|startDate dateTime|season? string|tweetSent bool|InterestLevel string
	/// </summary>
	public class MatchEntity
	{
		public int MatchId { get; set; }
		public int CompetitionId { get; set; }
		public string HomeTeam { get; set; }
		public string AwayTeam { get; set; }
		public DateTime StartTime { get; set; }
		public string Season { get; set; }
		public bool TweetSent { get; set; }
		public string Score { get; set; }
		public string InterestLevel { get; set; }
	}
}
