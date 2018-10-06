using System;
using System.Linq;

namespace MatchWorthWatchingService.WorkflowSteps
{
	public static class StringHelpers
	{
		public static int ParsePossessionTime(string possesionString)
		{
			int possesionPercentage = -1;

			if (!string.IsNullOrEmpty(possesionString))
			{
				var trimmedString = possesionString;
				if (possesionString.Contains("%"))
				{
					trimmedString = possesionString.Remove(possesionString.Length - 1);
				}

				int.TryParse(trimmedString, out possesionPercentage);
			}

			return possesionPercentage;
		}

		public static int ParseAwayScore(string fulltimeScore)
		{
			int awayScore = -1;

			if (!string.IsNullOrEmpty(fulltimeScore) && fulltimeScore.Contains('-'))
			{
				var splitScore = fulltimeScore.Split('-').LastOrDefault();
				var trimmedScore = splitScore.Substring(0, splitScore.Length - 1);
				if (!int.TryParse(trimmedScore, out awayScore))
				{
					return -1;
				}
			}

			return awayScore;
		}

		public static int ParseHomeScore(string fulltimeScore)
		{
			int homeScore = -1;

			if (!string.IsNullOrEmpty(fulltimeScore) && fulltimeScore.Contains('-'))
			{
				var splitScore = fulltimeScore.Split('-').FirstOrDefault();
				var trimmedScore = splitScore.Substring(1, splitScore.Length - 1);
				if (!int.TryParse(trimmedScore, out homeScore))
				{
					return -1;
				}
			}

			return homeScore;
		}

		/// <summary>
		/// API's UK format will be DD.MM.YYYY
		/// </summary>
		/// <param name="formattedDate"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public static DateTime CombineUKFormattedDateWithTime(string formattedDate, DateTime time)
		{
			var returnDateTime = DateTime.MinValue;

			if (!string.IsNullOrEmpty(formattedDate) && time != null && time != DateTime.MinValue)
			{
				var splitFormattedDate = formattedDate.Split('.');

				if (splitFormattedDate != null && splitFormattedDate.Any() && splitFormattedDate.Count() == 3)
				{
					int year, month, day;

					if (int.TryParse(splitFormattedDate[2], out year)
						&& int.TryParse(splitFormattedDate[1], out month)
						&& int.TryParse(splitFormattedDate[0], out day))
					{
						returnDateTime = new DateTime(year, month, day, time.Hour, time.Minute, time.Second);
					}
				}
			}

			return returnDateTime;
		}
	}
}
