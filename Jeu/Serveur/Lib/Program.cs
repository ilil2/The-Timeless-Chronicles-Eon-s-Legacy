// See https://aka.ms/new-console-template for more information

using Lib;

Console.WriteLine("Hello, World!");

/*
List<string> l1 = new List<string>() {"1","2","3"};
List<string> l2 = new List<string>() {"4","5","6"};
List<string> l3 = new List<string>() {"7","8","9"};
List<List<string>> l4 = new List<List<string>>() {l1,l2,l3};

Console.WriteLine(ListManupulation.ListofListNumberOf(l4,1,"8"));

Console.WriteLine(l4[1].Count);
l4[1] = new List<string> {"0","9"};
Console.WriteLine(l4[1].Count);
*/

Console.WriteLine(Hashing.CompressString("Hello"));
Console.WriteLine(Hashing.DecompressString(Hashing.CompressString("Hello:ça va;/!")));