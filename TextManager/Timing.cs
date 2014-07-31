namespace TextManager
{
        using System;

        public class Timing
        {
                private DateTime start = DateTime.Now;

                private DateTime end = DateTime.Now;

                private TimeSpan duration;

                private int delay = 0;

                public Timing()
                {
                        this.delay = new Random().Next(4);
                }

                public Timing(int seconds)
                {
                        this.delay = new Random().Next(seconds);
                }

                public TimeSpan Duration
                {
                        get
                        {
                                return this.duration;
                        }
                }

                public void SetStart()
                {
                        this.start = DateTime.Now;
                }

                public void SetEnd()
                {
                        this.end = DateTime.Now;
                        this.duration = this.end - this.start;
                }

                public void Wait()
                {
                        System.Threading.Thread.Sleep(this.delay * 1000);
                }
        }
}

