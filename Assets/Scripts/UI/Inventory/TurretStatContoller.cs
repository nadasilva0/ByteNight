using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TurretStatContoller : MonoBehaviour
{
    public List<TMP_Text> statsText;
    public List<GameObject> stats;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStats(int damage, float firedelay, int pierce, float shotspeed, float range, int bulletcount, float spreadangle, float homingStrength)
    {
        if (damage <= 0)
        {
            statsText[0].color = Color.red;
        }
        else
        {
            statsText[0].color = Color.white;
        }
        if (pierce <= 0)
        {
            statsText[2].color = Color.red;
        }
        else
        {
            statsText[2].color = Color.white;
        }
        if (shotspeed <= 0)
        {
            statsText[3].color = Color.red;
        }
        else
        {
            statsText[3].color = Color.white;
        }
        if (range <= 0)
        {
            statsText[4].color = Color.red;
        }
        else
        {
            statsText[4].color = Color.white;
        }
        if (bulletcount <= 0)
        {
            statsText[5].color = Color.red;
        }
        else
        {
            statsText[5].color = Color.white;
        }

        statsText[0].text = $"<sprite=0>{damage}\n";
        statsText[1].text = $"<sprite=1>{Mathf.Round(firedelay * 100f) / 100f}<size=-10>s</size>\n";
        statsText[2].text = $"<sprite=2>{pierce}\n";
        statsText[3].text = $"<sprite=3>{shotspeed}<size=-10>u/s</size>\n";
        statsText[4].text = $"<sprite=4>{Mathf.Round(range * 10f) / 10f}<size=-10>s</size>\n";
        statsText[5].text = $"<sprite=5>{bulletcount}\n";
        statsText[6].text = $"<sprite=6>{spreadangle}°\n";
        if (homingStrength > 0 )
        {
            stats[7].SetActive( true );
            statsText[7].text = $"<sprite=7>{Mathf.Round((homingStrength / 10) * 10f) / 10f}\n";
        }
        else
        {
            stats[7].SetActive(false);
        }

    }
}
