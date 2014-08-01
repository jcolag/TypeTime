// <copyright file="RandomTarget.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace TextManager
{
        using System;

        /// <summary>
        /// Random target string.
        /// </summary>
        public class RandomTarget
        {
                /// <summary>
                /// The letters for generating the random string.
                /// </summary>
                private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

                /// <summary>
                /// The random number generator.
                /// </summary>
                private readonly Random rand = new Random();

                /// <summary>
                /// The length of the target string.
                /// </summary>
                private int targetLength = 5;

                /// <summary>
                /// The target string.
                /// </summary>
                private string target;

                /// <summary>
                /// Initializes a new instance of the <see cref="TextManager.RandomTarget"/> class.
                /// </summary>
                public RandomTarget()
                {
                }

                /// <summary>
                /// Initializes a new instance of the <see cref="TextManager.RandomTarget"/> class.
                /// </summary>
                /// <param name="l">The length of the target string.</param>
                public RandomTarget(int l)
                {
                        this.targetLength = l;
                }

                /// <summary>
                /// Gets the target string.
                /// </summary>
                /// <value>The target string.</value>
                public string Target
                {
                        get
                        {
                                return this.target;
                        }
                }

                /// <summary>
                /// Sets the target string.
                /// </summary>
                public void SetString()
                {
                        this.target = this.MakeString(null);
                }

                /// <summary>
                /// Checks the input string against the target.
                /// Uses the Levenshtein Distance to determine how closely the strings match.
                /// </summary>
                /// <returns>The size of the difference between the strings.</returns>
                /// <param name="test">The input string.</param>
                public int CheckString(string test)
                {
                        if (test == null)
                        {
                                throw new ArgumentNullException("test");
                        }

                        string input = test.Trim().ToUpper(System.Globalization.CultureInfo.CurrentCulture);
                        int[,] matrix = new int[this.target.Length + 1, input.Length + 1];

                        for (int i = 0; i <= this.target.Length; i++)
                        {
                                matrix[i, 0] = i;
                        }

                        for (int i = 0; i <= input.Length; i++)
                        {
                                matrix[0, i] = i;
                        }

                        for (int i = 1; i <= this.target.Length; i++)
                        {
                                char c1 = this.target[i - 1];
                                for (int j = 1; j <= input.Length; j++)
                                {
                                        char c2 = input[j - 1];
                                        if (c1 == c2)
                                        {
                                                matrix[i, j] = matrix[i - 1, j - 1];
                                        }
                                        else
                                        {
                                                int delete = matrix[i - 1, j] + 1;
                                                int insert = matrix[i, j - 1] + 1;
                                                int substitute = matrix[i - 1, j - 1] + 1;
                                                int minimum = delete;
                                                if (insert < minimum)
                                                {
                                                        minimum = insert;
                                                }

                                                if (substitute < minimum)
                                                {
                                                        minimum = substitute;
                                                }

                                                matrix[i, j] = minimum;
                                        }
                                }
                        }

                        return matrix[this.target.Length, input.Length];
                }

                /// <summary>
                /// Generates the target string.
                /// </summary>
                /// <returns>The string.</returns>
                /// <param name="length">Length of the generated string.</param>
                private string MakeString(int? length)
                {
                        string result = string.Empty;
                        int len = this.targetLength;
                        if (length != null)
                        {
                                len = (int)length;
                        }

                        for (int count = 0; count < len; count++)
                        {
                                result += RandomTarget.Letters[this.rand.Next(0, 25)].ToString();
                        }

                        return result;
                }
        }
}