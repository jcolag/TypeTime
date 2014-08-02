// <copyright file="MainWindow.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
using System;
using System.Timers;
using Gtk;
using TextManager;

/// <summary>
/// Main window.
/// </summary>
public partial class MainWindow : Gtk.Window
{
        /// <summary>
        /// The target string.
        /// </summary>
        private RandomTarget target;

        /// <summary>
        /// The reaction time instrumentation.
        /// </summary>
        private Timing time;

        /// <summary>
        /// The reaction log.
        /// </summary>
        private Logger log;

        /// <summary>
        /// The delay before revealing the string.
        /// </summary>
        private Timer delay;

        /// <summary>
        /// The repeating timer to update the elapsed time.
        /// </summary>
        private Timer clock;

        /// <summary>
        /// The random number generator.
        /// </summary>
        private Random rand = new Random();

        /// <summary>
        /// The length of the target string.
        /// </summary>
        private int targetLength = 5;

        /// <summary>
        /// The interval before revealing the target string.
        /// </summary>
        private double interval;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow() : base(Gtk.WindowType.Toplevel)
        {
                this.Build();
                this.interval = (this.rand.Next(0, 4) * 1000) + 1;
                this.entryInput.Sensitive = false;
                this.delay = new Timer(this.interval);
                this.delay.AutoReset = false;
                this.delay.Elapsed += this.HandleElapsed;
                this.delay.Start();
                this.time = new Timing();
                this.log = new Logger("/home/john/Dropbox/activity.db", null);
        }

        /// <summary>
        /// Raises the delete event event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="a">The alpha component.</param>
        protected void OnDeleteEvent(object sender, DeleteEventArgs a)
        {
                Application.Quit();
                a.RetVal = true;
        }

        /// <summary>
        /// Raises the entry input activated event.
        /// </summary>
        /// <param name="sender">The input entry.</param>
        /// <param name="e">The event arguments.</param>
        protected void OnEntryInputActivated(object sender, EventArgs e)
        {
                string input = this.entryInput.Text.Trim();
                string output;
                int errors;

                this.clock.Stop();
                this.time.SetEnd();
                this.entryInput.Sensitive = false;
                errors = this.target.CheckString(input);
                output = this.time.Duration.TotalSeconds.ToString() + " seconds, "
                        + errors.ToString() + " errors";
                this.lblStatus.Text = output;

                this.log.Log(
                        this.time.Start,
                        (int)Math.Round(this.interval / 1000),
                        this.targetLength,
                        errors,
                        this.time.Duration.TotalSeconds);
        }

        /// <summary>
        /// Raises the entry input changed event.
        /// </summary>
        /// <param name="sender">The input entry.</param>
        /// <param name="e">The event arguments.</param>
        protected void OnEntryInputChanged(object sender, EventArgs e)
        {
                int len = this.entryInput.Text.Length;
                double done = (double)len / (double)this.targetLength;
                this.progressType.Fraction = done;
        }

        /// <summary>
        /// Handles the elapsed.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void HandleElapsed(object sender, ElapsedEventArgs e)
        {
                this.entryInput.Sensitive = true;
                this.target = new RandomTarget(this.targetLength);
                this.target.SetString();
                this.lblTarget.Text = this.target.Target;
                this.time.SetStart();
                this.lblStatus.Text = "Go!   ";

                this.clock = new Timer(90);
                this.clock.AutoReset = true;
                this.clock.Elapsed += this.HandleClockTick;
                this.clock.Start();

                this.entryInput.GrabFocus();
                this.Focus = this.entryInput;
        }

        /// <summary>
        /// Handles the clock tick.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void HandleClockTick(object sender, ElapsedEventArgs e)
        {
                TimeSpan dur = DateTime.Now - this.time.Start;
                this.lblElapsed.Text = dur.TotalSeconds.ToString();
        }
}