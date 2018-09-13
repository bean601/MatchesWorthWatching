## MatchesWorthWatching
Project to determine if a soccer match is worth watching (project just for fun, it's obviously impossible to tell this based on stats.

### Original Design
- Once every X min this job will run
- Once a day (how do we keep it from happening more than once a day?) we'll query the API to get the day's matches and store what time they're starting, their matchID, and the competitionID (this will be used all over)
- We'll save those IDs and timestamps in the SQL Lite db
- When the job re-runs we check the DB for any games we haven't gotten commentaries for in the past
- If those games should have ended by now (120min past start? make this a config value), check the matches against the API for commentaries
- If there's a commentary, pull it, parse it, use our logic to figure out if its interesting or not (make a lot of this config values)
- Then send out a tweet about it
- Once the tweet is sent, update our DB to show that that match was processed and sent

 #### Levels of interest - Very Boring, Boring, Interesting, ~~Interesting if you're a fan of X team, Very Interesting~~

### What makes a match interesting?
  - goals-more = more interesting
  - red cards-more = more interesting
	- possession-50/50 with no goals, not interesting, 90/10 and some goals - interesting if your team has 90
  - ~~twitter-more tweets about a game or team = more interesting~~ Note: not possible with the way Twitter search currently works
  - shots-more = more interesting
  - saves-more = more interesting, especially if its your team
  - corners-mildly interesting if there's a lot, higher threshold

### Sample games table structure
 competitionID int|matchID int|homeTeam string|awayTeam string|startDate date|startTime timestamp|season? string|tweetSent bool

### We're using -
 - RESTSharp 103.0.0
 - Football-API
 - SQLLite 3.22.0
 - SQLLite .Net ADO Provider
 - TBD - Twitter interface
 - Ninject 3.3.4

### TODO List
- ~~Use some DI framework~~ **DONE**
- Clean up workflow class
- Connect Twitter
- ~~Clean up gitignore file~~ **DONE**
- Finish interest calculator
- More tests!
- Add a UI
- Update referenced pacakges, some are getting out of date
