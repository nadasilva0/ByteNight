using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapInventory : MonoBehaviour
{
    public GameObject ScrapInv;
    // Handles Audio
    public AudioSource equipSource;
    public AudioSource scrapSource;
    public AudioSource invOpenSource;
    public AudioSource invCloseSource;

    GameObject TurInv;
    ModuleHolder TurInvscript;
    // Annoying naming conventions because I renamed the original TurInv to ModuleHolder and don't have the time to rename every instance of it in every script
    TurretInventory trueTurInventoryScript;


    public bool menuOn = false;

    bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        TurInv = GameObject.FindWithTag("TurretInv");
        TurInvscript = TurInv.GetComponent<ModuleHolder>();
        trueTurInventoryScript = FindObjectOfType<TurretInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (trueTurInventoryScript.menuOn)
            {
                trueTurInventoryScript.closeMenu();
            }
            if (!menuOn)
            {
                invOpenSource.Play();
                ScrapInv.transform.localScale = Vector3.one;
                menuOn = true;
            }
            else
            {
                closeMenu();
            }
            //Debug.Log(menuOn);
        }
    }

    public void closeMenu()
    {
        invCloseSource.Play();
        ScrapInv.transform.localScale = Vector3.zero;
        menuOn = false;
    }

    public void PlaySound(AudioClip clip)
    { 
        equipSource.PlayOneShot(clip);
    }
}
