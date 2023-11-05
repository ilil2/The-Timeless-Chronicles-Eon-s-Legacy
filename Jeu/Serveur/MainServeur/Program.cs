// See https://aka.ms/new-console-template for more information

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Serveur;

Process p1 = new Process();
p1.StartInfo.FileName = "bash";
p1.StartInfo.Arguments = "exec.sh";
p1.Start(); /* Cette instruction ouvre un invite de commande n°2 */

/*static void Main(string[] args)
{
    MainServeur ms = new MainServeur();
    ms.MainProgram();
}

{
    Main(null);
}*/