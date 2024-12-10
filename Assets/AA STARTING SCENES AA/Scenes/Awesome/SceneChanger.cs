using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public string sceneName; // Assign the scene name in the Inspector

    public void OnButtonClick()
    {
        // Call the ChangeScene method from SimpleSceneTransition
        GameObject.FindObjectOfType<SimpleSceneTransition>().ChangeScene(sceneName);
    }
}
