using UnityEngine;

public static class secondweapon
{
    public static string GetSecondary(string className)
    {
        switch (className)
        {
            case "Assault":
                return "Glock";

            case "Heavy":
                return "Revolver";

            case "Recon":
                return "USP45";

            case "Support":
                return "P226";

            case "Engineer":
                return "MachinePistol";
        }

        return "Glock";
    }
}