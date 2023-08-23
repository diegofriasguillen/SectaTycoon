using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Leader : MonoBehaviour
{
    public float checkInterval = 10f;
    public SectStates stateManager;

    private NavMeshAgent agent;
    private float timer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stateManager.ChangeLeaderState(SectStates.LeaderState.CheckFollowers);
    }

    void Update()
    {
        switch (stateManager.GetLeaderState())
        {
            case SectStates.LeaderState.CheckFollowers:
                CheckFollowers();
                break;
            case SectStates.LeaderState.CollectEconomy:
                CollectEconomy();
                break;
        }
    }

    void CheckFollowers()
    {
        timer += Time.deltaTime;
        if (timer >= checkInterval)
        {
            Room[] rooms = FindObjectsOfType<Room>();
            if (rooms.Length > 0)
            {
                int randomRoomIndex = Random.Range(0, rooms.Length);
                agent.SetDestination(rooms[randomRoomIndex].transform.position);

                // Recolectar econom�a de la sala que el l�der est� visitando
                CollectEconomyFromRoom(rooms[randomRoomIndex]);
            }
            timer = 0f;
        }
    }

    void CollectEconomyFromRoom(Room room)
    {
        Transform[] followers = room.GetComponentsInChildren<Transform>();
        foreach (Transform followerTransform in followers)
        {
            Follower follower = followerTransform.GetComponent<Follower>();
            if (follower)
            {
                float amount = follower.CollectEconomy();
                // Sumar esto al total de econom�a del jugador
                // Aqu� debes insertar c�digo para a�adir esta cantidad a la econom�a del jugador
            }
        }
    }

    void CollectEconomy()
    {
        Follower[] followers = FindObjectsOfType<Follower>();
        float totalEconomyCollected = 0f;
        float amount;

        foreach (Follower follower in followers)
        {
            amount = follower.CollectEconomy();
            totalEconomyCollected += amount;
            Debug.Log("Recolectado de " + follower.name + ": " + amount);
        }

        Debug.Log("Econom�a recolectada: " + totalEconomyCollected);
    }
}