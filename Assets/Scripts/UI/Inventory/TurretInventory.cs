using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretInventory : MonoBehaviour
{
    public GameObject TurretInv;
    public AudioSource invOpenSource;
    public AudioSource invCloseSource;

    GameObject ScrapInv;
    ScrapHolder ScrapInvHolder;
    ScrapInventory ScrapInvMenu;

    public bool menuOn = false;

    bool gameStarted = false;

    // Handles Audio
    public AudioSource equipSource;

    // Start is called before the first frame update
    void Start()
    {
        ScrapInv = GameObject.FindWithTag("ScrapInv");
        ScrapInvHolder = FindObjectOfType<ScrapHolder>();
        ScrapInvMenu = FindObjectOfType<ScrapInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !ScrapInvMenu.menuOn)
        {
            menuOn = !menuOn;
            if (menuOn)
            {
                invOpenSource.Play();
                TurretInv.transform.localScale = Vector3.one;
            }
            else
            {
                invCloseSource.Play();
                TurretInv.transform.localScale = Vector3.zero;
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        equipSource.PlayOneShot(clip);
    }
}
