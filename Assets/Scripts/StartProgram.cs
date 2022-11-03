using System;

namespace Encryption_Program;

class StartProgram
{
    public static bool Finished { get; set; }
    public static string InPath { get; } = $"{Directory.GetCurrentDirectory()}\\Assets\\Input Output\\Input.txt";
    public static string OutPath { get; } = $"{Directory.GetCurrentDirectory()}\\Assets\\Input Output\\Output.txt";
    public static string[] EncodeList { get; set; } =
    {
        "┌",
        "┍",
        "┎",
        "˦",
        "├",
        "˨",
        "└",
        "┗",
        "┕",
        "┏",
        "˧",
        "┖",
        "⎾"
    };

    public static void Main(string[] args)
    {
        Console.Clear();

        Finished = true;
        StatusUpdate.Run = false;
        Thread TaskUpdate = new(() => StatusUpdate.TaskStatus());
        TaskUpdate.Start();

        Console.Title = "Encryption Program";
        Console.CursorVisible = true;
        Console.ForegroundColor = ConsoleColor.White;
        while (true)
        {
            if (Finished)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(
                    "\r\nWould you like to encrypt or decrypt or exit? (Type E/D/X) (Type I/O to open Input/Output file)"
                );
                switch (Console.ReadLine()!.ToUpper())
                {
                    case "E":
                        Console.Clear();
                        StatusUpdate.IsEncoding = true;
                        Encryption.Encrypt();
                        break;
                    case "D":
                        Console.Clear();
                        StatusUpdate.IsEncoding = false;
                        Decryption.Decrypt();
                        break;
                    case "I":
                        System.Diagnostics.Process.Start(
                            new System.Diagnostics.ProcessStartInfo(InPath)
                            {
                                UseShellExecute = true
                            }
                        );
                        Console.Clear();
                        break;
                    case "O":
                        System.Diagnostics.Process.Start(
                            new System.Diagnostics.ProcessStartInfo(OutPath)
                            {
                                UseShellExecute = true
                            }
                        );
                        Console.Clear();
                        break;
                    case "X":
                        Console.Clear();
                        return;
                }
            }
        }
    }
}
