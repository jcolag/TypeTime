TypeTime
========

_TypeTime_ is a straightforward reaction-testing application.  A while ago, I had the idea that it might be useful to test reaction time on waking up, as an estimate of whether the previous night's sleep was enough and possibly of approximate health.  So far, it appears to be a valid experiment with at least some correlation.

The program works as follows:

 - Wait a small, random duration, to disrupt habit.

 - Choose a random string of pre-defined length, currently five, but intentionally made easy to change.

 - Time how long it takes the user to respond to that string.

 - Count the errors, using the [Levenshtein Distance](https://en.wikipedia.org/wiki/Levenshtein_distance), or at least something close enough.

 - Display and log the results.

The decision as to how to combine the delay, time, length, and errors into a single metric requires some analysis.

Logging
-------

_TypeTime_ logs either to a CSV file or an SQLite database, each with the following fields.

 - `date`, a `text` field, for the day's date.  Currently no time, because I only use it on waking up.

 - `day`, a `text` field with the day of the week.  Obviously not necessary, but handy for quick review.

 - `delay`, an `integer` field, representing the number of seconds the program waited before showing the target string.

 - `length`, an `integer` field, the number of characters in the target string.

 - `errors`, an `integer` field, the Levenshtein distance between the target string and the user input.

 - `time`, a `real` field, the user response time (in seconds).

The CSV format and the database schema are the same.

Status
------

I previously wrote a version in C that still has a few more features like logging (which I can release, if anybody really wants it), but am migrating tools away from the command line.  So, this seemed like a perfect candidate for .NET/Mono.

At this time, both the console and GUI versions mostly work.  There is a fair amount of configuration built-in, but the options are hardcoded for the moment.  Currently, only the database file is user-configurable as the single command-line option.

Potential Issues
----------------

The big issue that might interest somebody is that logging to SQLite databases occurs through `Mono.Data.Sqlite`, meaning that _TypeTime_ will not operate under .NET.  There _is_ a .NET implementation of the SQLite libraries, [`System.Data.Sqlite`](https://system.data.sqlite.org/index.html/doc/trunk/www/index.wiki), which may well be functionally identical.  That will require testing.

There appears to be some sort of GTK+ race condition that occasionally prevents the application from being usable.  Too often for the program to be reliable, an error of `(TypeTime:`_pid_`): GLib-CRITICAL **: Source ID` _some id_ `was not found when attempting to remove it` gets issued during the course of executing the `HandleElapsed()` method.  When it happens (always when the program's delay is non-trivial), the entry box will not accept focus.

Credits
-------

The icon is made up of a slightly modified [Keyboard](http://www.thenounproject.com/term/keyboard/6006/) by [David Cadusseau](http://www.thenounproject.com/kaduma/) from [The Noun Project](http://www.thenounproject.com/) and [Watch](http://thenounproject.com/term/watch/5837/) by [George Tsatalios](http://thenounproject.com/George.Tsatalios/) (also) from The Noun Project.

