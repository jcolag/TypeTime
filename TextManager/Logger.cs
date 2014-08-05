// <copyright file="Logger.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace TextManager
{
        using System;
        using System.IO;
        using Mono.Data.Sqlite;

        /// <summary>
        /// Centralized logging.
        /// </summary>
        public class Logger : IDisposable
        {
                /// <summary>
                /// The create table command.
                /// </summary>
                private const string CreateComm =
                        "CREATE TABLE IF NOT EXISTS reaction " +
                        "(date TEXT, day TEXT, delay INTEGER, " +
                        "length INTEGER, errors INTEGER, time REAL)";

                /// <summary>
                /// The database connection.
                /// </summary>
                private readonly Mono.Data.Sqlite.SqliteConnection db;

                /// <summary>
                /// The csv output file.
                /// </summary>
                private StreamWriter csvOut;

                /// <summary>
                /// Whether to log to stdout.
                /// </summary>
                private bool useStdout = false;

                /// <summary>
                /// Initializes a new instance of the <see cref="TextManager.Logger"/> class.
                /// </summary>
                /// <param name="connectionString">Connection string.</param>
                /// <param name="csvFile">CSV file.</param>
                public Logger(string connectionString, string csvFile)
                {
                        if (!string.IsNullOrWhiteSpace(connectionString))
                        {
                                this.db = new SqliteConnection("URI=file:" + connectionString);
                                this.db.Open();
                                using (SqliteCommand command = this.db.CreateCommand())
                                {
                                        command.CommandText = Logger.CreateComm;
                                        command.ExecuteNonQuery();
                                }
                        }

                        if (!string.IsNullOrWhiteSpace(csvFile))
                        {
                                this.csvOut = new StreamWriter(csvFile);
                        }
                }

                /// <summary>
                /// Initializes a new instance of the <see cref="TextManager.Logger"/> class.
                /// </summary>
                /// <param name="conf">Configuration object.</param>
                public Logger(Configuration conf)
                {
                        if (!string.IsNullOrWhiteSpace(conf.DbName))
                        {
                                this.db = new SqliteConnection("URI=file:" + conf.DbName);
                                this.db.Open();
                                using (SqliteCommand command = this.db.CreateCommand())
                                {
                                        command.CommandText = Logger.CreateComm;
                                        command.ExecuteNonQuery();
                                }
                        }

                        if (!string.IsNullOrWhiteSpace(conf.CsvName))
                        {
                                this.csvOut = new StreamWriter(conf.CsvName);
                        }

                        this.useStdout = conf.UseStdout;
                }

                /// <summary>
                /// Finalizes an instance of the <see cref="TextManager.Logger"/> class.
                /// Releases unmanaged resources and performs other cleanup operations before the
                /// <see cref="TextManager.Logger"/> is reclaimed by garbage collection.
                /// </summary>
                ~Logger()
                {
                        if (this.db != null)
                        {
                                this.db.Close();
                        }

                        if (this.csvOut != null)
                        {
                                this.csvOut.Close();
                        }
                }

                /// <summary>
                /// Gets a value indicating whether this <see cref="TextManager.Logger"/> is disposed.
                /// </summary>
                /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
                protected bool Disposed
                {
                        get;
                        private set;
                }

                /// <summary>
                /// Log the specified when, delay, length, errors and time.
                /// </summary>
                /// <param name="when">Event time.</param>
                /// <param name="delay">Delay before action.</param>
                /// <param name="length">Length of string.</param>
                /// <param name="errors">Errors made.</param>
                /// <param name="time">Time to respond.</param>
                public void Log(DateTime when, int delay, int length, int errors, double time)
                {
                        string now = when.ToString(
                                "yyyy-MM-dd",
                                System.Globalization.CultureInfo.CurrentCulture);
                        if (this.Disposed)
                        {
                                throw new ObjectDisposedException(GetType().Name);
                        }

                        if (this.db != null)
                        {
                                using (SqliteCommand command = this.db.CreateCommand())
                                {
                                        command.CommandText = "INSERT INTO reaction VALUES (\"" +
                                                now + "\",\"" + when.DayOfWeek.ToString() + "\"," +
                                                delay.ToString() + "," + length.ToString() + "," +
                                                errors.ToString() + "," + time.ToString() + ")";
                                        command.ExecuteNonQuery();
                                }
                        }

                        if (this.csvOut != null)
                        {
                                this.csvOut.WriteLine(now + "," + when.DayOfWeek.ToString() + "," +
                                        delay.ToString() + "," + length.ToString() + "," +
                                        errors.ToString() + "," + time.ToString());
                        }

                        if (this.useStdout)
                        {
                                Console.WriteLine(now + "," + when.DayOfWeek.ToString() + "," +
                                        delay.ToString() + "," + length.ToString() + "," +
                                        errors.ToString() + "," + time.ToString());
                        }
                }

                /// <summary>
                /// Releases all resource used by the <see cref="TextManager.Logger"/> object.
                /// </summary>
                /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="TextManager.Logger"/>. The
                /// <see cref="Dispose"/> method leaves the <see cref="TextManager.Logger"/> in an unusable state. After
                /// calling <see cref="Dispose"/>, you must release all references to the
                /// <see cref="TextManager.Logger"/> so the garbage collector can reclaim the memory that the
                /// <see cref="TextManager.Logger"/> was occupying.</remarks>
                public void Dispose()
                {
                        if (this.db != null)
                        {
                                this.db.Dispose();
                        }

                        if (this.csvOut != null)
                        {
                                this.csvOut.Dispose();
                        }
                }
 
                /// <summary>
                /// Dispose the specified disposing.
                /// </summary>
                /// <param name="disposing">If set to <c>true</c> disposing.</param>
                protected virtual void Dispose(bool disposing)
                {
                        // Multiple Dispose calls should be OK.
                        if (!this.Disposed)
                        {
                                if (disposing)
                                {
                                        // None of our fields have been finalized so it's safe to
                                        // clean them up here.
                                        this.Dispose();
                                }

                                // Our fields may have been finalized so we should only
                                // touch native fields (e.g. IntPtr or UIntPtr fields) here.
                                this.Disposed = true;
                        }
                }
        }
}