using MatchWorthWatchingService.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchWorthWatchingService
{
	public class WorkflowSessionValues
	{
		/// <summary>
		/// The current league's competitionId.  This will become more important when more leagues are supported, make it more flexible in the future
		/// </summary>
		public int CompetitionId
		{
			get
			{
				return int.Parse(ConfigurationManager.AppSettings["CompetitionID"]);
			}
		}

		/// <summary>
		/// The last time we checked the match schedule
		/// </summary>
		public DateTime LastCheckedDate { get; set; }

		/// <summary>
		/// The first date to start checking for matches
		/// </summary>
		/// <remarks>
		/// Could be pulled from LastCheckedDate, but leaving separate for testing and configuration
		/// </remarks>
		public DateTime MatchWindowStartDate { get; set; }

		/// <summary>
		/// The last date to check for matches
		/// </summary>
		/// <remarks>
		/// Could be pulled from LastCheckedDate, but leaving separate for testing and configuration
		/// </remarks>
		public DateTime MatchWindowEndDate { get; set; }

		/// <summary>
		/// The queue of matches we have to process
		/// </summary>
		public Queue<MatchEntity> MatchesToProcess { get; set; }

		/// <summary>
		/// The fully processed matches we've completed
		/// </summary>
		public List<ProcessedMatchEntity> ProcessedMatches { get; set; }

		/// <summary>
		/// Value to determine if we need to stop processing completely
		/// </summary>
		public bool ContinueProcessing { get; set; }
	}
}
