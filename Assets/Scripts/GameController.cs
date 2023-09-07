using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public SectManager sectManager;
    public TMP_Text economyText;
    public Button buyFollowerButton;
    public Slider npcSlider;
    public Slider policeSlider;
    public Button bribePoliceButton;

    public float followerCost = 100f;
    public float followerCostIncrement = 10f;

    public float initialPoliceRiskIncreaseRate = 0.1f;
    public float policeRiskRateIncrement = 0.01f;
    public int followersIncreaceRiskPolice = 5;
    public float bribeCost = 50f;
    public float bribeCostIncrement = 10f;

    private float playerEconomy = 100f;
    private float policeRisk = 0f;
    private float policeRiskIncreaseRate;
    private int previousFollowerCount;

    void Start()
    {
        policeRiskIncreaseRate = initialPoliceRiskIncreaseRate;
        previousFollowerCount = 0;
        npcSlider.maxValue = 216;  
        policeSlider.maxValue = 100;  
    }

    void Update()
    {
        economyText.text = "Slave faith: " + Mathf.RoundToInt(playerEconomy).ToString();
        buyFollowerButton.interactable = playerEconomy >= followerCost;
        bribePoliceButton.interactable = playerEconomy >= bribeCost;

        Follower[] followers = FindObjectsOfType<Follower>();
        npcSlider.value = followers.Length;

        policeRisk += policeRiskIncreaseRate * Time.deltaTime;
        policeSlider.value = policeRisk;

        int currentFollowerCount = followers.Length;

        if (currentFollowerCount / followersIncreaceRiskPolice > previousFollowerCount / followersIncreaceRiskPolice)
        {
            policeRiskIncreaseRate += policeRiskRateIncrement;
            previousFollowerCount = currentFollowerCount;
        }
    }

    private void FixedUpdate()
    {
        Follower[] followers = FindObjectsOfType<Follower>();
        foreach (Follower follower in followers)
        {
            AddEconomy(follower.CollectEconomy());
        }
    }

    public void BuyFollower()
    {
        if (playerEconomy >= followerCost)
        {
            playerEconomy -= followerCost;
            followerCost += followerCostIncrement;

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

    public void BribePolice()
    {
        if (playerEconomy >= bribeCost)
        {
            playerEconomy -= bribeCost;
            bribeCost += bribeCostIncrement;
            policeRisk = 0;  
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
    }
}


