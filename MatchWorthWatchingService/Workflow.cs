using MatchWorthWatchingService.DAL;
using MatchWorthWatchingService.Common;
using MatchWorthWatchingService.FootballAPI;
using MatchWorthWatchingService.Logging;
using MatchWorthWatchingService.Twitter;
using MatchWorthWatchingService.WorkflowSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using MatchesWorthWatching.Common;

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

		private GetMatchScoreFromAPI _getMatchScoreFromAPIStep;
		private GetLastMatchCheckTimeFromDatabase _getLastMatchCheckTimeStep;
		private BuildMatchTweet _buildMatchTweetStep;
		private GetMatchesFromAPI _getMatchesFromAPIStep;
		private GetMatchesToSearchFromDatabase _getMatchesToSearchFromDatabaseStep;
		private GetMatchStatsFromAPI _getMatchStatsFromAPIStep;
		private PersistMatchesToDatabase _persistMatchesToDatabaseStep;
		private ProcessMatchStats _processMatchStatsStep;
		private SendMatchTweet _sendMatchTweetStep;
		private UpdateMatchTwitterStatus _updateMatchTwitterStatusStep;

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

			_getMatchScoreFromAPIStep = new GetMatchScoreFromAPI(_logger, _footballAPI);
			_getLastMatchCheckTimeStep = new GetLastMatchCheckTimeFromDatabase(_logger, _database);
			_buildMatchTweetStep = new BuildMatchTweet(_logger, _tweetBuilder);
			_getMatchesFromAPIStep = new GetMatchesFromAPI(_logger, _footballAPI);
			_getMatchesToSearchFromDatabaseStep = new GetMatchesToSearchFromDatabase(_logger, _database);
			_getMatchStatsFromAPIStep = new GetMatchStatsFromAPI(_logger, _footballAPI);
			_persistMatchesToDatabaseStep = new PersistMatchesToDatabase(_logger, _database);
			_processMatchStatsStep = new ProcessMatchStats(_logger, _interestCalculator);
			_sendMatchTweetStep = new SendMatchTweet(_logger, _twitterService);
			_updateMatchTwitterStatusStep = new UpdateMatchTwitterStatus(_logger, _database);
		}

		public void Execute()
		{
			if (_logger != null
				&& _twitterService != null
				&& _footballAPI != null
				&& _database != null
				&& _tweetBuilder != null)
			{
				_logger.LogMessage(string.Format("Processing started on {0}", DateTime.Now));

				var sessionValues = new WorkflowSessionValues();
				sessionValues.ContinueProcessing = true;
				sessionValues.ProcessedMatches = new List<ProcessedMatchEntity>(); //TODO: move this to the session value getter, changed during debug
				sessionValues.MatchesToProcess = new Queue<MatchEntity>();

				sessionValues.LastCheckedDate = _getLastMatchCheckTimeStep.Execute();

				if (sessionValues.LastCheckedDate.Day != DateTime.Now.Day)
				{
					sessionValues.MatchWindowStartDate = DateTime.Now.AddDays(-130); //TODO: set for lastCheckedDate after testing
					sessionValues.MatchWindowEndDate = DateTime.Now;

					sessionValues.MatchesToProcess = _getMatchesFromAPIStep.Execute(sessionValues.CompetitionId, sessionValues.MatchWindowStartDate, sessionValues.MatchWindowEndDate);

					if (sessionValues.MatchesToProcess != null && sessionValues.MatchesToProcess.Any())
					{
						//TODO: if this fails we need to stop processing
						_persistMatchesToDatabaseStep.Execute(sessionValues.MatchesToProcess);
					}
				}

				//get matches that are before our search window but we haven't sent tweets for yet
				var matchesFromDB = _getMatchesToSearchFromDatabaseStep.Execute();

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
						//TODO: if we've already figured out the interest, only try to send the tweet
						var matchInProcess = new ProcessedMatchEntity { Match = match };

						//all to just get the full time score...
						matchInProcess.Match = _getMatchScoreFromAPIStep.Execute(match);

						if (matchInProcess.Match.Score != null)
						{
							matchInProcess.MatchStats = _getMatchStatsFromAPIStep.Execute(match);

							if (matchInProcess.MatchStats != null)
							{
								matchInProcess.MatchInterest = _processMatchStatsStep.Execute(matchInProcess.MatchStats);

								if (matchInProcess.MatchInterest != InterestLevel.Unknown)
								{
									matchInProcess.Tweet = _buildMatchTweetStep.Execute(matchInProcess);

									if (!string.IsNullOrEmpty(matchInProcess.Tweet))
									{
										if (_sendMatchTweetStep.Execute(matchInProcess))
										{
											_updateMatchTwitterStatusStep.Execute(matchInProcess);
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
