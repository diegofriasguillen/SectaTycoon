using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public SectManager sectManager;
    public TMP_Text economyText;
    public Button buyFollowerButton;
    public float followerCost = 100f;

    private float playerEconomy = 0f;

    void Update()
    {
        economyText.text = "Economía: " + playerEconomy;

        buyFollowerButton.interactable = playerEconomy >= followerCost;
    }

    public void BuyFollower()
    {
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
    }

    public void AddEconomy(float amount)
    {
        playerEconomy += amount;
    }
}