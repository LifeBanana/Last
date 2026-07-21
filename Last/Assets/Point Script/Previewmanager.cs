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

    void Start()
    {
        CurrentWeapons();
    }

    public void CurrentWeapons()
    {
        SpawnPrimary(SaveManager.Instance.Data.primaryWeaponID);
    }

    public void ShowWeapon(string className)
    {
        if (currentPrimary != null)
            Destroy(currentPrimary);

        if (currentSecondary != null)
            Destroy(currentSecondary);

        if (string.IsNullOrWhiteSpace(className))
        {
            className = "Assault";
            Debug.Log("No class selected. Defaulting to Assault.");
        }

        foreach (var weapon in weapons)
        {
            Debug.Log("Checking " + weapon.weaponID);

            if (weapon.weaponID != className)
                continue;

            Debug.Log("Found!");

            currentPrimary = Instantiate(  weapon.primaryWeaponPrefab,  primarySpawn);

            currentPrimary.transform.localPosition = Vector3.zero;
            currentPrimary.transform.localRotation = Quaternion.identity;

            currentSecondary = Instantiate(weapon.secondaryWeaponPrefab, secondarySpawn);

            currentSecondary.transform.localPosition = Vector3.zero;
            currentSecondary.transform.localRotation = Quaternion.identity;

            ApplyAttachments(currentPrimary, true);

            ApplyAttachments(currentSecondary, false);

            Inventory.Instance.BuildInventory();

            return;
        }

        Debug.LogWarning("No preview found for " + className);

        if (className != "Assault")
        {
            ShowWeapon("Assault");
        }
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

            if (attachment == null)
            {
                Debug.LogWarning("Attachment not found: " + id);
                continue;
            }

            manager.EquipAttachment(attachment);
        }
    }

    public GameObject CurrentPrimary
    {
        get { return currentPrimary; }
    }

    public GameObject CurrentSecondary
    {
        get { return currentSecondary; }
    }

    public void SpawnPrimary(string weaponID)
    {
        if (currentPrimary != null)
            Destroy(currentPrimary);

        Inventory.Instance.BuildInventory();

        foreach (WeaponPreview weapon in weapons)
        {
            if (weapon.weaponID != weaponID)
                continue;

            currentPrimary = Instantiate( weapon.primaryWeaponPrefab, primarySpawn);

            currentPrimary.transform.localPosition = Vector3.zero;
            currentPrimary.transform.localRotation = Quaternion.identity;

            ApplyAttachments(currentPrimary, true);

            return;
        }

        Debug.LogWarning("Primary weapon not found: " + weaponID);
    }
}

