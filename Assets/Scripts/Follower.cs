using UnityEngine;
using UnityEngine.AI;

public class Follower : MonoBehaviour
{
    private int destinationPoint = 0;
    private NavMeshAgent agent;
    private Room currentRoom;

    public float economyRate = 1f;
    public SectStates stateManager;

    private float currentEconomy = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        stateManager.ChangeFollowerState(SectStates.FollowerState.GenerateEconomy);

        currentRoom = GetComponentInParent<Room>();
        if (currentRoom != null)
        {
            GotoNextPoint();
        }
        else
        {
            Debug.LogError("Follower is not placed in a room!");
        }
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

        switch (stateManager.GetFollowerState())
        {
            case SectStates.FollowerState.GenerateEconomy:
                GenerateEconomy();
                break;
            case SectStates.FollowerState.Idle:
                Idle();
                break;
        }
    }

    void GenerateEconomy()
    {
        currentEconomy += economyRate * Time.deltaTime;
        //Debug.Log(gameObject.name + " generando economía. Total: " + currentEconomy);
    }

    void GotoNextPoint()
    {
        Transform[] points = currentRoom.GetInterestPoints();

        if (points.Length == 0)
            return;

        agent.destination = points[destinationPoint].position;
        destinationPoint = (destinationPoint + 1) % points.Length;
    }

    void Idle()
    {
        // No tengo ideas para usar idle
    }

    public float CollectEconomy()
    {
        float amount = currentEconomy;
        currentEconomy = 0f;
        return amount;
    }
}