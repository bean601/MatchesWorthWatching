using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MatchWorthWatchingService.WorkflowSteps;

namespace MatchesWorthWatchingService.Test
{
	[TestClass]
	public class StringHelpersTest
	{
		[TestMethod]
		public void GivenNullPossessionTimeWhenParseIsCalledThenNegative1IsReturned()
		{
			var possessionTime = StringHelpers.ParsePossessionTime(null);

			Assert.IsNotNull(possessionTime);
			Assert.AreEqual(-1, possessionTime);
		}

		[TestMethod]
		public void GivenNegativePossessionTimeWhenParseIsCalledThenValidResultIsReturned()
		{
			var possessionTime = StringHelpers.ParsePossessionTime("-50%");

			Assert.IsNotNull(possessionTime);
			Assert.AreEqual(-50, possessionTime);
		}

		[TestMethod]
		public void GivenValidPossessionTimeWhenParseIsCalledThenValidResultIsReturned()
		{
			var possessionTime = StringHelpers.ParsePossessionTime("45%");

			Assert.IsNotNull(possessionTime);
			Assert.AreEqual(45, possessionTime);
		}

		[TestMethod]
		public void GivenValidPossessionTimeWithoutPercentWhenParseIsCalledThenValidResultIsReturned()
		{
			var possessionTime = StringHelpers.ParsePossessionTime("88");

			Assert.IsNotNull(possessionTime);
			Assert.AreEqual(88, possessionTime);
		}

		[TestMethod]
		public void GivenValidHomeScoreWhenParseIsCalledThenValidResultIsReturned()
		{
			var score = StringHelpers.ParseHomeScore("[5-3]");

			Assert.IsNotNull(score);
			Assert.AreEqual(5, score);
		}

		[TestMethod]
		public void GivenLargeValidHomeScoreWhenParseIsCalledThenValidResultIsReturned()
		{
			var score = StringHelpers.ParseHomeScore("[68465-3654]");

			Assert.IsNotNull(score);
			Assert.AreEqual(68465, score);
		}

		[TestMethod]
		public void GivenValidHomeScoreWhenParseIsCalledThenNegative1IsReturned()
		{
			var score = StringHelpers.ParseHomeScore("[?-?]");

			Assert.IsNotNull(score);
			Assert.AreEqual(-1, score);
		}

		[TestMethod]
		public void GivenNullHomeScoreWhenParseIsCalledThenNegative1IsReturned()
		{
			var score = StringHelpers.ParseHomeScore(null);

			Assert.IsNotNull(score);
			Assert.AreEqual(-1, score);
		}

		[TestMethod]
		public void GivenInvalidValidHomeScoreWhenParseIsCalledThenThenNegative1IsReturned()
		{
			var score = StringHelpers.ParseHomeScore("5-3");

			Assert.IsNotNull(score);
			Assert.AreEqual(-1, score);
		}

		[TestMethod]
		public void GivenValidAwayScoreWhenParseIsCalledThenValidResultIsReturned()
		{
			var score = StringHelpers.ParseAwayScore("[5-3]");

			Assert.IsNotNull(score);
			Assert.AreEqual(3, score);
		}

		[TestMethod]
		public void GivenLargeValidAwayScoreWhenParseIsCalledThenValidResultIsReturned()
		{
			var score = StringHelpers.ParseAwayScore("[645-7963]");

			Assert.IsNotNull(score);
			Assert.AreEqual(7963, score);
		}

		[TestMethod]
		public void GivenValidAwayScoreWhenParseIsCalledThenNegative1IsReturned()
		{
			var score = StringHelpers.ParseAwayScore("[?-?]");

			Assert.IsNotNull(score);
			Assert.AreEqual(-1, score);
		}

		[TestMethod]
		public void GivenNullAwayScoreWhenParseIsCalledThenNegative1IsReturned()
		{
			var score = StringHelpers.ParseAwayScore(null);

			Assert.IsNotNull(score);
			Assert.AreEqual(-1, score);
		}

		[TestMethod]
		public void GivenInvalidValidAwayScoreWhenParseIsCalledThenThenNegative1IsReturned()
		{
			var score = StringHelpers.ParseAwayScore("5-3");

			Assert.IsNotNull(score);
			Assert.AreEqual(-1, score);
		}

		[TestMethod]
		public void GivenValidUKFormattedDateAndValidTimeThenValidDateTimeIsReturned()
		{
			var dateTime = StringHelpers.CombineUKFormattedDateWithTime("22.02.1987", new DateTime(2015, 11, 22, 16, 20, 00));

			Assert.IsNotNull(dateTime);
			Assert.AreEqual(new DateTime(1987, 2, 22, 16, 20, 00), dateTime);
		}

		[TestMethod]
		public void GivenInvalidUKFormattedDateAndValidTimeThenValidMinDateTimeIsReturned()
		{
			var dateTime = StringHelpers.CombineUKFormattedDateWithTime("thisIsn'tRight", new DateTime(2015, 11, 22, 16, 20, 00));

			Assert.IsNotNull(dateTime);
			Assert.AreEqual(DateTime.MinValue, dateTime);
		}

		[TestMethod]
		public void GivenInvalidUKFormattedDateAndValidTimeThenValidMinDateTimeIsReturned2()
		{
			var dateTime = StringHelpers.CombineUKFormattedDateWithTime(".2...2..", new DateTime(2015, 11, 22, 16, 20, 00));

			Assert.IsNotNull(dateTime);
			Assert.AreEqual(DateTime.MinValue, dateTime);
		}

		[TestMethod]
		public void GivenNullUKFormattedDateAndValidTimeThenValidMinDateTimeIsReturned()
		{
			var dateTime = StringHelpers.CombineUKFormattedDateWithTime(null, DateTime.MaxValue);

			Assert.IsNotNull(dateTime);
			Assert.AreEqual(DateTime.MinValue, dateTime);
		}
	}
}
