using UnityEngine;
//test scripts for weapon and applying weapons stats modifications
public class Gun : MonoBehaviour
{
    public float damage;
    public float fireRate;
    public float spread;
    public float recoil;
    public float adstime;
    public float range;
    public float sprinttofire;
    public float reload;

    public void ApplyProfile(Profile profile)
    {
        damage = profile.damage;
        fireRate = profile.fireRate;
        spread = profile.spread;
        recoil = profile.recoil;
        range = profile.range;
        reload = profile.reloadTime;
        adstime = profile.adsTime;
        sprinttofire = profile.sprintToFire;

    }
}
