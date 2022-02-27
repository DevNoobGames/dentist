using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneralManager : MonoBehaviour
{
    public TextMeshProUGUI counter;
    public List<GameObject> elephantList = new List<GameObject>();
    public int elephantAmount = 0;

    public int level;
    public GameObject walkingLips;
    public int[] spawnAmount;
    public GameObject[] spawnpoints;
    public int walkingLipsAmount;

    public TextMeshProUGUI walkinglipsCounter;
    public Player player;
    public int score;

    public GameObject gameOverScreen;
    public TextMeshProUGUI gameOverScreenText;

    public GameObject introPanel;
    
    // I WOULD LIKE TO SPEAK WITH THE MANAGER ABOUT THE QUALITY OF THIS SCRIPT! //
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        spawnpoints = GameObject.FindGameObjectsWithTag("spawnPoint");
        foreach (GameObject pnt in spawnpoints)
        {
            pnt.GetComponent<MeshRenderer>().enabled = false;
        }

        foreach (GameObject ele in GameObject.FindGameObjectsWithTag("elephant"))
        {
            elephantList.Add(ele);
            changeElephantAmount(1);
        }

        Time.timeScale = 0;
    }

    public void introPanelClose()
    {
        Time.timeScale = 1;
        introPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        level = 0;
        StartCoroutine(nextLevel());
    }


    public void changeElephantAmount(int elephantAdded)
    {
        elephantAmount += elephantAdded;
        counter.text = "Elephants: " + elephantAmount;

        if (elephantAmount <= 0)
        {
            gameOverScreen.SetActive(true);
            gameOverScreenText.text = "You killed " + score + " crazy lips!" + "\n" + "Thanks for playing :)";
            Time.timeScale = 0;
        }
    }

    public void levelUp(int leve)
    {
        if (leve < spawnAmount.Length)
        {
            for (int i = 0; i < spawnAmount[leve]; i++)
            {
                int randVal = Random.Range(0, spawnpoints.Length);
                GameObject bruh = Instantiate(walkingLips, spawnpoints[randVal].transform.position, Quaternion.identity);
                walkingLipsAmount += 1;
                setLipsText();
            }
        }
        else
        {
            for (int i = 0; i < spawnAmount[spawnAmount.Length - 1]; i++)
            {
                int randVal = Random.Range(0, spawnpoints.Length);
                GameObject bruh = Instantiate(walkingLips, spawnpoints[randVal].transform.position, Quaternion.identity);
                walkingLipsAmount += 1;
                setLipsText();
            }
        }
    }

    public void removeLips()
    {
        walkingLipsAmount -= 1;
        setLipsText();
        if (walkingLipsAmount <= 0)
        {
            level += 1;
            StartCoroutine(nextLevel());
        }
    }

    IEnumerator nextLevel()
    {
        yield return new WaitForSeconds(4);
        levelUp(level);
    }

    public void setLipsText()
    {
        walkinglipsCounter.text = "Lips: " + walkingLipsAmount;
    }

    public void addMoney(float moneyToAdd)
    {
        player.updateMoney(moneyToAdd);
    }
    public void addScore()
    {
        score += 1;
    }
}
