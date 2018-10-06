using MatchWorthWatchingService.DAL;
using MatchWorthWatchingService.Common;
using MatchWorthWatchingService.Logging;
using System;

namespace MatchWorthWatchingService.WorkflowSteps
{
	/// <summary>
	/// Step to update our DB to show that we've sent the tweet, consider logging the tweet itself?
	/// </summary>
	public class UpdateMatchTwitterStatus
	{
		private ILogger _logger;
		private IDatabaseOperations _database;

		public UpdateMatchTwitterStatus(ILogger logger, IDatabaseOperations database)
		{
			_logger = logger;
			_database = database;
		}

		public void Execute(ProcessedMatchEntity processedMatch)
		{
			try
			{
				_logger.LogMessage(string.Format("{0} execution started - {1}", this.GetType().Name, DateTime.Now));

				_database.UpdateMatchTwitterStatus(processedMatch.Match.MatchId, processedMatch.Match.TweetSent, processedMatch.MatchInterest);

				_logger.LogMessage(string.Format("{0} finished successfully - {1}", this.GetType().Name, DateTime.Now));
			}
			catch (Exception ex)
			{
				_logger.LogMessage(string.Format("{0} error occurred - {1}", this.GetType().Name, ex.Message), true);
			}
		}
	}
}
