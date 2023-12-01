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
		float count = 0;

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

		res /= 10;
		res /= count;
		return res;
	}

	public static int BtoI(bool b)
	{
		return b?1:0;
	}
}
