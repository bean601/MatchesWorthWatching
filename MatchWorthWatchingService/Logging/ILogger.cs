namespace MatchWorthWatchingService.Logging
{
	public interface ILogger
	{
		void LogMessage(string message, bool important = false);
	}
}
