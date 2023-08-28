using UnityEngine;
using UnityEngine.AI;

public class Leader : MonoBehaviour
{
    public float checkInterval = 10f;
    public SectStates stateManager;

    private NavMeshAgent agent;
    private float timer = 0f;

    private bool hasReachedDestination = false;
    private bool isFirstMovement = true;  

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stateManager.ChangeLeaderState(SectStates.LeaderState.CheckFollowers);
        hasReachedDestination = false;
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            hasReachedDestination = true;
        }
        else
        {
            hasReachedDestination = false;
        }

        if (hasReachedDestination && !isFirstMovement)  
        {
            Quaternion targetRotation = Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);
            hasReachedDestination = false;
        }

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
                CollectEconomyFromRoom(rooms[randomRoomIndex]);
            }
            timer = 0f;
            isFirstMovement = false;  
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
                
            }
        }
    }

    void CollectEconomy()
    {
        Follower[] followers = FindObjectsOfType<Follower>();
        float totalEconomyCollected = 0f;

        foreach (Follower follower in followers)
        {
            float amount = follower.CollectEconomy();
            totalEconomyCollected += amount;
        }

        //Debug.Log("Economía recolectada: " + totalEconomyCollected);
    }
}