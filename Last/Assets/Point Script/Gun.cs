using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage;
    public float fireRate;
    public float spread;

    public void ApplyProfile(WeaponProfile profile)
    {
        damage = profile.damage;
        fireRate = profile.fireRate;
        spread = profile.spread;

    }
}
