using UnityEngine;

public static class ClassGenerator
{
    //where the class swiching and creation happens the highest total stats related to each class
    public static string GetClass(Stats stats)
    {
        float assault = stats.damage * 2f + stats.rateOfFire * 2f + stats.recoil + stats.reloadTime + stats.adsTime;

        float heavy = stats.health * 2f + stats.shields * 2f + stats.damageFalloff + stats.recoil;

        float recon = stats.damageFalloff * 2f +  stats.adsTime * 2f + stats.spread + stats.walkSpeed + stats.sprintSpeed;

        float support = stats.reloadTime * 2f +stats.rateOfFire + stats.walkSpeed + stats.health + stats.shields;

        float engineer = stats.walkSpeed * 2f + stats.sprintSpeed * 2f + stats.reloadTime + stats.recoil + stats.sprintToFire;

        float demolitionist =  stats.damage * 2f +  stats.damageFalloff * 2f +   stats.recoil +   stats.reloadTime;

        float scout = stats.adsTime * 2f +  stats.spread * 2f +   stats.sprintSpeed * 2f + stats.walkSpeed +  stats.damageFalloff;

        float driver = stats.walkSpeed * 2f + stats.sprintSpeed * 2f +  stats.sprintToFire * 2f + stats.reloadTime + stats.health;


        float highest = Mathf.Max( assault, heavy, recon, support, engineer, demolitionist, scout, driver);

        if (highest == heavy) return "Heavy";
        if (highest == recon) return "Recon";
        if (highest == support) return "Support";
        if (highest == engineer) return "Engineer";
        if (highest == demolitionist) return "Demolitionist";
        if (highest == scout) return "Scout";
        if (highest == driver) return "Driver";

        return "Assault";
    }
}
