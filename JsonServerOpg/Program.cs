using JsonServerOpg;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

Console.WriteLine("TCP server");

TcpListener listener = new TcpListener(IPAddress.Any, 7);

listener.Start();

while (true)
{


    TcpClient socket = listener.AcceptTcpClient();
    IPEndPoint clientEndPoint = socket.Client.RemoteEndPoint as IPEndPoint;
    Console.WriteLine("Client connected: " + clientEndPoint.Address);


    Task.Run(() => HandleClient(socket));


}

void HandleClient(TcpClient socket)
{

    NetworkStream ns = socket.GetStream();
    StreamReader reader = new StreamReader(ns);
    StreamWriter writer = new StreamWriter(ns);

    while (socket.Connected)
    {

        string? message = reader.ReadLine();
        Command? command = null;
        Console.WriteLine("test: " + message);

        if (message.ToLower() == "stop")
        {
            writer.WriteLine("Server has been stopped");
        }
        else
        {

            try
            {
                command = JsonSerializer.Deserialize<Command>(message);
            }
            catch (Exception ex)
            {
                writer.WriteLine("Invalid command");
            }

            if (command != null)
            {
                switch (command.Method.ToLower())
                {
                    case "random":
                        Random rnd = new Random();
                        int random = rnd.Next(command.Number1, command.Number2);
                        string jsonthing = JsonSerializer.Serialize(random);
                        writer.WriteLine(jsonthing);
                        break;
                    case "add":
                        int sum = command.Number1 + command.Number2;
                        writer.WriteLine(sum);
                        break;
                    case "subtract":
                        int result = command.Number1 - command.Number2;
                        writer.WriteLine(result);
                        break;
                    default:
                        writer.WriteLine("Invalid command");
                        break;
                }
            }
        }

        writer.Flush();


        if (message == "stop")
        {
            socket.Close();
        }
    }


}
