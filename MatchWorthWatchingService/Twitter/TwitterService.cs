using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchWorthWatchingService.Twitter
{
	/// <summary>
	/// Connects to Twitter to send Tweets
	/// </summary>
	/// <remarks>
	/// Allow exception to bubble up to step level, they'll be logged and handled there
	/// </remarks>
	public class TwitterService : ITwitterService
	{
		public bool SendTweet(string tweet)
		{
			throw new NotImplementedException();
		}
	}
}
