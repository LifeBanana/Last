using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{

    public int damage;
    public int recoil;
    public int reload;
    public int damageFalloff;
    public int fireRate;
    public int spread;
    public int ads;
    public int sprintFire;

    public int health;
    public int shields;
    public int walkSpeed;
    public int sprintSpeed;


    public int skillPoints;

    public List<string> unlockedSkills = new List<string>();

    public List<string> equippedAttachments = new List<string>();
    public List<string> secondAttachments = new List<string>();
    public List<string> unlockedAttachments = new List<string>();

    public int playerLevel;

    public int experience;


    public string className;

    public string weaponName;

    public string primaryWeaponID;

    public string secondaryWeaponID;
}
