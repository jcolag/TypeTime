namespace TextManager
{
        using System;

        class MainClass
        {
                public static void Main(string[] args)
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
                        Console.WriteLine(errors.ToString() + " errors, " + dur.TotalSeconds + " seconds.");
                }
        }
}
