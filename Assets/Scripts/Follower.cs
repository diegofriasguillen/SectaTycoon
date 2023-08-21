using UnityEngine;
using UnityEngine.AI;

public class Follower : MonoBehaviour
{
    public float economyRate = 1f; 
    public SectStates stateManager;

    private float currentEconomy = 0f;

    void Start()
    {
        stateManager.ChangeFollowerState(SectStates.FollowerState.GenerateEconomy);
    }

    void Update()
    {
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
        Debug.Log(gameObject.name + " generando economía. Total: " + currentEconomy);
    }

    void Idle()
    {
        // Lógica inactividad
    }

    public float CollectEconomy()
    {
        float amount = currentEconomy;
        currentEconomy = 0f;
        return amount;
    }
}
