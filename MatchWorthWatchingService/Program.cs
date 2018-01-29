using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace MatchWorthWatchingService
{
	// To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
	public class Program
	{
		// Please set the following connection strings in app.config for this WebJob to run:
		// AzureWebJobsDashboard and AzureWebJobsStorage
		public static void Main()
		{
			Console.Out.WriteLine("first azure test!");

			//once every X min this job will run
 			//once a day (how do we keep it from happening more than once a day?) we'll query the API
			//to get the day's matches and store what time they're starting, their matchID, and the competitionID (this will be used all over)
			//we'll save those IDs and timestamps in the Azure SQL DB
			//when the job re-runs we check the DB for any games we haven't gotten commentaries for in the past
			//if those games should have ended by now (120min past start? make this a config value), check the matches against the API for
			//  commentaries 
			//if there's a commentary, pull it, parse it, use our logic to figure out if its interesting or not (make a lot of this config values)
			//then send out a tweet about it
			//once the tweet is sent, update our DB to show that that match was processed and sent
			//
			// Levels of interesting - Very Boring, Boring, Interesting, Interesting if you're a fan of X team, Very Interesting
			//
			//what makes a match interesting?
			//  goals-more = more interesting
			//  red cards-more = more interseting
 			//	possesion-50/50 with no goals, not interesting, 90/10 and some goals - interesting if youre team has 90
			//  twitter-more tweets about a game or team = more interesting
			//  shots-more = more interesting
			//  saves-more = more interesting, especially if its your team
			//  corners-mildly interesting if there's a lot, higher threshold
			//  
			//
			//sample games table structure
			// competitionID int|matchID int|homeTeam string|awayTeam string|startDate date|startTime timestamp|season? string|tweetSent bool
					
		}
	}
}
