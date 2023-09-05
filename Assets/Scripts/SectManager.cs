using UnityEngine;

public class SectManager : MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject followerPrefab;

    private int currentRoomIndex = 0;

    public void ActivateNextRoom()
    {
        if (currentRoomIndex < rooms.Length)
        {
            rooms[currentRoomIndex].SetActive(true);
            
            currentRoomIndex++;
        }
        else
        {
            
        }
    }

    public void AddFollowerToRoom(GameObject room)
    {
        
        Transform spawnPoint = room.transform.Find("FollowerSpawnPoint");

        
        GameObject followerInstance = Instantiate(followerPrefab, spawnPoint.position, Quaternion.identity, room.transform);

        
        followerInstance.SetActive(true);

        
    }
}