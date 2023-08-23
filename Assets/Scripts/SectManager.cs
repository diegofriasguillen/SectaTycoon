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
        Debug.Log("Intentando añadir seguidor al cuarto"); // Mensaje de depuración
        Transform spawnPoint = room.transform.Find("FollowerSpawnPoint");

        // Instanciamos el seguidor y lo guardamos en una variable para poder manipularlo después
        GameObject followerInstance = Instantiate(followerPrefab, spawnPoint.position, Quaternion.identity, room.transform);

        // Nos aseguramos de que el seguidor esté activo
        followerInstance.SetActive(true);

        Debug.Log("Seguidor añadido al cuarto: " + room.name);  // Mensaje de depuración
    }
}