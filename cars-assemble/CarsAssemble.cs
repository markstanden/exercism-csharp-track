using System;

static class AssemblyLine
{
    private const int BASE_PRODUCTION_SPEED = 221;

    public static double SuccessRate(int speed)
    {
        return speed switch
        {
            >= 1 and < 5 => 1,
            >= 5 and < 9 => 0.9,
            9            => 0.8,
            10           => 0.77,
            _            => 0
        };
    }

    public static double ProductionRatePerHour(int speed)
    {
        return BASE_PRODUCTION_SPEED * speed * SuccessRate(speed);
    }

    public static int WorkingItemsPerMinute(int speed)
        => (int) Math.Floor(ProductionRatePerHour(speed) / 60);
}