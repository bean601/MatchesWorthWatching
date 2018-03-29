using MatchWorthWatchingService.Common;
using System;

namespace MatchWorthWatchingService.Twitter
{
	public interface ITweetBuilder
	{
		string BuildTweet(InterestLevel matchInterestLevel, MatchEntity match);
	}
}
