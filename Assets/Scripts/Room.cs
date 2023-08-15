using UnityEngine;

public class Room : MonoBehaviour
{
    public int maxFollowers = 10;

    public int CurrentFollowersCount()
    {
        return transform.childCount; 
    }

    public bool CanAddFollower()
    {
        return CurrentFollowersCount() < maxFollowers;
    }
}