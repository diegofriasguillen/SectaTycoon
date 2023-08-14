using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MainCharacter : MonoBehaviour
{
    public Transform roomPoint;
    public List<Transform> objectPoints;
    public StatesMainCharacter stateManager;
    public AudioSource chasingSound;

    private NavMeshAgent mainCharacter;
    private Transform currentObject;

    void Start()
    {
        mainCharacter = GetComponent<NavMeshAgent>();
        stateManager.ChangeMainCharacterState(StatesMainCharacter.MainCharacterState.GoToRoom);
    }

    //void Update()
    //{
    //    switch (stateManager.MainCharacterState())
    //    {
    //        case StatesMainCharacter.MainCharacterState.GoToRoom:
    //            GoToRoom();
    //            break;
    //    }
    //}

    void GoToRoom()
    {
        if (objectPoints.Count > 0)
        {
            currentObject = objectPoints[0];
            mainCharacter.SetDestination(currentObject.position);
        }
    }


}