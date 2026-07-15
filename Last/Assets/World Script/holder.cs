using UnityEngine;

public class holder : MonoBehaviour
{
    public Transform weaponSocket;

    public Weapon currentPrimary;

    public Weapon currentSecondary;

    public Weapon SpawnPrimary(GameObject prefab)
    {
        if (currentPrimary != null)
            Destroy(currentPrimary.gameObject);

        currentPrimary = Instantiate(prefab, weaponSocket).GetComponent<Weapon>();

        return currentPrimary;
    }

    public Weapon SpawnSecondary(GameObject prefab)
    {
        if (currentSecondary != null)
            Destroy(currentSecondary.gameObject);

        currentSecondary = Instantiate(prefab, weaponSocket).GetComponent<Weapon>();

        currentSecondary.gameObject.SetActive(false);

        return currentSecondary;
    }
}