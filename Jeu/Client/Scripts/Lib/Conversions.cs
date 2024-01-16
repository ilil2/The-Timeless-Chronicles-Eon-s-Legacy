using Godot;
using System;

namespace Lib;

public static class Conversions
{
	public static int AtoI(string? s)
	{
		int res = 0;

		foreach (var c in s)
		{
			if (char.IsNumber(c))
			{
				res += (int)c - 48;
				res *= 10;
			}
			else
			{
				throw new InvalidCastException("veuillez rentrer un nombre en base 10");
			}
		}

		res /= 10;
		return res;
	}
	
	public static float AtoF(string? s)
	{
		if (s == null || s == "")
		{
			return 0f;
		}
		{
			return 0f;
		}
		float res = 0;
		bool nega = false;
		
		if (s[0] == '-')
		{
			nega = true;
			s = s.Substring(1);
		}

		res = float.Parse(s);
		
		return nega?-res:res;
	}

	public static int BtoI(bool b)
	{
		return b?1:0;
	}
	
	public static float Pow(float n,int b)
	{
		if (b == 0)
		{
			return 1;
		}
		return n * Pow(n,b-1);
	}
}
