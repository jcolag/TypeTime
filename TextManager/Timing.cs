// <copyright file="Timing.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace TextManager
{
        using System;

        /// <summary>
        /// Basic stopwatch for TypeTime.
        /// </summary>
        public class Timing
        {
                /// <summary>
                /// The start of typing.
                /// </summary>
                private DateTime start = DateTime.Now;

                /// <summary>
                /// The end of typing.
                /// </summary>
                private DateTime end = DateTime.Now;

                /// <summary>
                /// The duration of typing.
                /// </summary>
                private TimeSpan duration;

                /// <summary>
                /// The maximum delay before showing the text to type.
                /// </summary>
                private int delay;

                /// <summary>
                /// Initializes a new instance of the <see cref="TextManager.Timing"/> class.
                /// </summary>
                public Timing()
                {
                        this.delay = new Random().Next(4);
                }

                /// <summary>
                /// Initializes a new instance of the <see cref="TextManager.Timing"/> class.
                /// </summary>
                /// <param name="seconds">Maximum delay in seconds.</param>
                public Timing(int seconds)
                {
                        int sec = seconds;

                        if (sec < 0)
                        {
                                sec = 0;
                        }

                        this.delay = new Random().Next(sec);
                }

                /// <summary>
                /// Gets the duration.
                /// </summary>
                /// <value>The duration.</value>
                public TimeSpan Duration
                {
                        get
                        {
                                return this.duration;
                        }
                }

                /// <summary>
                /// Sets the starting time.
                /// </summary>
                public void SetStart()
                {
                        this.start = DateTime.Now;
                }

                /// <summary>
                /// Sets the ending time.
                /// </summary>
                public void SetEnd()
                {
                        this.end = DateTime.Now;
                        this.duration = this.end - this.start;
                }

                /// <summary>
                /// Pause before displaying content.
                /// </summary>
                public void Wait()
                {
                        System.Threading.Thread.Sleep(this.delay * 1000);
                }
        }
}