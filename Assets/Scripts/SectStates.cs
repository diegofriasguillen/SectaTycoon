using UnityEngine;

public class SectStates : MonoBehaviour
{
    public enum LeaderState { CheckFollowers, CollectEconomy }
    public enum FollowerState { GenerateEconomy, Idle }

    public Leader leader;
    public Follower follower;

    private LeaderState leaderState;
    private FollowerState followerState;

    public void ChangeLeaderState(LeaderState newState)
    {
        leaderState = newState;
    }

    public void ChangeFollowerState(FollowerState newState)
    {
        followerState = newState;
    }

    public LeaderState GetLeaderState()
    {
        return leaderState;
    }

    public FollowerState GetFollowerState()
    {
        return followerState;
    }
}