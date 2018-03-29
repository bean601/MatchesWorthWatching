using MatchWorthWatchingService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchInterestCalculator
{
	public class InterestCalculator : IInterestCalculator
	{
		public InterestLevel CalculateMatchInterest(MatchStatsEntity match)
		{
			//TODO: there's got to be a way to the comparison and assignment if higher value simpler
			var interest = InterestLevel.Unknown;

			//total score
			var totalScoreInterest = CalculateTotalScoreInterest(match);
			if (totalScoreInterest > interest)
			{
				interest = totalScoreInterest;
			}

			//red cards
			var redCardInterest = CalculateRedCardInterest(match);
			if (redCardInterest > interest)
			{
				interest = redCardInterest;
			}

			//shots on goal
			var shotsOnGoalInterest = CalculateShotsOnGoalInterest(match);
			if (shotsOnGoalInterest > interest)
			{
				interest = shotsOnGoalInterest;
			}

			//shots
			var shotsInterest = CalculateShotsInterest(match);
			if (shotsInterest > interest)
			{
				interest = shotsInterest;
			}

			//possession
			var possessionInterest = CalculatePossessionInterest(match);
			if (possessionInterest > interest)
			{
				interest = possessionInterest;
			}

			return interest;
		}

		private InterestLevel CalculateTotalScoreInterest(MatchStatsEntity match)
		{
			var interest = InterestLevel.Interesting;

			if (match.HomeScore + match.AwayScore >= 7)
			{
				interest = InterestLevel.Exciting;
			}
			else if (match.HomeScore + match.AwayScore >= 3)
			{
				interest = InterestLevel.Interesting;
			}
			else if (match.HomeScore + match.AwayScore >= 1)
			{
				interest = InterestLevel.Boring;
			}
			else if (match.HomeScore + match.AwayScore == 0)
			{
				interest = InterestLevel.VeryBoring;
			}

			return interest;
		}

		private InterestLevel CalculateRedCardInterest(MatchStatsEntity match)
		{
			var interest = InterestLevel.Unknown;

			if (match.HomeRedCards + match.AwayRedCards > 1)
			{
				interest = InterestLevel.Interesting;
			}
			else if (match.HomeRedCards + match.AwayRedCards > 2)
			{
				interest = InterestLevel.Exciting;
			}

			return interest;
		}

		private InterestLevel CalculateShotsOnGoalInterest(MatchStatsEntity match)
		{
			var interest = InterestLevel.Unknown;

			if (match.HomeShotsOnGoal > 3)
			{
				interest = InterestLevel.Boring;
			}


			return interest;
		}

		private InterestLevel CalculateShotsInterest(MatchStatsEntity match)
		{
			var interest = InterestLevel.Unknown;

			return interest;
		}

		private InterestLevel CalculatePossessionInterest(MatchStatsEntity match)
		{
			var interest = InterestLevel.Unknown;


			return interest;
		}
	}
}
