using System;

public partial class ICorentinException : Exception
{
	public ICorentinException()
	{
	}
	public ICorentinException(string message) : base(message)
	{
	}
	public ICorentinException(string message, Exception inner) : base(message, inner)
	{
	}
}
