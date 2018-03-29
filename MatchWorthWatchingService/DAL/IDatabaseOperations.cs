using System;
using System.Collections.Generic;
using MatchWorthWatchingService.Common;

namespace MatchWorthWatchingService.DAL
{
	public interface IDatabaseOperations
	{
		DateTime GetLastMatchCheckTime();
		IEnumerable<MatchEntity> GetMatchesToCheck();
		void UpdateMatchTwitterStatus(int matchId, bool tweetSent, InterestLevel matchInterest);
		void PersistMatchesToDatabase(IEnumerable<MatchEntity> matches);
	}
}
