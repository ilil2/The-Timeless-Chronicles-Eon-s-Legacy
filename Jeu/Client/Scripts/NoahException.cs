using System;

//NECESSAIRE
public partial class NoahException : Exception
{
	public NoahException()
	{
		//RATIO
	}
	public NoahException(string message) : base(message)
	{
		//RATIO + message
	}
	public NoahException(string message, Exception inner) : base(message, inner)
	{
		//RATIO + message + inner (C quoi ce truc ???)
	}
}
