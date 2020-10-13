using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenu;
    public GameObject Healthbar;
    public GameObject Nuts;
    public GameObject Timer;
    public GameObject HighscoreMenu;
    public GameObject ResumeBottun;
    public GameObject QuitBottun;
    public GameObject StasBottun;
    
    void Start(){
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }
    //funkcija koja vraca natrag u igru
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Healthbar.SetActive(true);
        Nuts.SetActive(true);
        Timer.SetActive(true);
    }

    //funkcija koja omogucuje prikaz PauseMenu i zaustavlja vrijeme igre
    void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Healthbar.SetActive(false);
        Nuts.SetActive(false);
        Timer.SetActive(false);
        HighscoreMenu.SetActive(false);
        ResumeBottun.SetActive(true);
        QuitBottun.gameObject.SetActive(true);
        StasBottun.gameObject.SetActive(true);
    }

    //play tipka koja pokrece sljedecu scenu
    public void Playgame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    //funkcija koja gasi app
    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetHSMenuActive(){
        HighscoreMenu.SetActive(true);
        ResumeBottun.SetActive(false);
        QuitBottun.gameObject.SetActive(false);
        StasBottun.gameObject.SetActive(false);
        //PauseMenu.SetActive(false);
    }
}
