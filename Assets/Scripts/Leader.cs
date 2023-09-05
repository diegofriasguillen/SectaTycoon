using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Leader : MonoBehaviour
{
    public float checkInterval = 5f;
    public Transform[] specificPoints; 
    public SectStates stateManager;

    private NavMeshAgent agent;
    private float timer = 0f;
    private Queue<Vector3> waypoints = new Queue<Vector3>();

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InitializeWaypoints();
        stateManager.ChangeLeaderState(SectStates.LeaderState.CheckFollowers);
    }

    void InitializeWaypoints()
    {
        Room[] rooms = FindObjectsOfType<Room>();
        foreach (Room room in rooms)
        {
            waypoints.Enqueue(room.transform.position);
        }

        foreach (Transform point in specificPoints)
        {
            waypoints.Enqueue(point.position);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (agent.remainingDistance < 0.5f && timer >= checkInterval)
        {
            if (stateManager.GetLeaderState() == SectStates.LeaderState.CheckFollowers)
            {
                CheckFollowers();
                stateManager.ChangeLeaderState(SectStates.LeaderState.CollectEconomy);
            }
            else if (stateManager.GetLeaderState() == SectStates.LeaderState.CollectEconomy)
            {
                CollectEconomy();
                GoToNextWaypoint();
                stateManager.ChangeLeaderState(SectStates.LeaderState.CheckFollowers);
            }
            timer = 0f;
        }
    }

    void CheckFollowers()
    {
        Room[] rooms = FindObjectsOfType<Room>();
        if (rooms.Length > 0)
        {
            int randomRoomIndex = Random.Range(0, rooms.Length);
            agent.SetDestination(rooms[randomRoomIndex].transform.position);
            CollectEconomyFromRoom(rooms[randomRoomIndex]);
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
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Count > 0)
        {
            Vector3 nextWaypoint = waypoints.Dequeue();
            agent.SetDestination(nextWaypoint);
            waypoints.Enqueue(nextWaypoint);  
        }
    }
}