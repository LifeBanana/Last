using UnityEngine;
//scriptable object for attachment to be created

[CreateAssetMenu(menuName = "Skill Tree/Attachment")]
public class Attachment : ScriptableObject
{
    public string attachmentName;

    public AttachmentType attachmentType;

    public GameObject prefab;

    public StatModifier[] modifiers;
}