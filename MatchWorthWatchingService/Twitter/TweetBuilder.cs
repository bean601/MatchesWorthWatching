using MatchWorthWatchingService.Common;

namespace MatchWorthWatchingService.Twitter
{
	public class TweetBuilder : ITweetBuilder
	{
		public string BuildTweet(InterestLevel matchInterestLevel, MatchEntity match)
		{
			return string.Format("Match between {0} (home) and {1} (away) was {2}",
				match.HomeTeam,
				match.AwayTeam,
				matchInterestLevel);
		}
	}
}
