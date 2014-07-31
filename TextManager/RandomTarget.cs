namespace TextManager
{
        using System;

        public class RandomTarget
        {
                private const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

                private int targetLength = 5;

                private readonly Random rand = new Random();

                private string target;

                public RandomTarget()
                {
                }

                public RandomTarget(int l)
                {
                        targetLength = l;
                }

                public string Target
                {
                        get
                        {
                                return this.target;
                        }
                }

                public void SetString()
                {
                        this.target = this.MakeString(null);
                }

                public int CheckString(string test)
                {
                        if (test == null)
                        {
                                throw new ArgumentNullException("test");
                        }

                        string input = test.Trim().ToUpper();
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
                                char c1 = target[i - 1];
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
                                result += RandomTarget.letters[rand.Next(0, 25)];
                        }

                        return result;
                }
        }
}

