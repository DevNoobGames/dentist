using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introsequence : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    public GameObject text;
    public GameObject devnoob;
    public GameObject textFaint;
    public GameObject blackoutpanel;
    public AudioSource bgMusic;
    public AudioSource scare;

    private void Start()
    {
        StartCoroutine(cor());
    }

    IEnumerator cor()
    {
        cam1.SetActive(true);
        yield return new WaitForSeconds(14);
        text.SetActive(true);
        yield return new WaitForSeconds(16);
        cam1.SetActive(false);
        cam2.SetActive(true);
        yield return new WaitForSeconds(2);
        bgMusic.Stop();
        scare.Play();
        devnoob.SetActive(true);
        yield return new WaitForSeconds(2);
        textFaint.SetActive(true);
        yield return new WaitForSeconds(4);
        blackoutpanel.SetActive(true);
        yield return new WaitForSeconds(3.5f);

        SceneManager.LoadScene("sampleScene");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("sampleScene");
        }
    }
}
