using UnityEngine;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    public string[] dialogLines;
    public TextMeshProUGUI dialogText;
    public GameObject npcPanel;  
    public GameObject policePanel;  

    private int currentLine = 0;

    void Start()
    {

        if (npcPanel != null) npcPanel.SetActive(false);
        if (policePanel != null) policePanel.SetActive(false);


        if (dialogText != null)
        {
            dialogText.text = dialogLines[currentLine];
        }
        else
        {
            Debug.LogError("No se encontró un componente TextMeshProUGUI");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentLine++;
            if (currentLine < dialogLines.Length)
            {
                dialogText.text = dialogLines[currentLine];
            }
            else
            {
                Time.timeScale = 1f;

                if (npcPanel != null) npcPanel.SetActive(true);
                if (policePanel != null) policePanel.SetActive(true);

                this.gameObject.SetActive(false);
            }
        }
    }
}