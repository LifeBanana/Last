using UnityEngine;

public static class WeaponBuild
{
    public static WeaponProfile Build(Stats stats)
    {
        WeaponProfile gun = new WeaponProfile();

        gun.damage = 30 + (stats.damage * 2);

        gun.recoil =   Mathf.Clamp(   1.0f - (stats.recoil * 0.05f),    0.25f,   3f);

        gun.reloadTime =  Mathf.Clamp(    2f - (stats.reloadTime * 0.08f),   0.5f,    5f);

        gun.range =  50 + (stats.damageFalloff * 5);

        gun.fireRate =  600 + (stats.rateOfFire * 30);

        gun.spread =   Mathf.Clamp(   3f - (stats.spread * 0.15f),    0.25f,      8f);

        gun.adsTime =    Mathf.Clamp(  0.25f - (stats.adsTime * 0.01f),  0.05f,  1f);

        gun.sprintToFire =   Mathf.Clamp(    0.3f - (stats.sprintToFire * 0.01f),   0.05f,     1f);

        return gun;
    }
}
