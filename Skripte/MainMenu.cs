using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject HighscoreMenu;
    public GameObject StartBottun;
    public GameObject QuitBottun;
    public GameObject HSBottun;
    public GameObject Inputfield;
    public GameObject text;
    public GameObject PlayBottun;
    public GameObject BackBottun;
    public string theName;


    public void StartGame(){
        theName = text.GetComponent<Text>().text;
        if(theName != string.Empty){
            PlayerPrefs.SetString("PlayerName" , theName);
            PlayerPrefs.Save();
            SceneManager.LoadScene("NewGame");
        }
    }

    public void ClickStart(){
        StartBottun.SetActive(false);
        QuitBottun.gameObject.SetActive(false);
        HSBottun.gameObject.SetActive(false);
        Inputfield.SetActive(true);
        PlayBottun.SetActive(true);
    }

    public void SetHSMenuDeactivate(){
        HighscoreMenu.SetActive(false);
        StartBottun.SetActive(true);
        QuitBottun.gameObject.SetActive(true);
        HSBottun.gameObject.SetActive(true);
        BackBottun.SetActive(false);
        //PauseMenu.SetActive(false);
    }

    public void SetHSMenuActive(){
        HighscoreMenu.SetActive(true);
        StartBottun.SetActive(false);
        QuitBottun.gameObject.SetActive(false);
        HSBottun.gameObject.SetActive(false);
        BackBottun.SetActive(true);
        //PauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
