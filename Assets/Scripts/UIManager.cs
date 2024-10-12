using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;

    private void Start()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void OptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
