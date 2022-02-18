using System;

class RemoteControlCar
{
    private int distance = 0;
    private int batteryPercentage = 100;

    public static RemoteControlCar Buy()
    {
        return new RemoteControlCar();
    }

    public string DistanceDisplay()
    {
        return $"Driven {this.distance} meters";
    }

    public string BatteryDisplay()
    {
        return batteryPercentage == 0 ? "Battery empty" : $"Battery at {this.batteryPercentage}%";
    }

    public void Drive()
    {
        if (batteryPercentage <= 0) return;

        batteryPercentage--;
        distance += 20;
    }
}