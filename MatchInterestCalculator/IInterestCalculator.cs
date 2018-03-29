using MatchWorthWatchingService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchInterestCalculator
{
	public interface IInterestCalculator
	{
		InterestLevel CalculateMatchInterest(MatchStatsEntity match);
	}
}
