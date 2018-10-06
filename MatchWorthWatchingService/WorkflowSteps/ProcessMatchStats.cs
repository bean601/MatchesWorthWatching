using MatchesWorthWatching.Common;
using MatchWorthWatchingService.Common;
using MatchWorthWatchingService.Logging;
using System;

namespace MatchWorthWatchingService.WorkflowSteps
{
	public class ProcessMatchStats
	{
		private ILogger _logger;
		private IInterestCalculator _interestCalculator;

		public ProcessMatchStats(ILogger logger, IInterestCalculator interestCalculator)
		{
			_logger = logger;
			_interestCalculator = interestCalculator;
		}

		public InterestLevel Execute(MatchStatsEntity match)
		{
			var interest = InterestLevel.Unknown;

			try
			{
				_logger.LogMessage(string.Format("{0} execution started - {1}", this.GetType().Name, DateTime.Now));

				interest = _interestCalculator.CalculateMatchInterest(match);

				_logger.LogMessage(string.Format("Match interest level rated at - *[{0}]*", interest));
				_logger.LogMessage(string.Format("{0} finished successfully - {1}", this.GetType().Name, DateTime.Now));
			}
			catch (Exception ex)
			{
				_logger.LogMessage(string.Format("{0} error occurred - {1}", this.GetType().Name, ex.Message), true);
			}

			return interest;
		}
	}
}
