using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Attachmentmenu : MonoBehaviour
{
    //where the attachments on weapon happens
    public static Attachmentmenu Instance;

    public GameObject panel;

    public Transform content;

    public GameObject buttonPrefab;

    Attachmentmanager currentWeapon;

    AttachmentType currentType;

    public TMP_Text titleText;

    void Awake()
    {
        Instance = this;

        panel.SetActive(false);
    }

    void Start()
    {
        foreach (Attachment attachment in DataBase.Instance.attachments)
        {
            if (!SaveManager.Instance.Data.unlockedAttachments.Contains(attachment.attachmentName))
            {
                SaveManager.Instance.Data.unlockedAttachments.Add(attachment.attachmentName);
            }
        }

        SaveManager.Instance.SaveGame();
    }

    public void Open(Attachmentmanager weapon, AttachmentType type)
    {
        currentWeapon = weapon;
        currentType = type;

        titleText.text = type.ToString();

        Socket socket = weapon.GetSocket(type);

        if (socket != null)
            Attchcamera.Instance.FocusSocket(socket);

        panel.SetActive(true);

        foreach (Transform child in content)
            Destroy(child.gameObject);

        Populate();
    }

    public void Close()
    {
        panel.SetActive(false);

        Attchcamera.Instance.ResetView();
    }
    //where attachbutton will populate on each panel to the correct attachment type
    void Populate()
    {
        foreach (Transform child in content)
            Destroy(child.gameObject);

        Debug.Log("Unlocked Count = " + SaveManager.Instance.Data.unlockedAttachments.Count);

        Debug.Log("Populate Type = " + currentType);

        foreach (string id in SaveManager.Instance.Data.unlockedAttachments)
        {

            Debug.Log("Unlocked: " + id);

            Attachment attachment = DataBase.Instance.GetAttachment(id);

            if (attachment == null)
            {
                Debug.Log("Database couldn't find " + id);
                continue;
            }

            Debug.Log("Found " + attachment.attachmentName +  " Type = " + attachment.attachmentType);

            if (attachment.attachmentType != currentType)
            {
                Debug.Log("Skipped");
                continue;
            }

            Debug.Log("Creating button for " + attachment.attachmentName);

            GameObject button = Instantiate(buttonPrefab, content);

            Debug.Log(button.name);
            Debug.Log(button.GetComponent<Button>());
            Debug.Log(button.GetComponent<Attachbutton>());

            //button.GetComponent<Attachbutton>().Setup(attachment);


            Attachbutton ui = button.GetComponentInChildren<Attachbutton>();

            ui.Setup(attachment);
        }
    }

    void Equip(Attachment attachment)
    {
        currentWeapon.EquipAttachment(attachment);

        SaveAttachment(attachment);

        Close();
    }

    //save the attachment on the weapon across scene
    void SaveAttachment(Attachment attachment)
    {
        SaveData save = SaveManager.Instance.Data;

        save.equippedAttachments.RemoveAll
        (
            x => x.socketType == attachment.attachmentType
        );

        save.equippedAttachments.Add
        (
            new EquippedAttachment
            {
                socketType = attachment.attachmentType,
                attachmentID = attachment.attachmentName
            }
        );

        SaveManager.Instance.SaveGame();
    }
    //equip the attachment to weapon
    public void EquipAttachment(Attachment attachment)
    {
        Debug.Log("Button pressed");

        if (currentWeapon == null)
        {
            Debug.LogError("Current Weapon NULL");
            return;
        }

        Debug.Log(currentWeapon.name);

        Attachmentmanager manager = Previewmanager.Instance.CurrentAttachmentManager;

        manager.EquipAttachment(attachment);

        currentWeapon.EquipAttachment(attachment);

        Debug.Log(currentWeapon);
        Debug.Log(currentWeapon.gameObject.name);

        SaveAttachment(attachment);

        Statsmanager.Instance.RefreshProfile();

        Close();
    }
}
