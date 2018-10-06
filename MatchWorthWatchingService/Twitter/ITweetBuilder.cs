using MatchWorthWatchingService.Common;

namespace MatchWorthWatchingService.Twitter
{
	public interface ITweetBuilder
	{
		string BuildTweet(InterestLevel matchInterestLevel, MatchEntity match);
	}
}
