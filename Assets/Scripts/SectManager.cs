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
            Debug.Log("Activando cuarto: " + currentRoomIndex); 
            currentRoomIndex++;
        }
        else
        {
            Debug.LogWarning("Todos los cuartos ya están activos.");
        }
    }

    public void AddFollowerToRoom(GameObject room)
    {

        Transform spawnPoint = room.transform.Find("FollowerSpawnPoint");
        Instantiate(followerPrefab, spawnPoint.position, Quaternion.identity, room.transform);
        Debug.Log("Agregando seguidor al cuarto: " + room.name); 
    }
}