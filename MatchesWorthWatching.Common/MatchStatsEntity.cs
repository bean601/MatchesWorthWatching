using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchWorthWatchingService.Common
{
	public class MatchStatsEntity
	{
		public int HomeScore { get; set; }
		public int AwayScore { get; set; }
		public int HomeShotsOnGoal { get; set; }
		public int AwayShotsOnGoal { get; set; }
		public int HomeShots { get; set; }
		public int AwayShots { get; set; }
		public int HomeSaves { get; set; }
		public int AwaySaves { get; set; }
		public int HomeRedCards { get; set; }
		public int AwayRedCards { get; set; }
		public int HomeYellowCards { get; set; }
		public int AwayYellowCards { get; set; }
		public int HomeCorners { get; set; }
		public int AwayCorners { get; set; }
		public int HomePossesion { get; set; }
		public int AwayPossesion { get; set; }
	}
}
