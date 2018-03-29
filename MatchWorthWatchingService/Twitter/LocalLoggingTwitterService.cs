using System;
namespace MatchWorthWatchingService.Twitter
{
	/// <summary>
	/// A fake Twitter service that just logs to a console or file
	/// </summary>
	/// <remarks>
	/// Use this for testing before we want to really send tweets.
	/// Allow exception to bubble up to step level, they'll be logged and handled there
	/// </remarks>
	public class LocalLoggingTwitterService : ITwitterService
	{
		/// <summary>
		/// Will log locally instead of sending a tweet
		/// </summary>
		/// <returns></returns>
		public bool SendTweet(string tweet)
		{
			Console.Out.WriteLine(tweet);
			return true;
		}
	}
}
