
namespace TLS
{
    interface Messager
    {
        void Send(string message);
        string Recieve();
    }
}
