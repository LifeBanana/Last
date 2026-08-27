using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public RectTransform center;

    public float baseSize = 8f;
    public float spreadMultiplier = 5f;

    public float movementExpansion = 5f;
    public float sprintExpansion = 10f;

    public float fireExpansion = 15f;
    public float fireReturnSpeed = 8f;

    public float smoothSpeed = 10f;

    private float currentExpansion;
    private float targetExpansion;

    private Weapon weapon;
    private SideArm sideArm;

    private void Start()
    {
        FindWeapon();

        currentExpansion = baseSize;
        targetExpansion = baseSize;

        SetCrosshairCentre();
        UpdateCrosshair();
    }

    private void Update()
    {
        if (weapon == null && sideArm == null)
        {
            FindWeapon();
        }

        CalculateExpansion();

        currentExpansion = Mathf.Lerp(  currentExpansion, targetExpansion,  smoothSpeed * Time.deltaTime );

        UpdateCrosshair();
    }

    void FindWeapon()
    {
        weapon = FindFirstObjectByType<Weapon>();
        sideArm = FindFirstObjectByType<SideArm>();
    }

    void CalculateExpansion()
    {
        targetExpansion = baseSize;

        if (weapon != null && weapon.gameObject.activeInHierarchy)
        {
            targetExpansion += weapon.spread * spreadMultiplier;
        }
        else if (sideArm != null && sideArm.gameObject.activeInHierarchy)
        {
            targetExpansion += sideArm.spread * spreadMultiplier;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool moving = Mathf.Abs(horizontal) > 0.1f ||  Mathf.Abs(vertical) > 0.1f;

        if (moving)
        {
            targetExpansion += movementExpansion;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            targetExpansion += sprintExpansion;
        }
    }

    void UpdateCrosshair()
    {
        if (center == null)
            return;

        SetCrosshairCentre();
    }

    void SetCrosshairCentre()
    {
        if (center == null)
            return;

        center.anchorMin = new Vector2(0.5f, 0.5f);
        center.anchorMax = new Vector2(0.5f, 0.5f);

        center.pivot = new Vector2(0.5f, 0.5f);

        center.anchoredPosition = Vector2.zero;
    }

    public void Fire()
    {
        targetExpansion += fireExpansion;
    }
}