using MatchesWorthWatching.Common;
using MatchWorthWatchingService.Common;

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
			var interest = InterestLevel.VeryBoring;

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

			return interest;
		}

		private InterestLevel CalculateRedCardInterest(MatchStatsEntity match)
		{
			var interest = InterestLevel.VeryBoring;

			if (match.HomeRedCards + match.AwayRedCards > 2)
			{
				interest = InterestLevel.Exciting;
			}
			else if (match.HomeRedCards + match.AwayRedCards > 1)
			{
				interest = InterestLevel.Interesting;
			}

			return interest;
		}

		private InterestLevel CalculateShotsOnGoalInterest(MatchStatsEntity match)
		{
			var interest = InterestLevel.VeryBoring;

			if (match.HomeShotsOnGoal > 10 || match.AwayShotsOnGoal > 10)
			{
				interest = InterestLevel.Exciting;
			}
			else if (match.HomeShotsOnGoal > 6 || match.AwayShotsOnGoal > 6)
			{
				interest = InterestLevel.Interesting;
			}
			else if (match.HomeShotsOnGoal > 2 || match.AwayShotsOnGoal > 2)
			{
				interest = InterestLevel.Boring;
			}

			return interest;
		}

		private InterestLevel CalculateShotsInterest(MatchStatsEntity match)
		{
			var interest = InterestLevel.VeryBoring;

			if (match.HomeShots > 20 || match.AwayShots > 20)
			{
				interest = InterestLevel.Exciting;
			}
			else if (match.HomeShots > 16 || match.AwayShots > 16)
			{
				interest = InterestLevel.Interesting;
			}
			else if (match.HomeShots > 4 || match.AwayShots > 4)
			{
				interest = InterestLevel.Boring;
			}

			return interest;
		}

		//TODO: not sure how much this impacts interest... revisit this
		private InterestLevel CalculatePossessionInterest(MatchStatsEntity match)
		{
			var interest = InterestLevel.VeryBoring;

			if (match.HomePossesion > 80 || match.AwayPossesion > 80)
			{
				interest = InterestLevel.Exciting;
			}

			return interest;
		}
	}
}
