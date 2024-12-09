using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretStatContoller : MonoBehaviour
{
    public TMP_Text[] stats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStats(int damage, float firedelay, int pierce, float shotspeed, float range, int bulletcount, float spreadangle)
    {
        if (damage <= 0)
        {
            stats[0].color = Color.red;
        }
        else
        {
            stats[0].color = Color.white;
        }
        if (pierce <= 0)
        {
            stats[2].color = Color.red;
        }
        else
        {
            stats[2].color = Color.white;
        }
        if (shotspeed <= 0)
        {
            stats[3].color = Color.red;
        }
        else
        {
            stats[3].color = Color.white;
        }
        if (range <= 0)
        {
            stats[4].color = Color.red;
        }
        else
        {
            stats[4].color = Color.white;
        }
        if (bulletcount <= 0)
        {
            stats[5].color = Color.red;
        }
        else
        {
            stats[5].color = Color.white;
        }

        stats[0].text = $"<sprite=0> {damage}\n";
        stats[1].text = $"<sprite=1> {Mathf.Round(firedelay * 100f) / 100f}<size=-10>s</size>\n";
        stats[2].text = $"<sprite=2> {pierce}\n";
        stats[3].text = $"<sprite=3> {shotspeed}<size=-10>u/s</size>\n";
        stats[4].text = $"<sprite=4> {Mathf.Round(range * 10f) / 10f}<size=-10>s</size>\n";
        stats[5].text = $"<sprite=5> {bulletcount}\n";
        stats[6].text = $"<sprite=6> {spreadangle}°\n";

    }
}
