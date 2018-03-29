using MatchWorthWatchingService.Common;
using MatchWorthWatchingService.Logging;
using MatchWorthWatchingService.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchWorthWatchingService.WorkflowSteps
{
	/// <summary>
	/// Sends a tweet for a specific match
	/// <remarks>
	/// Inject Twitter service so we can mock it out for testing or have it log to Azure console before publishing
	/// </remarks>
	/// </summary>
	public class SendMatchTweet
	{
		private ILogger _logger;
		private ITwitterService _twitterService;

		public SendMatchTweet(ILogger logger, ITwitterService twitterService)
		{
			_logger = logger;
			_twitterService = twitterService;
		}

		public bool Execute(ProcessedMatchEntity processedMatch)
		{
			bool tweetSent = false;

			try
			{
				_logger.LogMessage(string.Format("{0} execution started - {1}", this.GetType().Name, DateTime.Now));

				tweetSent = _twitterService.SendTweet(processedMatch.Tweet);

				_logger.LogMessage(
					string.Format("Tweet {0}sent for [{1}] vs [{2}] on [{3}] with interest level [{4}]",
						tweetSent ? "" : "NOT ",
						processedMatch.Match.HomeTeam,
						processedMatch.Match.AwayTeam,
						processedMatch.Match.StartTime,
						processedMatch.MatchInterest));

				_logger.LogMessage(string.Format("{0} finished successfully - {1}", this.GetType().Name, DateTime.Now));
			}
			catch (Exception ex)
			{
				_logger.LogMessage(string.Format("{0} error occurred - {1}", this.GetType().Name, ex.Message), true);
			}

			return tweetSent;
		}
	}
}
