using MatchWorthWatchingService.DAL;
using MatchWorthWatchingService.Common;
using MatchWorthWatchingService.FootballAPI;
using MatchWorthWatchingService.Logging;
using MatchWorthWatchingService.Twitter;
using MatchWorthWatchingService.WorkflowSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchInterestCalculator;

namespace MatchWorthWatchingService
{
	public class Workflow
	{
		private ILogger _logger;
		private ITwitterService _twitterService;
		private IFootballAPIClient _footballAPI;
		private IDatabaseOperations _database;
		private ITweetBuilder _tweetBuilder;
		private IInterestCalculator _interestCalculator;

		public Workflow(ILogger logger,
			ITwitterService twitterService,
			IFootballAPIClient footballAPI,
			IDatabaseOperations database,
			ITweetBuilder tweetBuilder,
			IInterestCalculator interestCalculator)
		{
			_logger = logger;
			_twitterService = twitterService;
			_footballAPI = footballAPI;
			_database = database;
			_tweetBuilder = tweetBuilder;
			_interestCalculator = interestCalculator;
		}

		public void Execute()
		{
			if (_logger != null && _twitterService != null && _footballAPI != null && _database != null && _tweetBuilder != null)
			{
				_logger.LogMessage(string.Format("Processing started on {0}", DateTime.Now));

				var sessionValues = new WorkflowSessionValues();
				sessionValues.ContinueProcessing = true;
				sessionValues.ProcessedMatches = new List<ProcessedMatchEntity>(); //TODO: move this to the session value getter, changed during debug
				sessionValues.MatchesToProcess = new Queue<MatchEntity>();

				var getMatchScoreFromAPIStep = new GetMatchScoreFromAPI(_logger, _footballAPI);
				var getLastMatchCheckTimeStep = new GetLastMatchCheckTimeFromDatabase(_logger, _database);
				var buildMatchTweetStep = new BuildMatchTweet(_logger, _tweetBuilder);
				var getMatchesFromAPIStep = new GetMatchesFromAPI(_logger, _footballAPI);
				var getMatchesToSearchFromDatabaseStep = new GetMatchesToSearchFromDatabase(_logger, _database);
				var getMatchStatsFromAPIStep = new GetMatchStatsFromAPI(_logger, _footballAPI);
				var persistMatchesToDatabaseStep = new PersistMatchesToDatabase(_logger, _database);
				var processMatchStatsStep = new ProcessMatchStats(_logger, _interestCalculator);
				//var searchTwitterStep = new SearchTwitterStep(logger, twitterService);  //TODO: implement this later

				var sendMatchTweetStep = new SendMatchTweet(_logger, _twitterService);
				var updateMatchTwitterStatusStep = new UpdateMatchTwitterStatus(_logger, _database);

				//TODO: just getting it working in a messy way then will refactor around a list of steps concept with a managing class
				//TODO: get rid of passing around sessionValues like this

				sessionValues.LastCheckedDate = getLastMatchCheckTimeStep.Execute();

				if (sessionValues.LastCheckedDate.Day != DateTime.Now.Day)
				{
					sessionValues.MatchWindowStartDate = DateTime.Now.AddDays(-2); //TODO: set for lastCheckedDate after testing
					sessionValues.MatchWindowEndDate = DateTime.Now;

					sessionValues.MatchesToProcess = getMatchesFromAPIStep.Execute(sessionValues.CompetitionId, sessionValues.MatchWindowStartDate, sessionValues.MatchWindowEndDate);

					if (sessionValues.MatchesToProcess != null && sessionValues.MatchesToProcess.Any())
					{
						persistMatchesToDatabaseStep.Execute(sessionValues.MatchesToProcess);
					}
				}

				//get matches that are before our search window but we haven't sent tweets for yet
				var matchesFromDB = getMatchesToSearchFromDatabaseStep.Execute();

				if (matchesFromDB != null && matchesFromDB.Any())
				{
					foreach (var match in matchesFromDB)
					{
						if (!sessionValues.MatchesToProcess.Any(x => x.MatchId == match.MatchId))
						{
							sessionValues.MatchesToProcess.Enqueue(match);
						}
					}
				}

				//TODO: remove nesting
				if (sessionValues != null && sessionValues.MatchesToProcess.Any())
				{
					foreach (var match in sessionValues.MatchesToProcess)
					{
						var matchInProcess = new ProcessedMatchEntity { Match = match };

						//all to just get the full time score...
						matchInProcess.Match = getMatchScoreFromAPIStep.Execute(match);

						if (matchInProcess.Match.Score != null)
						{
							matchInProcess.MatchStats = getMatchStatsFromAPIStep.Execute(match);

							if (matchInProcess.MatchStats != null)
							{
								matchInProcess.MatchInterest = processMatchStatsStep.Execute(matchInProcess.MatchStats);

								if (matchInProcess.MatchInterest != InterestLevel.Unknown)
								{
									matchInProcess.Tweet = buildMatchTweetStep.Execute(matchInProcess);

									if (!string.IsNullOrEmpty(matchInProcess.Tweet))
									{
										if (sendMatchTweetStep.Execute(matchInProcess))
										{
											updateMatchTwitterStatusStep.Execute(matchInProcess);
										}
									}
								}
							}
						}

						sessionValues.ProcessedMatches.Add(matchInProcess);
					}
				}

				_logger.LogMessage(string.Format("Processing completed on {0}", DateTime.Now));
			}
			else
			{
				throw new Exception("One or more dependencies were null");
			}
		}
	}
}
