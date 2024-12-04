using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugControls : MonoBehaviour
{
    GameObject Turret;
    TurretScript script;
    GameObject Conveyor;
    ModuleMaker inventoryScript;
    // Start is called before the first frame update
    void Start()
    {
        Turret = GameObject.FindWithTag("Turret");
        script = Turret.GetComponent<TurretScript>();
        Conveyor = GameObject.FindWithTag("Conveyor");
        inventoryScript = Conveyor.GetComponent<ModuleMaker>();
    }

    // Update is called once per frame
    void Update()
    {
        //Increases
        if (Input.GetKeyDown(KeyCode.Alpha1) && script.fireDelay >= 0)
        {
            script.fireDelay -= 0.05f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            script.damage += 1;
            script.changeCostume();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            script.shotSpeed += 1f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            script.bulletLifetime += 0.05f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && script.spreadAngle <= 25)
        {
            script.spreadAngle += 1f;
            script.changeCostume();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            script.bulletCount += 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            script.pierce += 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            script.range += 0.1f;
        }
        //Decreases
        if (Input.GetKeyDown(KeyCode.Q))
        {
            script.fireDelay += 0.05f;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            script.damage -= 1;
            script.changeCostume();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            script.shotSpeed -= 1f;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            script.bulletLifetime -= 0.05f;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            script.spreadAngle -= 1f;
            script.changeCostume();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            script.bulletCount -= 1;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            script.pierce -= 1;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            script.range -= 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            inventoryScript.CreateStatModule(0);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            inventoryScript.CreateStatModule(5);
        }

        if (Input.GetKeyDown(KeyCode.Comma))
        {
            inventoryScript.CreateStatModule(10);
        }
    }
}
