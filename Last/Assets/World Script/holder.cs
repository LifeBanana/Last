using UnityEngine;
//where the main weapons and side arm prefabs are spawned in the empty game object
public class holder : MonoBehaviour
{
    public Transform weaponSocket;

    public Weapon currentPrimary;

    public SideArm currentSecondary;

    public Weapon SpawnPrimary(GameObject prefab)
    {
        if (currentPrimary != null)
            Destroy(currentPrimary.gameObject);

        currentPrimary = Instantiate(prefab, weaponSocket).GetComponent<Weapon>();

        return currentPrimary;
    }

    public SideArm SpawnSecondary(GameObject prefab)
    {
        if (currentSecondary != null)
            Destroy(currentSecondary.gameObject);

        currentSecondary = Instantiate(prefab, weaponSocket).GetComponent<SideArm>();

        currentSecondary.gameObject.SetActive(false);

        return currentSecondary;
    }
}