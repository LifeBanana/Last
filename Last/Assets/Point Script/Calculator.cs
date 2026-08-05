using UnityEngine;

public static class Calculator
{
    public static int CalculateCost(Stats stats)
    {
        int cost = 0;

        cost += Cost(stats.damage, 2);
        cost += Cost(stats.recoil, 2);
        cost += Cost(stats.reloadTime, 1);
        cost += Cost(stats.damageFalloff, 1);
        cost += Cost(stats.rateOfFire, 2);
        cost += Cost(stats.spread, 2);
        cost += Cost(stats.adsTime, 1);
        cost += Cost(stats.sprintToFire, 1);

        cost += Cost(stats.health, 3);
        cost += Cost(stats.shields, 3);
        cost += Cost(stats.walkSpeed, 2);
        cost += Cost(stats.sprintSpeed, 2);

        return cost;
    }

    private static int Cost(int value, int multiplier)
    {
        return value * multiplier;
    }
}