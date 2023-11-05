// See https://aka.ms/new-console-template for more information

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Serveur;

int[] ports = new[] {4242,4002};

for (int i = 0; i < ports.Length; i++)
{
    StreamWriter sw = new StreamWriter("port.txt"); 
    sw.Write(ports[i]);
    sw.Close();
    
    Process p1 = new Process();
    p1.StartInfo.FileName = "bash";
    p1.StartInfo.Arguments = "exec.sh";
    p1.Start(); /* Cette instruction ouvre un invite de commande n°2 */
    
    System.Threading.Thread.Sleep(1000);
}



/*static void Main(string[] args)
{
    MainServeur ms = new MainServeur();
    ms.MainProgram();
}

{
    Main(null);
}*/