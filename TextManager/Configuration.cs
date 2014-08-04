namespace TextManager
{
        using System;
        using System.IO;

        /// <summary>
        /// Configuration for TypeTime operations.
        /// </summary>
        public class Configuration
        {
                /// <summary>
                /// The configuration file location.
                /// </summary>
                string configLocation;

                /// <summary>
                /// The name of the SQLite database for reporting, optional.
                /// </summary>
                string dbName = null;

                /// <summary>
                /// The name of the CSV file for reporting, optional.
                /// </summary>
                string csvName = null;

                /// <summary>
                /// Whether to use stdout for reporting.
                /// </summary>
                bool useStdout = false;

                /// <summary>
                /// The length of the target string.
                /// </summary>
                int targetLength = 5;

                /// <summary>
                /// The maximum delay before accepting input.
                /// </summary>
                int maxDelay = 5;

                /// <summary>
                /// Initializes a new instance of the <see cref="TextManager.Configuration"/> class.
                /// </summary>
                public Configuration()
                {
                        configLocation = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        if (configLocation[0] == '/')
                        {
                                configLocation += "/.config";
                        }

                        configLocation += "/TypeTime";
                        DirectoryInfo di = Directory.CreateDirectory(configLocation);
                        FileInfo[] files = di.GetFiles();

                        FileInfo config = null;
                        foreach (FileInfo fi in files)
                        {
                                if (fi.Name == "TypeTime.ini")
                                {
                                        config = fi;
                                        break;
                                }
                        }

                        if (config == null)
                        {
                                return;
                        }


                }
        }
}