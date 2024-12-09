using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.GraphicsBuffer;

public class TurretInventory : MonoBehaviour
{
    public GameObject TurretInv;
    public AudioSource invOpenSource;
    public AudioSource invCloseSource;
    public AudioSource invCloseSource2;

    GameObject ScrapInv;
    ScrapHolder ScrapInvHolder;
    ScrapInventory ScrapInvMenu;

    GameObject Turret;
    TurretScript turretScript;

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

        Turret = GameObject.FindWithTag("Turret");
        turretScript = Turret.GetComponent<TurretScript>();
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
                StartCoroutine(changeCostume());
                TurretInv.transform.localScale = Vector3.zero;
                turretScript.upgradeDrillSparks.Play();
                turretScript.upgradeSparks.Play();
                turretScript.upgradeSpraypaint.Play();
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        equipSource.PlayOneShot(clip);
    }

    IEnumerator changeCostume()
    {
        yield return new WaitWhile(() => invCloseSource.isPlaying);
        turretScript.changeCostume();
        invCloseSource2.Play();
    }
}
