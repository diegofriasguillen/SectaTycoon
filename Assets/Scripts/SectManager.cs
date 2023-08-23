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
            Debug.LogWarning("Todos los cuartos ya est�n activos.");
        }
    }

    public void AddFollowerToRoom(GameObject room)
    {
        Debug.Log("Intentando a�adir seguidor al cuarto"); // Mensaje de depuraci�n
        Transform spawnPoint = room.transform.Find("FollowerSpawnPoint");

        // Instanciamos el seguidor y lo guardamos en una variable para poder manipularlo despu�s
        GameObject followerInstance = Instantiate(followerPrefab, spawnPoint.position, Quaternion.identity, room.transform);

        // Nos aseguramos de que el seguidor est� activo
        followerInstance.SetActive(true);

        Debug.Log("Seguidor a�adido al cuarto: " + room.name);  // Mensaje de depuraci�n
    }
}