// <copyright file="MainClass.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace TypeTime
{
        using System;
        using Gtk;

        /// <summary>
        /// Main class.
        /// </summary>
        public class MainClass
        {
                /// <summary>
                /// The entry point of the program, where the program control starts and ends.
                /// </summary>
                /// <param name="args">The command-line arguments.</param>
                public static void Main(string[] args)
                {
                        string db = args.Length > 0 ? args[0] : null;

                        Application.Init();
                        MainWindow win = new MainWindow(db, null);
                        win.Show();
                        Application.Run();
                }
        }
}
