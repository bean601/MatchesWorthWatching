using MatchWorthWatchingService.DAL;
using MatchWorthWatchingService.Common;
using MatchWorthWatchingService.Logging;
using System;
using System.Collections.Generic;

namespace MatchWorthWatchingService.WorkflowSteps
{
	public class PersistMatchesToDatabase
	{
		private ILogger _logger;
		private IDatabaseOperations _database;

		public PersistMatchesToDatabase(ILogger logger, IDatabaseOperations database)
		{
			_logger = logger;
			_database = database;
		}

		public void Execute(IEnumerable<MatchEntity> matchesToPersist)
		{
			try
			{
				_logger.LogMessage(string.Format("{0} execution started - {1}", this.GetType().Name, DateTime.Now));

				_database.PersistMatchesToDatabase(matchesToPersist);

				_logger.LogMessage(string.Format("{0} finished successfully - {1}", this.GetType().Name, DateTime.Now));
			}
			catch (Exception ex)
			{
				_logger.LogMessage(string.Format("{0} error occurred - {1}", this.GetType().Name, ex.Message), true);
			}
		}
	}
}
