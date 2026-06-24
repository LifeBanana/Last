using UnityEngine;

public static class ClassGenerator
{
    public static string GetClass(Stats stats)
    {
        float assaultScore =   stats.damage +  stats.rateOfFire +  stats.recoil;

        float heavyScore =  stats.health +    stats.shields;

        float reconScore =  stats.adsTime +    stats.spread +   stats.sprintSpeed;

        float supportScore =   stats.reloadTime +   stats.walkSpeed;

        float engineerScore =    stats.walkSpeed +   stats.sprintSpeed +  stats.recoil;

        float highest =  Mathf.Max(    assaultScore,   heavyScore,  reconScore,  supportScore,    engineerScore);

        if (highest == heavyScore)
            return "Heavy";

        if (highest == reconScore)
            return "Recon";

        if (highest == supportScore)
            return "Support";

        if (highest == engineerScore)
            return "Engineer";

        return "Assault";
    }
}
