using MatchWorthWatchingService.Common;

namespace MatchesWorthWatching.Common
{
	public interface IInterestCalculator
	{
		InterestLevel CalculateMatchInterest(MatchStatsEntity match);
	}
}
