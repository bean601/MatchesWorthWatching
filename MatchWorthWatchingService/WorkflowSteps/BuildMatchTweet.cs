using MatchWorthWatchingService.Common;
using MatchWorthWatchingService.Logging;
using MatchWorthWatchingService.Twitter;
using System;

namespace MatchWorthWatchingService.WorkflowSteps
{
	/// <summary>
	/// Builds a tweet for a specific match
	/// <remarks>
	/// Inject Twitter service so we can mock it out for testing or have it log to Azure console before publishing
	/// </remarks>
	/// </summary>
	public class BuildMatchTweet
	{
		private ITweetBuilder _tweetBuilder;
		private ILogger _logger;

		public BuildMatchTweet(ILogger logger, ITweetBuilder tweetBuilder)
		{
			_logger = logger;
			_tweetBuilder = tweetBuilder;
		}

		public string Execute(ProcessedMatchEntity match)
		{
			string tweet = null;
			try
			{
				_logger.LogMessage(string.Format("{0} execution started - {1}", this.GetType().Name, DateTime.Now));

				tweet = _tweetBuilder.BuildTweet(match.MatchInterest, match.Match);

				_logger.LogMessage(string.Format("Tweet built with message - {0} on {1}", tweet, DateTime.Now));
				_logger.LogMessage(string.Format("{0} finished successfully - {1}", this.GetType().Name, DateTime.Now));
			}
			catch (Exception ex)
			{
				_logger.LogMessage(string.Format("{0} error occurred - {1}", this.GetType().Name, ex.Message), true);
			}

			return tweet;
		}
	}
}
