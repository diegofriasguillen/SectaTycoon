using System.Collections.Generic;
using UnityEngine;

public class SectManager : MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject followerPrefab;

    private int currentRoomIndex = 0;
    private List<GameObject> followerPool = new List<GameObject>();

    public void ActivateNextRoom()
    {
        if (currentRoomIndex < rooms.Length)
        {
            rooms[currentRoomIndex].SetActive(true);
            currentRoomIndex++;
        }
    }

    public GameObject GetPooledFollower()
    {
        for (int i = 0; i < followerPool.Count; i++)
        {
            if (!followerPool[i].activeInHierarchy)
            {
                return followerPool[i];
            }
        }
        return null;
    }

    public void AddFollowerToRoom(GameObject room)
    {
        if (room == null)
        {
            Debug.LogWarning("Room is null");
            return;
        }

        Transform spawnPoint = room.transform.Find("FollowerSpawnPoint");
        if (spawnPoint == null)
        {
            Debug.LogWarning("SpawnPoint not found");
            return;
        }

        GameObject followerInstance = GetPooledFollower();

        if (followerInstance == null)
        {
            followerInstance = Instantiate(followerPrefab, spawnPoint.position, Quaternion.identity, room.transform);
            followerPool.Add(followerInstance);
        }
        else
        {
            followerInstance.transform.position = spawnPoint.position;
            followerInstance.transform.SetParent(room.transform);
        }

        followerInstance.SetActive(true);
    }

    public void ReturnFollowerToPool(GameObject follower)
    {
        follower.SetActive(false);
    }
}
