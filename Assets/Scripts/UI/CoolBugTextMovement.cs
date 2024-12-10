using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolBugTextMovement : MonoBehaviour
{
    public bool isCoolBugOnScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCoolBugOnScreen)
        {
            transform.position += new Vector3(0.3f, 0);
        }
    }
}
