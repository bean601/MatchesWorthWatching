namespace MatchWorthWatchingService.Twitter
{
	/// <summary>
	/// Defines the Twitter interactions we will support
	/// </summary>
	public interface ITwitterService
	{
		/// <summary>
		/// Send a tweet
		/// </summary>
		/// <returns>Boolean to determine if the tweet was sent successfully</returns>
		bool SendTweet(string tweet);
	}
}
