using System;

namespace RemotePortChanger
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Current Remote Desktop Connection port number: " + RegistrySettings.GetRemoteDesktopPortNumber());

                if (args.Length > 0)
                {
                    // If the remote desktop connection port number is not an integer inform the user.
                    if (!int.TryParse(args[0], out var portNumber))
                    {
                        Console.WriteLine("Please provide a valid Remote Desktop Connection port number.");
                        return;
                    }

                    RegistrySettings.SetRemoteDesktopPortNumber(portNumber);
                    Console.WriteLine($"Updated Remote Desktop Connection port number: {RegistrySettings.GetRemoteDesktopPortNumber()}.");
                }
                else
                {
                    Console.Write("Enter a new Remote Desktop Connection port number: ");

                    var input = Console.ReadLine();

                    if (!int.TryParse(input, out var portNumber))
                    {
                        Console.WriteLine("Please provide a valid Remote Desktop Connection port number.");
                        return;
                    }

                    RegistrySettings.SetRemoteDesktopPortNumber(portNumber);

                    Console.WriteLine($"Updated Remote Desktop Connection port number: {RegistrySettings.GetRemoteDesktopPortNumber()}.");
                    Console.Read();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Updating Remote Desktop Connection port failed. " + ex.Message);
            }
        }
    }
}