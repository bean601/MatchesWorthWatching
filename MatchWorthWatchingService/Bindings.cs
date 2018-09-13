using MatchesWorthWatching.Common;
using MatchInterestCalculator;
using MatchWorthWatchingService.DAL;
using MatchWorthWatchingService.FootballAPI;
using MatchWorthWatchingService.Logging;
using MatchWorthWatchingService.Twitter;
using Ninject.Modules;

namespace MatchWorthWatchingService
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().To<ConsoleLogger>();
            Bind<IFootballAPIClient>().To<FootballAPIClient>();
            Bind<ITweetBuilder>().To<TweetBuilder>();
            Bind<IDatabaseOperations>().To<DatabaseOperations>();
            Bind<IInterestCalculator>().To<InterestCalculator>();

            //TODO: when Twitter connection is done, use the real Twitter service
            //Bind<ITwitterService>().To<TwitterService>();
            Bind<ITwitterService>().To<LocalLoggingTwitterService>();
        }
    }
}
