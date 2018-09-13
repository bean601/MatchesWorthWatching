using MatchWorthWatchingService.DAL;
using MatchWorthWatchingService.Logging;
using MatchWorthWatchingService.Twitter;
using MatchWorthWatchingService.FootballAPI;
using Ninject;
using System.Reflection;
using MatchesWorthWatching.Common;

namespace MatchWorthWatchingService
{
	public class Program
	{
		public static void Main()
		{
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            
			var workflow = new Workflow(kernel.Get<ILogger>(),
                kernel.Get<ITwitterService>(),
                kernel.Get<IFootballAPIClient>(),
                kernel.Get<IDatabaseOperations>(),
                kernel.Get<ITweetBuilder>(),
                kernel.Get<IInterestCalculator>());

			workflow.Execute();
        }
    }
}