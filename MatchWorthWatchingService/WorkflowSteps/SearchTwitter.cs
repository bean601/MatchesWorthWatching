using MatchWorthWatchingService.Logging;
using MatchWorthWatchingService.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchWorthWatchingService
{
	/// <summary>
	/// Step to initiate a search of Twitter for mentions of the teams playing in the match or the match itself
	/// </summary>
	/// <remarks>
	/// Work on this last, nice to have but not MVP
	/// </remarks>
	public class SearchTwitter
	{
		private ILogger _logger;
		private ITwitterService _twitterService;

		public SearchTwitter(ILogger logger, ITwitterService twitterService)
		{
			_logger = logger;
			_twitterService = twitterService;
		}

		public bool Execute() //TODO: not sure what type this will be or how I'll work this in
		{
			try
			{
				_logger.LogMessage(string.Format("{0} execution started - {1}", this.GetType().Name, DateTime.Now));

				//search twitter every x min for mentions and use that after the match to calculate interest?
				//if we pull this, have some kind of cool graph showing tweet counts over time of the match

				_logger.LogMessage(string.Format("{0} finished successfully - {1}", this.GetType().Name, DateTime.Now));
			}
			catch (Exception ex)
			{
				_logger.LogMessage(string.Format("{0} error occurred - {1}", this.GetType().Name, ex.Message), true);
			}

			return true;
		}
	}
}
