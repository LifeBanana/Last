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
        if (SaveManager.Instance == null)
        {
            Debug.LogError("SaveManager Instance is NULL");
            return;
        }

        if (SaveManager.Instance.Data == null)
        {
            Debug.LogError("SaveData is NULL");
            return;
        }

        Debug.Log("Primary Weapon ID = " + SaveManager.Instance.Data.primaryWeaponID);

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

            //if (Inventory.Instance != null)
            //{
            //    Inventory.Instance.BuildInventory();
            //}
            //else
            //{
            //    Debug.LogError("Inventory.Instance is NULL");
            //}

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
        {
            Debug.LogError(
                weapon.name + " has no Attachmentmanager component!");
            return;
        }

        var attachments = SaveManager.Instance.Data.equippedAttachments;

        if (attachments == null)
        {
            Debug.LogError("Attachment list is NULL");
            return;
        }

        foreach (string id in attachments)
        {
            if (DataBase.Instance == null)
            {
                Debug.LogError("Database Instance is NULL");
                return;
            }

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

        //Inventory.Instance.BuildInventory();

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

