using System.Linq;

public static class ResistorColor
{
    public static string[] Resistors = {"black", "brown", "red", "orange", "yellow", "green", "blue", "violet", "grey", "white"};


    public static int ColorCode(string color)
        => Resistors.ToList()
                    .IndexOf(color);


    public static string[] Colors()
        => Resistors;
}