using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

// Handles the display of the module card
public class ModuleCard : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text upgradeText;
    public Module module;
    private GameObject Turret;
    TurretScript script;

    GameObject TurInv;
    ModuleHolder TurInvscript;

    public Sprite[] moduleBackground;
    public Image imageComponent;
    [SerializeField] private bool inTurret = false;
    GameObject Conveyor;
    ModuleMaker ConveyorInvScript;

    GameObject ScrapInv;
    ScrapHolder ScrapInvHolder;
    ScrapInventory ScrapInvMenu;

    // Annoying naming conventions because I renamed the original TurInv to ModuleHolder and don't have the time to rename every instance of it in every script
    TurretInventory trueTurInventoryScript;

    public AudioClip equipSound;
    public AudioClip unequipSound;

    private void Start()
    {
        Turret = GameObject.FindWithTag("Turret");
        script = Turret.GetComponent<TurretScript>();

        TurInv = GameObject.FindWithTag("TurretInv");
        TurInvscript = TurInv.GetComponent<ModuleHolder>();

        Conveyor = GameObject.FindWithTag("Conveyor");
        ConveyorInvScript = Conveyor.GetComponent<ModuleMaker>();

        trueTurInventoryScript = FindObjectOfType<TurretInventory>();

        ScrapInv = GameObject.FindWithTag("ScrapInv");
        ScrapInvHolder = FindObjectOfType<ScrapHolder>();
        ScrapInvMenu = FindObjectOfType<ScrapInventory>();

        Debug.Log(TurInv);
        Debug.Log(TurInvscript);
    }

    public void setStatDisplay(Module _module, int moduleBg)
    {
        // Change background depending on module quality
        imageComponent.sprite = moduleBackground[moduleBg];
        upgradeText.text = "";
        module = _module;
        // Positive stats
        if (module.damage > 0) 
            upgradeText.text += $"<sprite=0> +{module.damage}\n";
        if (module.fireDelay > 0)
            upgradeText.text += $"<sprite=1> -{module.fireDelay}\n";
        if (module.pierce > 0)
            upgradeText.text += $"<sprite=2> +{module.pierce}\n"; 
        if (module.shotSpeed > 0)
            upgradeText.text += $"<sprite=3> +{module.shotSpeed}\n";
        if (module.range > 0)
            upgradeText.text += $"<sprite=4> +{module.range}\n";
        if (module.bulletCount > 0) 
            upgradeText.text += $"<sprite=5> +{module.bulletCount}\n";
        if (module.spreadAngle > 0) 
            upgradeText.text += $"<sprite=6> +{module.spreadAngle}\n";
        if (module.homingStrength > 0)
            upgradeText.text += $"<sprite=7> +{module.homingStrength}\n";

    }
    
    public void CantEquipModule(string reason)
    {
        Debug.Log(reason);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Yanderedev ass code
        if (trueTurInventoryScript.menuOn)
        {
            if (module.InTurret == false)
            {
                if (script.modules.Count <= 5)
                {
                    script.AddModule(module);
                    TurInvscript.AddModule(module);
                    trueTurInventoryScript.PlaySound(equipSound);
                    Destroy(gameObject);
                }
                else
                {
                    CantEquipModule("Inventory Full!");
                }
            }
            else
            {
                script.RemoveModule(module);
                ConveyorInvScript.CreateModuleCard(module);
                trueTurInventoryScript.PlaySound(unequipSound);
                Destroy(gameObject);
            }
            
        }
        else if (ScrapInvMenu.menuOn)
        {
            if (module.InTurret == false)
            {
                if (ScrapInvHolder.modules.Count <= 1)
                {
                    ScrapInvHolder.AddModule(module);
                    trueTurInventoryScript.PlaySound(equipSound);
                    Destroy(gameObject);
                }
                else
                {
                    CantEquipModule("Scrap Slots Full!");
                }
            }
            else
            {
                ScrapInvHolder.RemoveModule(module);
                ConveyorInvScript.CreateModuleCard(module); // In the future probably have this done on the Scrap Inventory script
                trueTurInventoryScript.PlaySound(unequipSound);
                Destroy(gameObject);
            }
        }
        else
        {
            CantEquipModule("Menu not open!");
        }
        
    }
}
