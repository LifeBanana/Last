using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Attachmentmenu : MonoBehaviour
{
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
        if (SaveManager.Instance.Data.unlockedAttachments.Count == 0)
        {
            foreach (Attachment attachment in DataBase.Instance.attachments)
            {
                SaveManager.Instance.Data.unlockedAttachments.Add(attachment.attachmentName);
            }

            SaveManager.Instance.SaveGame();
        }
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

    void Populate()
    {
        foreach (Transform child in content)
            Destroy(child.gameObject);

        foreach (string id in SaveManager.Instance.Data.unlockedAttachments)
        {

            Debug.Log("Unlocked: " + id);

            Attachment attachment = DataBase.Instance.GetAttachment(id);

            if (attachment == null)
                continue;

            if (attachment.attachmentType != currentType)
                continue;

            if (attachment == null)
            {
                Debug.Log("Database couldn't find " + id);
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

        Close();
    }
}
