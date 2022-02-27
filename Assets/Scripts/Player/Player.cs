using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Basic stats")]
    public float health;
    public float money = 500;
    public TextMeshProUGUI moneyText;
    
    [Header ("Buying")]
    public TextMeshProUGUI instructionsText;
    public bool inBuyZone;
    public buyScript activeBuyScript;

    [Header("Gun info")]
    public toothpaste[] guns;
    public int activeGun;
    public AudioSource buyAudio;

    private void Start()
    {
        updateMoney(0); //to set the text
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("buyItem"))
        {
            activeBuyScript = other.GetComponent<buyScript>();
            instructionsText.text = activeBuyScript.instruction + "\n" + activeBuyScript.cost + "$ [E]";
            inBuyZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("buyItem"))
        {
            instructionsText.text = "";
            inBuyZone = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inBuyZone && money >= activeBuyScript.cost)
            {
                activeBuyScript.reward();
                updateMoney(-activeBuyScript.cost);
                buyAudio.Play();

                if (activeBuyScript.gunScript)
                {
                    guns[activeGun].gameObject.SetActive(false);
                    activeGun = activeBuyScript.gunID;
                    guns[activeGun].gameObject.SetActive(true);
                }
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            guns[activeGun].gameObject.SetActive(false);

            if (activeGun == 0)
            {
                for (int i = guns.Length; i-- > 0;)
                {
                    if (guns[i].owned)
                    {
                        guns[i].gameObject.SetActive(true);
                        activeGun = i;
                        return;
                    }

                    /*activeGun = 0;
                    guns[activeGun].gameObject.SetActive(true);*/
                }
            }
            else
            {
                for (int i = activeGun; i-- > 0;)
                {
                    if (guns[i].owned)
                    {
                        guns[i].gameObject.SetActive(true);
                        activeGun = i;
                        return;
                    }

                    /*activeGun = 0;
                    guns[activeGun].gameObject.SetActive(true);*/
                }
            }

        }





        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            guns[activeGun].gameObject.SetActive(false);

            if (activeGun >= (guns.Length - 1))
            {
                activeGun = 0;
                guns[activeGun].gameObject.SetActive(true);
            }
            else
            {
                for (int i = activeGun + 1; i < guns.Length; i++)
                {

                    if (guns[i].owned)
                    {
                        guns[i].gameObject.SetActive(true);
                        activeGun = i;
                        return;
                    }

                    activeGun = 0;
                    guns[activeGun].gameObject.SetActive(true);
                }
            }
        }
    }

    public void updateMoney(float change)
    {
        money += change;
        moneyText.text = money.ToString();
    }
}
