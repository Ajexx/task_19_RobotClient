using System.Net;
using System.Net.Sockets;
using System.Text;

int port = 5000;

Socket clientSocket = new Socket
    (
     AddressFamily.InterNetwork,
     SocketType.Stream,
     ProtocolType.Tcp
     );

var Ipendpoint = new IPEndPoint(IPAddress.Loopback, port);
clientSocket.Connect(Ipendpoint);

Console.WriteLine("-----Menu-----");
Console.WriteLine("UP");
Console.WriteLine("DOWN");
Console.WriteLine("LEFT");
Console.WriteLine("RIGHT");
Console.WriteLine("POSITION");
Console.WriteLine("EXIT");
Console.Write("Choice: ");

bool running = true;

byte[] buffer = new byte[1024];

while (running)
{
    string? choice = Console.ReadLine();
    clientSocket.Send(Encoding.UTF8.GetBytes(choice));
    int received = clientSocket.Receive(buffer);

    if (received == 0)
    {
        Console.WriteLine("Server closed the connection.");
        break;
    }

    var ecdn = Encoding.UTF8.GetString(buffer, 0, received);

    Console.WriteLine(ecdn);

    if (choice.Trim().ToUpper() == "EXIT")
        break;
}

clientSocket.Close();
