using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour
{
    public Transform[] spawnPoints;

    public float respawnTime = 5f;

    private CharacterController controller;
    private Health health;

    private bool respawning;

    private MonoBehaviour[] playerScripts;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        health = GetComponent<Health>();
    }

    private void Start()
    {
        playerScripts = GetComponents<MonoBehaviour>();
    }
    //function for other script to respawn the player
    public void RespawnPlayer()
    {
        if (respawning)
            return;

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Respawn: No spawn points assigned.");
            return;
        }

        StartCoroutine(RespawnRoutine());
    }
    //process to respawning the player
    private IEnumerator RespawnRoutine()
    {
        respawning = true;

        DisablePlayer();

        Debug.Log("Player died. Respawning in " + respawnTime + " seconds.");

        yield return new WaitForSeconds(respawnTime);

        Transform spawn =
            spawnPoints[Random.Range(0, spawnPoints.Length)];

        TeleportPlayer(spawn);

        if (health != null)
        {
            health.ResetHealth();
        }

        EnablePlayer();

        respawning = false;

        Debug.Log("Player respawned.");
    }
    //moves the player to a respawn position
    private void TeleportPlayer(Transform spawn)
    {
        if (controller != null)
        {
            controller.enabled = false;
        }

        transform.position = spawn.position;
        transform.rotation = spawn.rotation;

        if (controller != null)
        {
            controller.enabled = true;
        }
    }
    //disabl scritps and stops player from moving
    private void DisablePlayer()
    {
        foreach (MonoBehaviour script in playerScripts)
        {
            if (script == null)
                continue;

            if (script == this)
                continue;

            if (script == health)
                continue;

            if (script is Respawn)
                continue;

            script.enabled = false;
        }

        if (controller != null)
            controller.enabled = false;
    }
    //enables the player and any its scripts is active
    private void EnablePlayer()
    {
        foreach (MonoBehaviour script in playerScripts)
        {
            if (script == null)
                continue;

            if (script == this)
                continue;

            script.enabled = true;
        }

        if (controller != null)
            controller.enabled = true;
    }
    //checks to see if respawning is true
    public bool IsRespawning()
    {
        return respawning;
    }
}