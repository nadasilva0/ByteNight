using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatHoverDescription : MonoBehaviour
{
    private GameObject textBox;
    private TMP_Text statText;
    public string mouseOverText;
    // Start is called before the first frame update
    void Start()
    {
        textBox = GameObject.FindGameObjectWithTag("DescBox");
        statText = textBox.GetComponent<TMP_Text>();
    }

    public void OnMouseOver()
    {
        textBox.SetActive(true);
    }

    public void OnMouseExit()
    {
        textBox.SetActive(false);
    }
}
