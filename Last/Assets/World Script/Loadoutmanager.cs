using TMPro;
using UnityEngine;

public class Loadoutmanager : MonoBehaviour
{
    public WeaponPreview[] weapons;
    public PrimaryWeapon[] primaryWeapons;

    public Transform primaryWeaponSpawn;
    public Transform secondaryWeaponSpawn;

    public TMP_Text classText;
    public TMP_Text weaponText;
    public TMP_Text skillText;

    public SkillData[] allSkills;

    GameObject currentPrimary;
    GameObject currentSecondary;

    void Start()
    {
        SetDefaults();
        SpawnWeapons();
        DisplaySkills();
        DisplayLoadoutInfo();
    }

    void SetDefaults()
    {
        SaveData save = SaveManager.Instance.Data;

        if (string.IsNullOrWhiteSpace(save.className))
            save.className = "Assault";

        if (string.IsNullOrWhiteSpace(save.primaryWeaponID))
            save.primaryWeaponID = "Assault Rifle";

        if (string.IsNullOrWhiteSpace(save.secondaryWeaponID))
            save.secondaryWeaponID = "Glock";
    }

    void SpawnWeapons()
    {
        SpawnPrimary();
        SpawnSecondary();
    }

    void SpawnPrimary()
    {
        if (currentPrimary != null)
            Destroy(currentPrimary);

        string className = SaveManager.Instance.Data.className;

        foreach (PrimaryWeapon weapon in primaryWeapons)
        {
            if (weapon.className != className)
                continue;

            currentPrimary = Instantiate(  weapon.weaponPrefab,  primaryWeaponSpawn);

            currentPrimary.transform.localPosition = Vector3.zero;
            currentPrimary.transform.localRotation = Quaternion.identity;

            ApplyAttachments(currentPrimary, true);

            Debug.Log("Spawned primary weapon for class: " + className);

            return;
        }

        Debug.LogError("No primary weapon assigned for class: " + className);
    }

    void SpawnSecondary()
    {
        GameObject prefab = WeaponDatabase.Instance.GetWeapon(  SaveManager.Instance.Data.secondaryWeaponID);

        if (prefab == null)
        {
            Debug.LogError("Secondary weapon not found.");
            return;
        }

        currentSecondary =  Instantiate(prefab, secondaryWeaponSpawn);

        currentSecondary.transform.localPosition = Vector3.zero;
        currentSecondary.transform.localRotation = Quaternion.identity;

        ApplyAttachments(currentSecondary, false);
    }

    void ApplyAttachments(GameObject weapon, bool primary)
    {
        Attachmentmanager manager =  weapon.GetComponent<Attachmentmanager>();

        if (manager == null)
            return;

        var list = primary ?  SaveManager.Instance.Data.equippedAttachments :  SaveManager.Instance.Data.secondAttachments;

        foreach (string id in list)
        {
            Attachment attachment =  DataBase.Instance.GetAttachment(id);

            if (attachment != null)
                manager.EquipAttachment(attachment);
        }
    }

    void DisplaySkills()
    {
        skillText.text = "Unlocked Skills\n\n";

        if (SaveManager.Instance.Data.unlockedSkills.Count == 0)
        {
            skillText.text += "None";
            return;
        }

        foreach (string id in SaveManager.Instance.Data.unlockedSkills)
        {
            foreach (SkillData skill in allSkills)
            {
                if (skill.skillID == id)
                {
                    skillText.text += "• " + skill.skillName + "\n";
                    break;
                }
            }
        }
    }

    void DisplayLoadoutInfo()
    {
        classText.text = "Class : " +  SaveManager.Instance.Data.className;

        weaponText.text =  "Primary : " +   SaveManager.Instance.Data.primaryWeaponID +  "\nSecondary : " +  SaveManager.Instance.Data.secondaryWeaponID;
    }
}
