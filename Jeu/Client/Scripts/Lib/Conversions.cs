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
		float res = 0;
		bool compteur = true;
		int count = 0;
		bool nega = false;
		
		if (s[0] == '-')
		{
			nega = true;
			s = s.Substring(1);
		}

		foreach (var c in s)
		{
			if (char.IsNumber(c))
			{
				res += c - 48;
				res *= 10;
			}
			else if (c == ',')
			{
				compteur = false;
			}
			else
			{
				throw new InvalidCastException("veuillez rentrer un nombre en base 10");
			}

			if (compteur)
			{
				count += 1;
			}
		}

		res /= 10f;
		res /= Pow(10,count);
		return nega?-res:res;
	}

	public static int BtoI(bool b)
	{
		return b?1:0;
	}
	
	public static float Pow(int n,int b)
	{
		if (b == 0)
		{
			return 1;
		}
		return n * Pow(n,b-1);
	}
}
