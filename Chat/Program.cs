using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class TcpClientSample
{
    static void Connect(String server)
    {
        try
        {
            // Create a TcpClient. 
            int port = 13000;
            TcpClient client = new TcpClient(server, port);

            // Get a client stream for reading and writing.
            NetworkStream stream = client.GetStream();

            byte[] data = new byte[1024];
            string message;

            Console.Write("Please enter a username: ");
            message = Console.ReadLine();
            data = System.Text.Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);

            while (true)
            {
                Console.Write("Me:");
                message = Console.ReadLine();
                // Translate the passed message into ASCII and store it as a Byte array.
                data = System.Text.Encoding.ASCII.GetBytes(message);

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine($"Server: {responseData}");
            }

            // Close everything.
            stream.Close();
            client.Close();
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("ArgumentNullException: {0}", e);
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }

        Console.WriteLine("\n Press Enter to continue...");
        Console.Read();
    }

    static void Main(string[] args)
    {
        Connect("10.3.0.20");
    }
}

