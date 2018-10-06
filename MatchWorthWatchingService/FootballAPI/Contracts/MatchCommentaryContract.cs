using System.Collections.Generic;

namespace MatchWorthWatchingService.FootballAPI.Contracts
{
	public class MatchStats
	{
		public List<Team> localteam {get; set;}
		public List<Team> visitorteam { get; set; }
	}

	public class Team
	{
		public int shots_total { get; set; }
		public int shots_onGoal { get; set; }
		public int saves { get; set; }
		public int corners { get; set; }
		public string possesiontime { get; set; }
		public int redcards { get; set; }
		public int yellowcards { get; set; }
	}

	public class MatchCommentaryContract
	{
		public int match_id { get; set; }
		public MatchStats match_stats { get; set; }
		
		/*
		{
		  "match_id": "1967661",
		  "match_info": {
			"stadium": "Stadio Marc`Antonio Bentegodi, Italy",
			"attendance": "string",
			"referee": "string"
		  },
		  "lineup": {
			"localteam": [
			  {
				"id": "3372",
				"number": "1",
				"name": "Albano Bizarri",
				"pos": "G"
			  }
			],
			"visitorteam": [
			  {
				"id": "3372",
				"number": "1",
				"name": "Albano Bizarri",
				"pos": "G"
			  }
			]
		  },
		  "subs": {
			"localteam": [
			  {
				"id": "3372",
				"number": "1",
				"name": "Albano Bizarri",
				"pos": "G"
			  }
			],
			"visitorteam": [
			  {
				"id": "3372",
				"number": "1",
				"name": "Albano Bizarri",
				"pos": "G"
			  }
			]
		  },
		  "substitutions": {
			"localteam": [
			  {
				"off_name": "Riccardo Meggiorini",
				"on_name": "Roberto Inglese",
				"off_id": "58418",
				"on_id": "93392",
				"minute": "18"
			  }
			],
			"visitorteam": [
			  {
				"off_name": "Riccardo Meggiorini",
				"on_name": "Roberto Inglese",
				"off_id": "58418",
				"on_id": "93392",
				"minute": "18"
			  }
			]
		  },
		  "comments": [
			{
			  "id": "51787772",
			  "important": "0",
			  "isgoal": "0",
			  "minute": "90'",
			  "comment": "Offside, Roma. Kostas Manolas tries a through ball, but Marco Tumminello is caught offside."
			}
		  ],
		  "match_stats": {
			"localteam": {
			  "shots_total": "12",
			  "shots_ongoal": "6",
			  "fouls": "12",
			  "corners": "8",
			  "offsides": "1",
			  "possesiontime": "55%",
			  "yellowcards": "3",
			  "redcards": "0",
			  "saves": "2"
			},
			"visitorteam": {
			  "shots_total": "12",
			  "shots_ongoal": "6",
			  "fouls": "12",
			  "corners": "8",
			  "offsides": "1",
			  "possesiontime": "55%",
			  "yellowcards": "3",
			  "redcards": "0",
			  "saves": "2"
			}
		  },
		  "player_stats": {
			"localteam": [
			  {
				"id": "190553",
				"num": "24",
				"name": "Alessandro Florenzi",
				"pos": "RM",
				"posx": "4",
				"posy": "5",
				"shots_total": "3",
				"shots_on_goal": "1",
				"goals": "1",
				"assists": 0,
				"offsides": 0,
				"fouls_drawn": 0,
				"fouls_committed": 0,
				"saves": 0,
				"yellowcards": 0,
				"redcards": 0,
				"pen_score": 0,
				"pen_miss": 0
			  }
			],
			"visitorteam": [
			  {
				"id": "190553",
				"num": "24",
				"name": "Alessandro Florenzi",
				"pos": "RM",
				"posx": "4",
				"posy": "5",
				"shots_total": "3",
				"shots_on_goal": "1",
				"goals": "1",
				"assists": 0,
				"offsides": 0,
				"fouls_drawn": 0,
				"fouls_committed": 0,
				"saves": 0,
				"yellowcards": 0,
				"redcards": 0,
				"pen_score": 0,
				"pen_miss": 0
			  }
			]
		  }
		}
		 */
	}
}
