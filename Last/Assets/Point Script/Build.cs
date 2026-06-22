using UnityEngine;

public static class Build
{
    public static Profile build(Stats stats)
    {
        Profile profile = new Profile();

        profile.health = 100 + (stats.health * 10);
        profile.shields = 50 + (stats.shields * 10);

        profile.walkSpeed = 5 + (stats.walkSpeed * 0.25f);
        profile.sprintSpeed = 8 + (stats.sprintSpeed * 0.35f);

        return profile;
    }
}
