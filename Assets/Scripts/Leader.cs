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
            }
            timer = 0f;
        }
    }

    void CollectEconomy()
    {
        Follower[] followers = FindObjectsOfType<Follower>();
        float totalEconomyCollected = 0f;

        foreach (Follower follower in followers)
        {
            totalEconomyCollected += follower.CollectEconomy();
        }

        Debug.Log("Economía recolectada: " + totalEconomyCollected);
    }
}