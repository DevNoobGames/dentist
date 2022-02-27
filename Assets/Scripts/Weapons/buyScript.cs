using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyScript : MonoBehaviour
{
    public string instruction;
    public float cost;
    public int gunID;
    public int AmmoReward;

    public GeneralManager genMan;

    [Header("If ammo")]
    public toothpaste gunScript;

    [Header ("If elephant")]
    public bool isElephant;
    public GameObject elephantObj;

    public void reward()
    {
        if (gunScript)
        {
            if (!gunScript.owned)
            {
                gunScript.owned = true;

            }
            else
            {
                gunScript.reserveAmmo += AmmoReward;
                gunScript.setAmmoText();
            }
        }

        if (isElephant)
        {
            genMan.changeElephantAmount(1);
            Instantiate(elephantObj, new Vector3(0, 17, 0), Quaternion.identity);
        }
    }
}
