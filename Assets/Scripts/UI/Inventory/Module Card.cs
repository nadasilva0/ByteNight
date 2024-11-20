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
    //Use an array of TMP_Text later to organize the stat descriptions;
    public TMP_Text upgradeText;
    public Module module;
    private GameObject Turret;
    TurretScript script;
    public Sprite[] moduleBackground;
    public Image imageComponent;

    private void Start()
    {
        Turret = GameObject.FindWithTag("Turret");
        script = Turret.GetComponent<TurretScript>();
    }

    public void setStatDisplay(Module _module)
    {
        // Change background depending on module quality
        imageComponent.sprite = moduleBackground[0];
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
            upgradeText.text += $"A +{module.bulletCount}\n";
        if (module.spreadAngle > 0) 
            upgradeText.text += $"B +{module.spreadAngle}\n";

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        script.AddModule(module);
        Destroy(gameObject);
    }
}
