using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameController : MonoBehaviour
{
    public SectManager sectManager;
    public TMP_Text economyText;
    public Button buyFollowerButton;
    public Slider npcSlider;
    public Slider policeSlider;
    public Button bribePoliceButton;
    public GameObject musicSound;

    public float followerCost = 100f;
    public float followerCostIncrement = 50f;
    public TMP_Text followerCostText;

    public float initialPoliceRiskIncreaseRate = 0.1f;
    public float policeRiskRateIncrement = 0.01f;
    public int followersIncreaceRiskPolice = 5;
    public float bribeCost = 50f;
    public float bribeCostIncrement = 10f;

    private float playerEconomy = 100f;
    private float policeRisk = 0f;
    private float policeRiskIncreaseRate;
    private int previousFollowerCount;

    public GameObject winPanel;
    public GameObject losePanel;
    private bool gameIsOver = false;

    public GameObject followerKilledMessage;
    public GameObject soundKilledFollower;
    public GameObject soundPolice;
    public GameObject secondsoundPolice;

    public GameObject decoration1;
    public GameObject decoration2;
    public GameObject decoration3;
    public GameObject decoration4;
    public GameObject decoration5;
    public GameObject decoration6;
    public GameObject decoration7;     
    public GameObject decoration8;
    public GameObject decoration9;
    public GameObject decoration10;
    public GameObject decoration11;
    public GameObject decoration12;
    public GameObject decoration13;


    void Start()
    {
        policeRiskIncreaseRate = initialPoliceRiskIncreaseRate;
        previousFollowerCount = 0;
        npcSlider.maxValue = 216;  
        policeSlider.maxValue = 100;  

        winPanel.SetActive(false);
        losePanel.SetActive(false);
        gameIsOver = false; 

        decoration1.SetActive(false);
        decoration2.SetActive(false);

        followerCostText.text = "Follower Cost: " + followerCost.ToString();
        musicSound.SetActive(true);
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

        if (followers.Length >= 216)
        {
            WinGame();
            return;
        }

        // Decoraciones se activan respecto a la cantidad de slaves

        if (followers.Length >= 3)
        {
            decoration1.SetActive(true);
        }
        if (followers.Length >= 13)
        {
            decoration2.SetActive(true);
        }
        if (followers.Length >= 33)
        {
            decoration3.SetActive(true);
        }
        if (followers.Length >= 53)
        {
            decoration4.SetActive(true);
        }
        if (followers.Length >= 83)
        {
            decoration5.SetActive(true);
        }
        if (followers.Length >= 103)
        {
            decoration6.SetActive(true);
        }
        if (followers.Length >= 123)
        {
            decoration7.SetActive(true);
        }
        if (followers.Length >= 153)
        {
            decoration8.SetActive(true);
        }
        if (followers.Length >= 183)
        {
            decoration9.SetActive(true);
        }
        if (followers.Length >= 183)
        {
            decoration10.SetActive(true);
        }
        if (followers.Length >= 193)
        {
            decoration11.SetActive(true);
        }
        if (followers.Length >= 200)
        {
            decoration12.SetActive(true);
        }
        if (followers.Length >= 210)
        {
            decoration13.SetActive(true);
        }


        // Primer sonido policias
        if (policeRisk < 50)
        {
            soundPolice.SetActive(false);
        }

        if (policeRisk >= 50)
        {
            soundPolice.SetActive(true);
        }

        if (policeRisk >= 80)
        {
            soundPolice.SetActive(false);
        }

        // Segundo sonido policias
        if (policeRisk < 50)
        {
            secondsoundPolice.SetActive(false);
        }

        if (policeRisk > 90)
        {
            secondsoundPolice.SetActive(true);
        }

        if (soundPolice.activeSelf || secondsoundPolice.activeSelf)
        {
            musicSound.SetActive(false);
        }
        else
        {
            musicSound.SetActive(true);
        }

        if (policeRisk >= 100)
        {
            LoseGame();
            return; 
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


    //public void BuyFollower()
    //{

    //    if (playerEconomy >= followerCost)
    //    {
    //        playerEconomy -= followerCost;
    //        followerCost += followerCostIncrement;

    //        float randomChance = Random.Range(0f, 1f);

    //        if (randomChance <= 0.1f)
    //        {
    //            Follower[] followers = FindObjectsOfType<Follower>();
    //            if (followers.Length > 3)
    //            {
    //                int randomIndex = Random.Range(0, followers.Length);
    //                Follower followerToDestroy = followers[randomIndex];

    //                if (followerToDestroy != null)
    //                {
    //                    sectManager.ReturnFollowerToPool(followerToDestroy.gameObject);
    //                    ShowFollowerKilledMessage();
    //                    //Debug.Log("Un seguidor ha sido eliminado!");
    //                }
    //            }
    //        }
    //        else
    //        {
    //            Room[] rooms = FindObjectsOfType<Room>();
    //            foreach (Room room in rooms)
    //            {
    //                if (room.CanAddFollower())
    //                {
    //                    sectManager.AddFollowerToRoom(room.gameObject);
    //                    return;
    //                }
    //            }
    //            sectManager.ActivateNextRoom();
    //            Room newRoom = FindObjectOfType<Room>();
    //            sectManager.AddFollowerToRoom(newRoom.gameObject);
    //        }
    //    }

    //    followerCostText.text = "Follower Cost: " + followerCost.ToString();
    //}

    public void BuyFollower()
    {
        if (playerEconomy >= followerCost)
        {
            playerEconomy -= followerCost;
            followerCost += followerCostIncrement;

            float randomChance = Random.Range(0f, 1f);

            if (randomChance <= 0.1f)
            {
                Follower[] followers = FindObjectsOfType<Follower>();
                if (followers.Length > 3)
                {
                    int randomIndex = Random.Range(0, followers.Length);
                    Follower followerToDestroy = followers[randomIndex];

                    if (followerToDestroy != null)
                    {
                        sectManager.ReturnFollowerToPool(followerToDestroy.gameObject);
                        ShowFollowerKilledMessage();
                        //Debug.Log("Un seguidor ha sido eliminado!");
                    }
                }
            }
            else
            {
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

        followerCostText.text = "Follower Cost: " + followerCost.ToString();
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

    public IEnumerator DesactivateAfterSeconds(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }

    public void ShowFollowerKilledMessage()
    {
        followerKilledMessage.SetActive(true);
        soundKilledFollower.SetActive(true);
        StartCoroutine(DesactivateAfterSeconds(soundKilledFollower, 2));
        StartCoroutine(DesactivateAfterSeconds(followerKilledMessage, 2));
    }

    public void WinGame()
    {
        if (gameIsOver) return;  

        gameIsOver = true;  
        winPanel.SetActive(true);  
        Time.timeScale = 0f;  

        musicSound.SetActive(false);

        Debug.Log("Has ganado el juego!");
    }

    public void LoseGame()
    {
        if (gameIsOver) return;  

        gameIsOver = true;
        losePanel.SetActive(true);
        Time.timeScale = 0f;

        musicSound.SetActive(false);

        Debug.Log("Has perdido el juego!");
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

