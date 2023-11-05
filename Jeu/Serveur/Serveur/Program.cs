using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.IO;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Serveur;
using Serveur = Serveur.Serveur;



for (int i = 0; i < 1; i++)
{
    /*Serveur.Serveur.Prog ms = new Serveur.Serveur.Prog(ports[i]);
    Thread th = new Thread(Serveur.Serveur.MainProgram);
    th.Start(ms);*/
}

static void Main(string[] args)
{
    int[] ports = new[] {4242,4002};

    StreamReader sr = new StreamReader("port.txt");
    string? port = sr.ReadLine();
    sr.Close();
    
    global::Serveur.Serveur ms = new global::Serveur.Serveur(); 
    ms.MainProgram(global::Serveur.Serveur.AtoI(port));
}

