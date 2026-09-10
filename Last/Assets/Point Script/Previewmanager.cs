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
        //ShowWeapon(SaveManager.Instance.Data.className);
    }
    //spawns the primary weapons using save manager
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
    //displays the current weapons for class
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

            return;
        }

        Debug.LogWarning("No preview found for " + className);

        if (className != "Assault")
        {
            ShowWeapon("Assault");
        }
    }
    //apply the attachments to weapons in the point stat weapon scene
    void ApplyAttachments(GameObject weapon, bool primary)
    {
        Attachmentmanager manager = weapon.GetComponent<Attachmentmanager>();

        foreach (AttchSelect socket in currentPrimary.GetComponentsInChildren<AttchSelect>())
        {
            socket.weapon = manager;
        }

        foreach (AttchSelect socket in currentPrimary.GetComponents<AttchSelect>())
        {
            socket.weapon = manager;
        }

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

        foreach (EquippedAttachment equipped in SaveManager.Instance.Data.equippedAttachments)
        {
            Attachment attachment = DataBase.Instance.GetAttachment(equipped.attachmentID);

            if (attachment == null)
                continue;

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
    //spawns the primary weapons
    public void SpawnPrimary(string weaponID)
    {
        if (currentPrimary != null)
            Destroy(currentPrimary);

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
    //refresh the preview
    public void RefreshPreview()
    {
        if (SaveManager.Instance == null)
            return;

        ShowWeapon(SaveManager.Instance.Data.className);
    }
    //gets the attachmentmanager
    public Attachmentmanager CurrentAttachmentManager
    {
        get
        {

            if (currentPrimary == null)
                return null;

            return currentPrimary.GetComponent<Attachmentmanager>();
        }
    }
}

