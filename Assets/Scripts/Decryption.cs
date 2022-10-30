namespace Encryption_Program;

using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Text;

public class Decryption
{
    public static void Decrypt()
    {
        Process.Start(new ProcessStartInfo(StartProgram.InPath) { UseShellExecute = true });

        Console.WriteLine(
            "Enter a value in the input file to be decrypted (press enter when you are done or and press x to exit))"
        );
        if (string.Compare(Console.ReadLine(), "x") == 0)
        {
            Console.Clear();
            return;
        }

        string? inputValue = File.ReadAllText(StartProgram.InPath);
        string decryptedValue;
        List<string> DecryptedInputList = new();
        List<string> DecryptedRandomNumberList = new();
        List<byte> unDecodedValue = new();
        StringBuilder decodedValue = new();
        Stopwatch executionTimer = new();
        StatusUpdate.Run = true;

        StatusUpdate.Stage = 0;
        StatusUpdate.TotalLength = inputValue.Length;
        if (string.Compare(inputValue[..1], ">") == 0)
        {
            inputValue = inputValue.Remove(0, 1);
            for (int i = 0; i < inputValue.Length; i++)
            {
                if (string.Compare(inputValue.Substring(i, 1), StartProgram.EncodeList[10]) == 0)
                {
                    decodedValue.Append(",");
                }
                else if (
                    string.Compare(inputValue.Substring(i, 1), StartProgram.EncodeList[11]) == 0
                )
                {
                    decodedValue.Append(";");
                }
                else if (
                    string.Compare(inputValue.Substring(i, 1), StartProgram.EncodeList[12]) == 0
                )
                {
                    decodedValue.Append("-");
                }
                else
                {
                    decodedValue.Append(
                        Array.IndexOf(StartProgram.EncodeList, inputValue.Substring(i, 1))
                    );
                }
                StatusUpdate.Index = i;
            }
        }
        else
        {
            decodedValue.Append(inputValue);
            foreach (char i in decodedValue.ToString())
            {
                if (true) { }
            }
        }

        StatusUpdate.Stage = 1;

        try
        {
            string[] tempVal = decodedValue.ToString().Split(";");
            DecryptedInputList = tempVal[0].Split(",").ToList();
            DecryptedRandomNumberList = tempVal[1].Split(",").ToList();
        }
        catch (System.Exception)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid Code");
            StatusUpdate.Run = false;
            StartProgram.Finished = true;
            return;
        }

        StatusUpdate.Stage = 2;
        for (int i = 0; i < DecryptedInputList.Count; i++)
        {
            if (string.Compare(DecryptedRandomNumberList[i], "0") == 0)
            {
                unDecodedValue.Add(0);
                continue;
            }
            unDecodedValue.Add(
                (byte)(int.Parse(DecryptedInputList[i]) / int.Parse(DecryptedRandomNumberList[i]))
            );
            StatusUpdate.Index = i;
        }

        decryptedValue = Encoding.UTF8.GetString(unDecodedValue.ToArray());
        StatusUpdate.FinalOutput = decryptedValue;

        StatusUpdate.Run = false;
    }
}
