namespace MatchWorthWatchingService.Common
{
	public class ProcessedMatchEntity
	{
		public ProcessedMatchEntity()
		{
			MatchInterest = InterestLevel.Unknown;
			Processed = false;
		}

		public InterestLevel MatchInterest { get; set; }
		public MatchEntity Match { get; set; }
		public MatchStatsEntity MatchStats { get; set; }
		public string Tweet { get; set; }
		public bool Processed { get; set; }
	}
}
