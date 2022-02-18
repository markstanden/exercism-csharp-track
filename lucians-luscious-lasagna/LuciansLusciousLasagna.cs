class Lasagna
{
    private const int Cooktime = 40;
    public int ExpectedMinutesInOven() => Cooktime;

    public int RemainingMinutesInOven(int currentCooktime) => Cooktime - currentCooktime;

    public int PreparationTimeInMinutes(int numberOfLayers) => 2 * numberOfLayers;

    public int ElapsedTimeInMinutes(int numberOfLayers, int currentCooktime)
    {
        return PreparationTimeInMinutes(numberOfLayers) + currentCooktime;
    }
}