using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    //the values to be stored
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

    public List<EquippedAttachment> equippedAttachments = new List<EquippedAttachment>();
    public List<string> unlockedAttachments = new List<string>();

    public string className;

    public string weaponName;

    public string primaryWeaponID;

    public string secondaryWeaponID;
}
//for storing what attachments type is on the weapon
[System.Serializable]
public class EquippedAttachment
{
    public AttachmentType socketType;
    public string attachmentID;
}