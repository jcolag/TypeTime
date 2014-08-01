// <copyright file="MainClass.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace TextManager
{
        using System;

        /// <summary>
        /// Main class.
        /// </summary>
        public static class MainClass
        {
                /// <summary>
                /// The entry point of the program, where the program control starts and ends.
                /// </summary>
                private static void Main()
                {
                        var rt = new RandomTarget(5);
                        var time = new Timing(4);
                        string input;
                        int errors;
                        TimeSpan dur;

                        time.Wait();
                        rt.SetString();
                        Console.WriteLine(rt.Target);
                        time.SetStart();
                        input = Console.ReadLine();
                        time.SetEnd();
                        errors = rt.CheckString(input);
                        dur = time.Duration;
                        Console.WriteLine(errors.ToString() + " errors, " +
                                dur.TotalSeconds.ToString() + " seconds.");
                }
        }
}