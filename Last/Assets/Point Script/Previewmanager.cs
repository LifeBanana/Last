using UnityEngine;

public class Previewmanager : MonoBehaviour
{
    public static Previewmanager Instance;

    [Header("Weapon Database")]
    public WeaponPreview[] weapons;

    public Transform primarySpawn;

    public Transform secondarySpawn;

    private GameObject currentPrimary;

    private GameObject currentSecondary;

    void Awake()
    {
        Instance = this;
    }

    public void ShowWeapon(string className)
    {
        if (currentPrimary != null)
            Destroy(currentPrimary);

        if (currentSecondary != null)
            Destroy(currentSecondary);

        foreach (var weapon in weapons)
        {
            if (weapon.weaponID != className)
                continue;

            currentPrimary =
                Instantiate(
                    weapon.primaryWeaponPrefab,
                    primarySpawn);

            currentPrimary.transform.localPosition = Vector3.zero;
            currentPrimary.transform.localRotation = Quaternion.identity;

            currentSecondary = Instantiate(weapon.secondaryWeaponPrefab, secondarySpawn);

            currentSecondary.transform.localPosition = Vector3.zero;
            currentSecondary.transform.localRotation = Quaternion.identity;

            ApplyAttachments(currentPrimary, true);

            ApplyAttachments(currentSecondary, false);

            return;
        }

        Debug.LogWarning("No preview found for " + className);
    }

    void ApplyAttachments(GameObject weapon, bool primary)
    {
        Attachmentmanager manager = weapon.GetComponent<Attachmentmanager>();

        if (manager == null)
            return;

        var attachments = primary ? SaveManager.Instance.Data.equippedAttachments : SaveManager.Instance.Data.secondAttachments;

        foreach (string id in attachments)
        {
            Attachment attachment = DataBase.Instance.GetAttachment(id);

            manager.EquipAttachment(attachment);
        }
    }
}

