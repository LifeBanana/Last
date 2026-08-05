using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class GameRefresh
{
    public static void RefreshEverything()
    {
        Statsmanager.Instance.RefreshProfile();

        if (Previewmanager.Instance != null)
            Previewmanager.Instance.RefreshPreview();

        Loadoutmanager loadout = UnityEngine.Object.FindFirstObjectByType<Loadoutmanager>();

        if (loadout != null)
            loadout.Initialize();

        Weapon weapon = UnityEngine.Object.FindFirstObjectByType<Weapon>();

        if (weapon != null)
            weapon.Initialize();

        SideArm sidearm = UnityEngine.Object.FindFirstObjectByType<SideArm>();

        if (sidearm != null)
            sidearm.Initialize();

        Controller controller = UnityEngine.Object.FindFirstObjectByType<Controller>();

        if (controller != null)
            controller.Initialize();
    }
}