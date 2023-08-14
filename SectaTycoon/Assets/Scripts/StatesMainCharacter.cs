using UnityEngine;

public class StatesMainCharacter : MonoBehaviour
{
    public enum MainCharacterState { GoToRoom }

    public MainCharacter mainCharacter;

    private MainCharacterState mainCharacterState;


    public void ChangeMainCharacterState(MainCharacterState newState)
    {
        mainCharacterState = newState;
    }


    public MainCharacterState GetMainCharacterState()
    {
        return mainCharacterState;
    }

}