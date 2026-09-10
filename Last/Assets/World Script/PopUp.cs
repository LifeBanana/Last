using TMPro;
using UnityEngine;
//where the pop up text is created
public class PopUp : MonoBehaviour
{
    public TMP_Text text;

    public float speed = 2;

    public float lifetime = 1f;

    public static PopUp prefab;

    public static void Create(
        Vector3 worldPos,
        float damage)
    {
        PopUp popup = Instantiate( prefab,worldPos, Quaternion.identity);

        popup.Setup(damage);
    }

    public void Setup(float damage)
    {
        Debug.Log("Setting popup damage: " + damage);

        if (text == null)
        {
            Debug.LogError("TMP_Text reference missing!");
            return;
        }

        text.text = damage.ToString("0");
    }

    void Update()
    {
        transform.position +=  Vector3.forward * speed * Time.deltaTime;

        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
            Destroy(gameObject);
    }
}
