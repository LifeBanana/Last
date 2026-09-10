using UnityEngine;
using TMPro;
//where the player earns points from killing enemies
public class Scoremanager : MonoBehaviour
{
    public static Scoremanager Instance;

    public int score = 0;

    public TextMeshProUGUI scoretxt;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        scoretxt.text = "Score: " + score;
    }

    public void AddScore(int amount)
    {
        score += amount;

        Debug.Log("Score increased by " + amount);
        Debug.Log("Current Score: " + score);
    }

    public int GetScore()
    {
        return score;
    }
}