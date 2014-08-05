// <copyright file="Configuration.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace TextManager
{
        using System;
        using System.IO;
        using Nini.Config;

        /// <summary>
        /// Configuration for TypeTime operations.
        /// </summary>
        public class Configuration
        {
                /// <summary>
                /// The configuration file location.
                /// </summary>
                private string configLocation;

                /// <summary>
                /// The name of the SQLite database for reporting, optional.
                /// </summary>
                private string databaseName;

                /// <summary>
                /// The name of the CSV file for reporting, optional.
                /// </summary>
                private string csvName;

                /// <summary>
                /// Whether to use stdout for reporting.
                /// </summary>
                private bool useStdout;

                /// <summary>
                /// The length of the target string.
                /// </summary>
                private int targetLength = 5;

                /// <summary>
                /// The maximum delay before accepting input.
                /// </summary>
                private int maxDelay = 5;

                /// <summary>
                /// Initializes a new instance of the <see cref="TextManager.Configuration"/> class.
                /// </summary>
                public Configuration()
                {
                        this.configLocation = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        if (this.configLocation[0] == '/')
                        {
                                this.configLocation += "/.config";
                        }

                        this.configLocation += "/TypeTime";
                        DirectoryInfo di = Directory.CreateDirectory(this.configLocation);
                        FileInfo[] files = di.GetFiles();

                        FileInfo config = null;
                        foreach (FileInfo fi in files)
                        {
                                if (fi.Name == "TypeTime.ini")
                                {
                                        config = fi;
                                        this.configLocation += "/TypeTime.ini";
                                        break;
                                }
                        }

                        if (config == null)
                        {
                                return;
                        }

                        this.ReadConfiguration(this.configLocation);
                }

                /// <summary>
                /// Initializes a new instance of the <see cref="TextManager.Configuration"/> class.
                /// </summary>
                /// <param name="filename">Configuration file name.</param>
                public Configuration(string filename)
                {
                        this.configLocation = filename;
                        this.ReadConfiguration(filename);
                }

                /// <summary>
                /// Gets the name of the db.
                /// </summary>
                /// <value>The name of the database.</value>
                public string DbName
                {
                        get
                        {
                                return this.databaseName;
                        }
                }

                /// <summary>
                /// Gets the name of the CSV file.
                /// </summary>
                /// <value>The name of the csv.</value>
                public string CsvName
                {
                        get
                        {
                                return this.csvName;
                        }
                }

                /// <summary>
                /// Gets a value indicating whether this <see cref="TextManager.Configuration"/> should
                /// log to stdout.
                /// </summary>
                /// <value><c>true</c> if use stdout; otherwise, <c>false</c>.</value>
                public bool UseStdout
                {
                        get
                        {
                                return this.useStdout;
                        }
                }

                /// <summary>
                /// Gets the length of the target string.
                /// </summary>
                /// <value>The length of the target.</value>
                public int TargetLength
                {
                        get
                        {
                                return this.targetLength;
                        }
                }

                /// <summary>
                /// Gets the maximum delay.
                /// </summary>
                /// <value>The maximum delay.</value>
                public int MaxDelay
                {
                        get
                        {
                                return this.maxDelay;
                        }
                }

                /// <summary>
                /// Gets the configuration option.
                /// </summary>
                /// <returns>The configuration option.</returns>
                /// <param name="src">Source configuration.</param>
                /// <param name="section">File section.</param>
                /// <param name="key">Configuration key.</param>
                /// <param name="defValue">Default value.</param>
                /// <typeparam name="T">The expected type.</typeparam>
                private static T GetConfiguration<T>(IConfigSource src, string section, string key, string defValue)
                {
                        T res = default(T);
                        string value = src.Configs[section].Get(key, defValue);
                        try
                        {
                                res = (T)Convert.ChangeType(
                                        value,
                                        Convert.GetTypeCode(res),
                                        System.Globalization.CultureInfo.CurrentCulture);
                        }
                        catch (InvalidCastException)
                        {
                                res = default(T);
                        }
                        catch (FormatException)
                        {
                                res = default(T);
                        }
                        catch (OverflowException)
                        {
                                res = default(T);
                        }
                        catch (ArgumentException)
                        {
                                res = default(T);
                        }

                        return res;
                }

                /// <summary>
                /// Reads the configuration.
                /// </summary>
                /// <param name="filename">Configuration file name.</param>
                private void ReadConfiguration(string filename)
                {
                        IniConfigSource source;

                        try
                        {
                                source = new IniConfigSource(filename);
                        }
                        catch (Exception)
                        {
                                return;
                        }

                        this.databaseName = Configuration.GetConfiguration<string>(source, "File", "Database", string.Empty);
                        this.csvName = Configuration.GetConfiguration<string>(source, "File", "CSV", string.Empty);
                        this.useStdout = Configuration.GetConfiguration<bool>(source, "File", "Stdout", "false");
                        this.targetLength = Configuration.GetConfiguration<int>(source, "Target", "Length", this.targetLength.ToString());
                        this.maxDelay = Configuration.GetConfiguration<int>(source, "Operations", "Delay", this.maxDelay.ToString());
                }
        }
}