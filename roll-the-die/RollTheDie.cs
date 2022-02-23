using System;

public class Player
{
    private Random rando = new Random();
    private const int MaxStrength = 100;
    private const int DieSides = 18;


    public int RollDie()
        => rando.Next(DieSides) + 1;


    public double GenerateSpellStrength()
        => rando.NextDouble() * MaxStrength;
}