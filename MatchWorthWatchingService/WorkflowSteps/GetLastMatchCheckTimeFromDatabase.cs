using MatchWorthWatchingService.DAL;
using MatchWorthWatchingService.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchWorthWatchingService.WorkflowSteps
{
	public class GetLastMatchCheckTimeFromDatabase
	{
		private ILogger _logger;
		private IDatabaseOperations _database;

		public GetLastMatchCheckTimeFromDatabase(ILogger logger, IDatabaseOperations database)
		{
			_logger = logger;
			_database = database;
		}

		public DateTime Execute()
		{
			DateTime lastCheckedDate = DateTime.MinValue;

			try
			{
				_logger.LogMessage(string.Format("{0} execution started - {1}", this.GetType().Name, DateTime.Now));

				lastCheckedDate = _database.GetLastMatchCheckTime();

				_logger.LogMessage(string.Format("LastCheckDate - {0}", lastCheckedDate));
				_logger.LogMessage(string.Format("{0} finished successfully - {1}", this.GetType().Name, DateTime.Now));
			}
			catch (Exception ex)
			{
				_logger.LogMessage(string.Format("{0} error occurred - {1}", this.GetType().Name, ex.Message), true);
			}

			return lastCheckedDate;
		}
	}
}
