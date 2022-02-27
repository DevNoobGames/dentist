using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class toothpaste : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] CameraShake camShake;

    public GameObject toothpasteBullet;
    public RawImage[] crossHairs;


    public int clipSize;
    public int activeAmmo;
    public int reserveAmmo;
    public float reloadTime;
    public bool isReloading;
    public float timeBetweenShot;
    public bool hasShot;
    public bool isAutomatic;
    public bool visualBullet;
    public AudioSource reloadAudio;
    public float damage;
    public bool owned;

    public Animator shootAnim;
    public bool singleShotDone = false;
    public AudioSource gunShotAudio;
    public TextMeshProUGUI ammoText;
    public Transform shootPos;

    public float shakeDuration;
    public float shakeAmount;

    private void OnEnable()
    {
        setAmmoText();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        isReloading = false;
        hasShot = false;
        shootAnim.Rebind();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            singleShotDone = false;
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && reserveAmmo > 0 && !hasShot && activeAmmo < clipSize)
        {
            isReloading = true;
            StartCoroutine(Reloading());
        }

        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    IEnumerator crosshairChange()
    {
        foreach (RawImage crossh in crossHairs)
        {
            crossh.color = Color.green;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (RawImage crossh in crossHairs)
        {
            crossh.color = Color.red;
        }
    }

    void Shoot()
    {
        if (activeAmmo > 0 && !singleShotDone && !hasShot && !isReloading)
        {
            if (!isAutomatic)
            {
                singleShotDone = true;
            }

            hasShot = true;
            StartCoroutine(TimeBetweenShot());

            activeAmmo -= 1;
            setAmmoText();

            camShake.shakeAmount = shakeAmount;
            camShake.shakeDuration = shakeDuration;

            //Shoot bullet
            if (!visualBullet)
            {
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                ray.origin = cam.transform.position;
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    hit.transform.gameObject.SendMessage("gotHit", damage, SendMessageOptions.DontRequireReceiver);
                    if (hit.transform.CompareTag("lips"))
                    {
                        StartCoroutine(crosshairChange());
                    }
                }
            }
            if (visualBullet)
            {
                GameObject bull = Instantiate(toothpasteBullet, shootPos.position, Quaternion.identity); //spawn bullet
                bull.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
            }

            //Play rest
            shootAnim.SetTrigger("shoot");
            gunShotAudio.Play();


        }
        else if (activeAmmo <= 0 && !hasShot)
        {
            if (reserveAmmo > 0 && !isReloading)
            {
                isReloading = true;
                StartCoroutine(Reloading());
            }
        }
    }

    IEnumerator Reloading()
    {
        isReloading = true;
        shootAnim.SetTrigger("reload");
        reloadAudio.Play();
        yield return new WaitForSeconds(reloadTime);

        int ammoToAdd = clipSize - activeAmmo;
        if (reserveAmmo <= ammoToAdd) 
        {
            activeAmmo += reserveAmmo;
            reserveAmmo = 0;
        }
        else
        {
            activeAmmo = clipSize;
            reserveAmmo -= ammoToAdd;
        }


        isReloading = false;
        hasShot = false;
        setAmmoText();
    }
    IEnumerator TimeBetweenShot()
    {
        yield return new WaitForSeconds(timeBetweenShot);
        hasShot = false;

        if (activeAmmo <= 0 && !hasShot)
        {
            if (reserveAmmo > 0 && !isReloading)
            {
                isReloading = true;
                StartCoroutine(Reloading());
            }
        }
    }

    public void setAmmoText()
    {
        ammoText.text = activeAmmo + "/" + reserveAmmo;
    }

}

/*if (reserveAmmo >= clipSize)
{
    activeAmmo = clipSize;
    reserveAmmo -= clipSize;
}
else if (reserveAmmo < clipSize)
{
    activeAmmo = reserveAmmo;
    reserveAmmo = 0;
}*/