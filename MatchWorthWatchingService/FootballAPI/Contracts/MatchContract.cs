using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchWorthWatchingService.FootballAPI.Contracts
{
	public class MatchContract
	{
		public int id { get; set; }
		public int comp_id { get; set; }
		public string season { get; set; }
		public string localteam_name { get; set; }
		public string visitorteam_name { get; set; }
		public string formatted_date { get; set; }
		public DateTime time { get; set; }
		public string status { get; set; }
		public string ft_score { get; set; }
/*
[
  {
    "id": "1921980",
    "comp_id": "1204",
    "formatted_date": "03.01.2016",
    "season": "2015/2016",
    "week": "20",
    "venue": "Selhurst Park (London)",
    "venue_id": "1265",
    "venue_city": "London",
    "status": "FT",
    "timer": "string",
    "time": "13:30",
    "localteam_id": "9127",
    "localteam_name": "Crystal Palace",
    "localteam_score": "0",
    "visitorteam_id": "9092",
    "visitorteam_name": "Chelsea",
    "visitorteam_score": "3",
    "ht_score": "[0-1]",
    "ft_score": "[0-3]",
    "et_score": "string",
    "penalty_local": "string",
    "penalty_visitor": "string",
    "events": [
      {
        "id": "21583632",
        "type": "goal",
        "minute": "29",
        "extra_min": 0,
        "team": "visitorteam",
        "player": "Oscar",
        "player_id": "57860",
        "assist": "D. Costa",
        "assist_id": "60977",
        "result": "[0-1]"
      }
    ]
  }
]
 */
	}
}
