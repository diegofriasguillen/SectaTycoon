using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public SectManager sectManager;
    public TMP_Text economyText;
    public Button buyFollowerButton;
    public float followerCost = 100f;

    private float playerEconomy = 100f;

    void Update()
    {
        economyText.text = "Economía: " + playerEconomy;

        buyFollowerButton.interactable = playerEconomy >= followerCost;
    }

    public void BuyFollower()
    {
        Debug.Log("Intentando comprar seguidor. Economía actual: " + playerEconomy);
        if (playerEconomy >= followerCost)
        {
            playerEconomy -= followerCost;

            Room[] rooms = FindObjectsOfType<Room>();
            foreach (Room room in rooms)
            {
                if (room.CanAddFollower())
                {
                    sectManager.AddFollowerToRoom(room.gameObject); 
                    return;
                }
            }


            sectManager.ActivateNextRoom(); 
            Room newRoom = FindObjectOfType<Room>();
            sectManager.AddFollowerToRoom(newRoom.gameObject);
        }

        {
            Debug.LogWarning("No hay suficiente economía para comprar un seguidor.");
        }
    }

    public void AddEconomy(float amount)
    {
        playerEconomy += amount;
    }

    void UpdateEconomyUI()
    {
        if (economyText != null)
        {
            economyText.text = "Economía: " + playerEconomy.ToString();
        }
        else
        {
            Debug.LogError("El componente Text de economía no está asignado.");
        }
    }
}