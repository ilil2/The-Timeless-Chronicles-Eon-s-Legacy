using System.Net;
using System.Net.Sockets;
using System.Threading;

using Serveur;

int[] ports = new[] {4242,4002};

for (int i = 0; i < ports.Length; i++)
{
    Serveur.Serveur.Prog ms = new Serveur.Serveur.Prog(ports[i]);
    Thread th = new Thread(global::Serveur.Serveur.MainProgram);
    th.Start(ms);
}