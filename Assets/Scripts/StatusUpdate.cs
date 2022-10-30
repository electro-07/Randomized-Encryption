namespace Encryption_Program;

using System;
using System.IO;
using System.Diagnostics;
using System.Threading;

public class StatusUpdate
{
    public static int TotalLength { private get; set; }
    public static int Stage { private get; set; }
    public static int Index { private get; set; }
    public static int EncodingLevel { private get; set; }
    public static int CurrentEncodingLevel { private get; set; }
    public static string? FinalOutput { private get; set; }
    public static bool IsEncoding { private get; set; }
    public static bool Run { private get; set; }

    public static void TaskStatus()
    {
        string status = "";
        string elapsedTime;
        Stopwatch stopwatch = new();
        stopwatch.Start();

        while (true)
        {
            if (Run)
            {
                StartProgram.Finished = false;

                if (IsEncoding)
                {
                    while (Run)
                    {
                        switch (Stage)
                        {
                            case 0:
                                status =
                                    $"Multiplying digits {Index}/{TotalLength} ({Math.Round(Index / (decimal)(TotalLength) * 100, 2)}%)";
                                break;
                            case 1:
                                status = "Joining values";
                                break;
                            case 2:
                                status =
                                    $"Encoding data {Index}/{TotalLength} ({Math.Round(Index / (decimal)TotalLength * 100, 2)}%)";
                                break;
                        }

                        elapsedTime = $"{stopwatch.Elapsed:dd':'hh':'mm':'ss':'ff}";

                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Console.Write(
                            $"{status}\r\n\r\nEncoding stage: {CurrentEncodingLevel}/{EncodingLevel}\r\n\r\n{elapsedTime}"
                        );
                        Thread.Sleep(50);
                    }
                }
                else
                {
                    while (Run)
                    {
                        switch (Stage)
                        {
                            case 0:
                                status =
                                    $"Dividing digits {Index}/{TotalLength} ({Math.Round(Index / (decimal)(TotalLength) * 100, 2)}%)";
                                break;
                            case 1:
                                status = "Splitting values";
                                break;
                            case 2:
                                status =
                                    $"Decoding data {Index}/{TotalLength} ({Math.Round(Index / (decimal)TotalLength * 100, 2)}%)";
                                break;
                        }

                        elapsedTime = $"{stopwatch.Elapsed:dd':'hh':'mm':'ss':'ff}";

                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Console.Write($"{status}\r\n\r\n{elapsedTime}");
                        Thread.Sleep(50);
                    }
                }

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                File.WriteAllText(StartProgram.OutPath, FinalOutput?.ToString());
                Process.Start(
                    new ProcessStartInfo(StartProgram.OutPath) { UseShellExecute = true }
                );
                Console.WriteLine("Output is in the output file");
                Console.WriteLine(
                    $"\r\nEncryption time: {stopwatch.Elapsed:dd':'hh':'mm':'ss':'ffff}"
                );
                StartProgram.Finished = true;
            }
        }
    }
}
