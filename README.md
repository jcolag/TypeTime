TypeTime
========

_TypeTime_ is a straightforward reaction-testing application.  A while ago, I had the idea that it might be useful to test reaction time on waking up, as an estimate of whether the previous night's sleep was enough and possibly of approximate health.  So far, it appears to be a valid experiment.

The program works as follows:

 - Wait a small, random number of seconds, to disrupt habit.

 - Choose a random string of pre-defined length, currently five, but intentionally made easy to change.

 - Time how long it takes the user to respond to that string.

 - Count the errors, using the [Levenshtein Distance](https://en.wikipedia.org/wiki/Levenshtein_distance), or at least something close enough.

The decision as to how to combine the delay, time, length, and errors into a single metric requires some analysis.

Status
------

I previously wrote a version in C that still has a few more features like logging (which I can release, if anybody really wants it), but am migrating tools away from the command line.  So, this seemed like a perfect candidate for .NET/Mono.

Right now, only the console version has been written, and seems to work well enough.  I will get to the GTK# version in the near future and probably extract the common functionality into an explicit library, rather than keeping it in an executable.

There is a fair amount of configuration built-in, but the options are hardcoded for the moment.

The program doesn't yet do any logging, whereas the C version shows all of the metrics and the date is supplied elsewhere.  Given the work on [uManage](https://github.com/jcolag/uManage), _TypeTime_ will probably eventually need to log to an [SQLite](https://www.sqlite.org/) database for consistency.

