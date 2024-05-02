using Godot;
using System;
using System.Diagnostics;

public abstract class Potion
{
	public int ID;
	public string img;
	public Stopwatch timer = new Stopwatch();
	public abstract void UsePotion();
}
