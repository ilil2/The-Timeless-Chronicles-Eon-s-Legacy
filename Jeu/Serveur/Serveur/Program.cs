using Lib;

static void Main(string?[]? args)
{
    int[] ports = new[] {4242,4002};

    StreamReader sr = new StreamReader("../MainServeur/port.txt");
    string? port = sr.ReadLine();
    sr.Close();
    Console.WriteLine(port);
    string[] line = port.Split(';');
    global::Serveur.Serveur ms = new global::Serveur.Serveur(); 
    ms.MainProgram(Conversion.AtoI(line[0]),Conversion.AtoI(line[1]));
}

{
    Main(null);
}

