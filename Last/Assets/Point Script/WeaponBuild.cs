using UnityEngine;

public static class WeaponBuild
{
    //applying the stats modifications to weapons in the point stats weapons scene
    public static Profile Build(Stats stats)
    {
       Profile  gun = new Profile();

        gun.damage = 10 + (stats.damage * 2);

        gun.recoil =   Mathf.Clamp(   5.0f - (stats.recoil * 0.05f),    0.25f,   10f);

        gun.reloadTime =  Mathf.Clamp(    2f - (stats.reloadTime * 0.08f),   0.5f,    5f);

        gun.range =  50 + (stats.damageFalloff * 5);

        gun.fireRate =  10 + (stats.rateOfFire * 30);

        gun.spread =   Mathf.Clamp(   10f - (stats.spread * 0.15f),    0.25f,      15f);

        gun.adsTime =    Mathf.Clamp(  0.25f - (stats.adsTime * 0.01f),  0.05f,  1f);

        gun.sprintToFire =   Mathf.Clamp(    0.3f - (stats.sprintToFire * 0.01f),   0.05f,     1f);

        gun.health = (stats.health * 10);
        gun.shields = (stats.shields * 10);

        gun.walkSpeed = 5 + (stats.walkSpeed * 0.25f);
        gun.sprintSpeed = 8 + (stats.sprintSpeed * 0.35f);

        gun.crouchSpeed = 2.5f;

        gun.jumpHeight = 1.5f;

        gun.gravity = -20f;

        gun.normalFOV = 100;

        gun.adsFOV = 50;

        gun.adsSpeed = 10;

        return gun;
    }
}
