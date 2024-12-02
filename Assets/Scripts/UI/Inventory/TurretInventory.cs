using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretInventory : MonoBehaviour
{
    public GameObject TurretInv;
    public AudioSource invOpenSource;
    public AudioSource invCloseSource;

    public bool menuOn = false;

    bool gameStarted = false;

    // Handles Audio
    public AudioSource equipSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TurretInv.SetActive(!TurretInv.activeSelf);
            menuOn = !menuOn;
            if (TurretInv.activeSelf)
            {
                invOpenSource.Play();
            }
            else
            {
                invCloseSource.Play();
            }
        }
    }

    private void LateUpdate()
    {
        //Dumb way of having inventory start closed while still giving everything a reference to it
        if (gameStarted == false)
        {
            gameStarted = true;
            TurretInv.SetActive(false);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        equipSource.PlayOneShot(clip);
    }
}
