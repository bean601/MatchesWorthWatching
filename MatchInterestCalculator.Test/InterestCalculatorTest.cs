using Microsoft.VisualStudio.TestTools.UnitTesting;
using MatchWorthWatchingService.Common;
using MatchesWorthWatching.Common;

namespace MatchInterestCalculator.Test
{
	[TestClass]
	public class MatchInterestCalculatorTest
	{
		private IInterestCalculator _interestCalculator;

		[TestInitialize]
		public void Init()
		{
			_interestCalculator = new InterestCalculator();
		}

		[TestMethod]
		public void GivenHighScoringGameTheCorrectInterestLevelIsReturned()
		{
			var matchStats = new MatchStatsEntity
			{
				HomeScore = 15,
				AwayScore = 15
			};

			var interest = _interestCalculator.CalculateMatchInterest(matchStats);

			Assert.AreEqual(interest, InterestLevel.Exciting);
		}
	}
}
