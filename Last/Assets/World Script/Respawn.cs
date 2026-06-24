using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour
{
    public Transform[] spawnPoints;

    public float respawnTime = 5f;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void respawn()
    {
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        gameObject.SetActive(false);

        yield return new WaitForSeconds(respawnTime);

        Transform spawn =  spawnPoints[  Random.Range(    0, spawnPoints.Length)];

        transform.position = spawn.position;

        gameObject.SetActive(true);
    }
}
