// <copyright file="MainWindow.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
using System;
using Gtk;

/// <summary>
/// Main window.
/// </summary>
public partial class MainWindow : Gtk.Window
{
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow() : base(Gtk.WindowType.Toplevel)
        {
                this.Build();
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
}
