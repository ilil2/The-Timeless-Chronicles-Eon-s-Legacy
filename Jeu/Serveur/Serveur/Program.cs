// See https://aka.ms/new-console-template for more information

using Serveur;

int[] ports = new[] {4242,4002};
int nbr_serveur = 0;

static void Main(string[] args)
{
    MainServeur ms = new MainServeur();
    ms.MainProgram();
}

{
    Main(null);
}