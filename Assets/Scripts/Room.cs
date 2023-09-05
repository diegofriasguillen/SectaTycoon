using UnityEngine;

public class Room : MonoBehaviour
{
    public int maxFollowers = 18;
    public Transform[] interestPoints;

    public int CurrentFollowersCount()
    {
        return transform.childCount;
    }

    public bool CanAddFollower()
    {
        return CurrentFollowersCount() < maxFollowers;
    }

    public Transform[] GetInterestPoints()
    {
        return interestPoints;
    }
}