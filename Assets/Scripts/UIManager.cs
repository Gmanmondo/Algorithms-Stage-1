using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas canvas;
    
    public void ChangeScenes(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
