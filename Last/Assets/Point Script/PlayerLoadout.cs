using UnityEngine;

public class PlayerLoadout : MonoBehaviour
{
    public const int MAX_POINTS = 30;

    public Stats stats = new Stats();

    public WeaponProfile weapon;
    public Profile character;

    public string className;

    [SerializeField]
    private Gun equippedGun;

    [SerializeField]
    private PlayerMove movement;

    private void Start()
    {
        BuildLoadout();
    }

    public bool IsValidBuild()
    {
        return Calculator.CalculateCost(stats)
            <= MAX_POINTS;
    }

    public void BuildLoadout()
    {
        if (!IsValidBuild())
        {
            Debug.LogError("Build exceeds point limit.");
            return;
        }

        weapon = WeaponBuild.Build(stats);
        equippedGun.ApplyProfile(weapon);

        character = Build.build(stats);
        movement.ApplyProfile(character);

        className = ClassGenerator.GetClass(stats);

        Debug.Log("Generated Class: " + className);
    }
}
