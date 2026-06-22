using UnityEngine;

[System.Serializable]
public class Stats
{
    [Header("Weapon")]
    public int damage;
    public int recoil;
    public int reloadTime;
    public int damageFalloff;
    public int rateOfFire;
    public int spread;
    public int adsTime;
    public int sprintToFire;

    [Header("Player")]
    public int health;
    public int shields;
    public int walkSpeed;
    public int sprintSpeed;
}