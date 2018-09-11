using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;

public class TcpListenerSample
{
    private class Client
    {
        public String username {get; set;}
        public TcpClient socket {get; set;}
        public NetworkStream stream {get; set;}
    }

    private class Message
    {
        public String username {get; set;}
        public String messsage {get; set;}
    }

    private class Server
    {
        // List of all connected clients
        private List<Client> clients = new List<Client>();

        // Queue to store messages from clients until they sent to all clients
        private ConcurrentQueue<Message> messages = new ConcurrentQueue<Message>();


    }

    

    static void Main(string[] args)
    {
        try
        {
            // set the TcpListener on port 13000
            int port = 13000;
            IPAddress localAddr = IPAddress.Parse("10.3.0.20");
            TcpListener server = new TcpListener(localAddr, port);

            // Start listening for client requests
            server.Start();

            // Buffer for reading data
            byte[] bytes = new byte[1024];

            // open new thread to read messages from the client and sending to 
            // all other clients
            Thread reader = new Thread(new ThreadStart(Read));
            Thread writer = new Thread(new ThreadStart(WriteToAll));
            reader.Start();
            writer.Start();

            //Enter the listening loop
            while (true)
            {
                TcpClient socket = server.AcceptTcpClient();

                Client client = new Client();
                client.socket = socket;
                client.stream = socket.GetStream();

                // get clients username
                int i = client.stream.Read(bytes, 0, bytes.Length);
                client.username = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                clients.Add(client); 
            }

            reader.Interrupt();
            writer.Interrupt();
        }
        catch (SystemException e)
        {
            Console.WriteLine("SystemException: {0}", e);
        }     
    }
        
    public static void Read()
    {
        while (true)
        {
            if (clients.)
        }
    }



                while (true)
                {
                    i = stream.Read(bytes, 0, bytes.Length);
                    // Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine(String.Format($"{username}: {data}"));

                    Console.Write("Me: ");
                    message = Console.ReadLine();
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);

                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
                    //Console.WriteLine(String.Format("Sent: {0}", data));



                }

                // Shutdown and end connection
                client.Close();
            }
        }
        


        Console.WriteLine("Hit enter to continue...");
        Console.Read();
    }

}

