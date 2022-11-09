namespace Encryption_Program;

using System;
using System.IO;
using System.Text;
using System.Diagnostics;

public class Encryption
{
    public static void Encrypt()
    {
        int encodingLevel = 1;
        string path = string.Empty;
        bool isEncode = false;
        StringBuilder unEncodedValue = new();
        StringBuilder encodedValue = new();
        Random randomNumber = new();

        Console.WriteLine(
            "\r\nEnter a value in the input file to be encrypted or type F to encrypt a file (press enter when you are done or press X to exit )\r\n"
        );
        Process.Start(new ProcessStartInfo(StartProgram.InPath) { UseShellExecute = true });

        switch (Console.ReadLine()!.ToUpper())
        {
            case "F":
                do
                {
                    Console.WriteLine("\r\nEnter the file path here or press X to exit");
                    path = Console.ReadLine()!.Replace("\"", string.Empty);
                    if (string.Compare(path, "x") == 0)
                    {
                        Console.Clear();
                        return;
                    }
                } while (!File.Exists(@path));
                break;
            case "X":
                Console.Clear();
                return;
            default:
                path = StartProgram.InPath;
                break;
        }

        byte[] inputValue = Encoding.UTF8.GetBytes(File.ReadAllText(@path));
        StatusUpdate.TotalLength = inputValue.Length;

        Console.WriteLine("Do you want to do encoding (slower) (type Y/N)");

        isEncode = (string.Compare(Console.ReadLine()!.ToUpper(), "Y") == 0);
        if (isEncode)
        {
            int parsedInput;
            do
            {
                Console.WriteLine("\r\nEnter encoding level");
            } while (!int.TryParse(Console.ReadLine(), out parsedInput));
            encodingLevel = StatusUpdate.EncodingLevel = parsedInput;
        }

        Console.Clear();
        Console.CursorVisible = false;

        StatusUpdate.Run = true;
        for (int j = 0; j < encodingLevel; j++)
        {
            long[] randomNumberList = new long[inputValue.Length];
            long[] multipliedInputList = new long[inputValue.Length];

            StatusUpdate.Stage = 0;
            for (int i = 0; i < inputValue.Length; i++)
            {
                randomNumberList[i] = randomNumber.Next(-9999, 9999);
                multipliedInputList[i] = inputValue[i] * randomNumberList[i];
                StatusUpdate.Index = i;
            }

            StatusUpdate.Stage = 1;
            unEncodedValue.Append($"{string.Join(",", multipliedInputList)};");
            unEncodedValue.Append($"{string.Join(",", randomNumberList)};");
            StatusUpdate.Stage = 2;

            StatusUpdate.TotalLength = unEncodedValue.Length;
            for (int i = 0; i < unEncodedValue.Length; i++)
            {
                switch (unEncodedValue.ToString(i, 1))
                {
                    case ",":
                        {
                            encodedValue.Append(StartProgram.EncodeList[10]);
                            break;
                        }
                    case ";":
                        {
                            encodedValue.Append(StartProgram.EncodeList[11]);
                            break;
                        }
                    case "-":
                        {
                            encodedValue.Append(StartProgram.EncodeList[12]);
                            break;
                        }
                    default:
                        {
                            encodedValue.Append(
                                StartProgram.EncodeList[int.Parse(unEncodedValue.ToString(i, 1))]
                            );
                            break;
                        }
                }
                StatusUpdate.Index = i;
            }

            inputValue = Encoding.UTF8.GetBytes(encodedValue.ToString());
            StatusUpdate.CurrentEncodingLevel = j + 1;
        }

        if (isEncode)
        {
            StatusUpdate.FinalOutput = encodedValue.ToString();
        }
        else
        {
            encodedValue.Insert(0, "\'");
            StatusUpdate.FinalOutput = unEncodedValue.ToString();
        }
        StatusUpdate.Run = false;
    }
}
