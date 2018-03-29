using MatchWorthWatchingService.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Data.SQLite.Generic;
using Dapper;

namespace MatchWorthWatchingService.DAL
{
	/// <summary>
	/// Houses all database interactions, just a first pass, need to refactor
	/// </summary>
	/// <remarks>
	/// Replace with Dapper at some point soon
	/// </remarks>
	public class DatabaseOperations : IDatabaseOperations
	{
		private string GetDbConnectionString()
		{
			return ConfigurationManager.ConnectionStrings["SqliteConnectionString"].ConnectionString;
		}

		private string GetPriorDaysToCheckForMatches()
		{
			return ConfigurationManager.AppSettings["PriorDaysToCheckForMatches"];  //TODO: come up with a better name
		}

		public DateTime GetLastMatchCheckTime()
		{
			var lastCheckDate = DateTime.MinValue;
			var connection = new SQLiteConnection(GetDbConnectionString());

			var sql = "select lastcheck from lastchecktime order by lastcheck desc limit 1";
			lastCheckDate = connection.QueryFirst<DateTime>(sql);
			return lastCheckDate;
		}

		public IEnumerable<MatchEntity> GetMatchesToCheck()
		{
			IEnumerable<MatchEntity> matches = null;
			var connection = new SQLiteConnection(GetDbConnectionString());

			var sql = string.Format("select * from matches where tweetSent = 0 and (julianday('now') - julianday(startTime) < {0})", GetPriorDaysToCheckForMatches());
			matches = connection.Query<MatchEntity>(sql);
			return matches;
		}

		public void UpdateMatchTwitterStatus(int matchId, bool tweetSent)
		{
			var connection = new SQLiteConnection(GetDbConnectionString());
			connection.Open();

			var sql = @"update matches set tweetSent = @tweetSent where matchId = @matchId;";
			var command = new SQLiteCommand(sql, connection);

			command.Parameters.AddWithValue("matchId", matchId);
			command.Parameters.AddWithValue("tweetSent", tweetSent);

			command.ExecuteNonQuery();
		}

		public void PersistMatchesToDatabase(IEnumerable<MatchEntity> matches)
		{
			var connection = new SQLiteConnection(GetDbConnectionString());
			connection.Open();

			foreach(var match in matches)
			{
				var sql = @"insert into matches (competitionId, matchId, homeTeam, awayTeam, season, startTime, tweetSent)
							select @competitionId, @matchId, @homeTeam, @awayTeam, @season, @startTime, @tweetSent
							where not exists (select 1 from matches where matchId = @matchId);"; //TODO: optimize this query
				var command = new SQLiteCommand(sql, connection);

				command.Parameters.AddWithValue("competitionId", match.CompetitionId);
				command.Parameters.AddWithValue("matchId", match.MatchId);
				command.Parameters.AddWithValue("homeTeam", match.HomeTeam);
				command.Parameters.AddWithValue("awayTeam", match.AwayTeam);
				command.Parameters.AddWithValue("season", match.Season);
				command.Parameters.AddWithValue("startTime", match.StartTime);
				command.Parameters.AddWithValue("tweetSent", match.TweetSent);

				command.ExecuteNonQuery();
			}
		}
	}
}
