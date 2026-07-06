using UnityEngine;

public class Previewmanager : MonoBehaviour
{
    public static Previewmanager Instance;

    [Header("Weapon Database")]
    public WeaponPreview[] weapons;

    public Transform spawnPoint;

    private GameObject currentWeapon;

    void Awake()
    {
        Instance = this;
    }

    public void ShowWeapon(string className)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);


        foreach (WeaponPreview weapon in weapons)
        {
            if (weapon.weaponID == className)
            {
                currentWeapon = Instantiate(weapon.prefab, spawnPoint);

                currentWeapon.transform.localPosition = Vector3.zero;
                currentWeapon.transform.localRotation = Quaternion.identity;

                return;
            }
        }

        Attachmentmanager manager = currentWeapon.GetComponent<Attachmentmanager>();

        foreach (string id in SaveManager.Instance.Data.equippedAttachments)
        {
            Attachment attachment = DataBase.Instance.GetAttachment(id);

            manager.EquipAttachment(attachment);
        }

        Debug.LogWarning("No preview weapon found for class: " + className);
    }
}

