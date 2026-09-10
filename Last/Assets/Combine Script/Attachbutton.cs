using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Attachbutton : MonoBehaviour
{
    //where the attachment prefab data will be put into the button to be abled to interact with the weapons
    public TMP_Text attachmentNameText;

    Attachment attachment;

    public Button button;

    void Awake()
    {
        attachmentNameText = GetComponentInChildren<TMP_Text>();
    }

    public void Setup(Attachment data)
    {
        if (data == null)
        {
            Debug.LogError("Attachment passed into Setup is NULL.");
            return;
        }

        if (attachmentNameText == null)
        {
            Debug.LogError("attachmentNameText isn't assigned on " + gameObject.name);
            return;
        }

        attachment = data;

        attachmentNameText.text = data.attachmentName + "\n" + data.attachmentType.ToString();

        button = GetComponentInChildren<Button>();

        if (button == null)
        {
            Debug.LogError("No Button component on " + gameObject.name);
            return;
        }

        Debug.Log(button);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => 
        {  
            Debug.Log("UNITY BUTTON CLICKED");
            Equip();
        });
    }

    //calls attachment menu to equip
    void Equip()
    {
        Debug.Log("Attachbutton Equip()");

        Attachmentmenu.Instance.EquipAttachment(attachment);
    }
}