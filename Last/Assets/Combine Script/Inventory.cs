using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public Transform gridParent;

    public GameObject slotPrefab;

    void Awake()
    {
        Instance = this;
    }

    //public void BuildInventory()
    //{
    //    Debug.Log("Building Inventory");
    //    foreach (Transform child in gridParent)
    //        Destroy(child.gameObject);

    //    foreach (string id in SaveManager.Instance.Data.unlockedAttachments)
    //    {
    //        Attachment attachment = DataBase.Instance.GetAttachment(id);

    //        if (attachment == null)
    //            continue;

    //        GameObject slot = Instantiate(slotPrefab, gridParent);
 
    //        slot.GetComponent<AttachSlot>().Setup(attachment);
    //    }
    //}
}
