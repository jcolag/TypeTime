TypeTime
========

_TypeTime_ is a straightforward reaction-testing application.  A while ago, I had the idea that it might be useful to test reaction time on waking up, as an estimate of whether the previous night's sleep was enough and possibly of approximate health.  So far, it appears to be a valid experiment.

The program works as follows:

 - Wait a small, random number of seconds, to disrupt habit.

 - Choose a random string of pre-defined length, currently five, but intentionally made easy to change.

 - Time how long it takes the user to respond to that string.

 - Count the errors, using the [Levenshtein Distance](https://en.wikipedia.org/wiki/Levenshtein_distance), or at least something close enough.

 - Log the results.

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

Right now, only the console version has been written, and seems to work well enough.  I will get to the GTK# version in the near future and probably extract the common functionality into an explicit library, rather than keeping it in an executable.

There is a fair amount of configuration built-in, but the options are hardcoded for the moment.

Potential Issues
----------------

The big issue that might interest somebody is that logging to SQLite databases occurs through `Mono.Data.Sqlite`, meaning that _TypeTime_ will not operate under .NET.  There _is_ a .NET implementation of the SQLite libraries, [`System.Data.Sqlite`](https://system.data.sqlite.org/index.html/doc/trunk/www/index.wiki), which may well be functionally identical.  That will require testing.

