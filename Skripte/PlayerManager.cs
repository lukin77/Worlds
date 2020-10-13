using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private GameObject[] NutsSprites;
    public GameObject player;
    private Player playerScript;
    
    private float time;
    public Text text;

    public HighscoreTable highscore;

    private GameObject[] planet_list1;
    void Start(){
        NutsSprites = GameObject.FindGameObjectsWithTag("NutSprite");
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    // Update is called once per frame
    void Update()
    {
        if(player == null) return;
        else{
            if(player.GetComponent<Player>().GetHealth() <= 0){
                //endgame
                EndGameLose();
            }else if (player.GetComponent<Player>().GetNutsCollected() >= 1){
                //wingame, save prefs, highscore
                PlayerPrefs.SetFloat("Time",time);
                PlayerPrefs.Save();
                player.GetComponent<Player>().setPlayersPrefs();
                EndGameWon();
                
            }
            for (int i=0; i < player.GetComponent<Player>().GetNutsCollected(); i++){
            NutsSprites[i].SetActive(false);
            }
        }
        
        
        time += Time.deltaTime;
        var minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
    	var seconds = time % 60;//Use the euclidean division for the seconds.
    	var fraction = (time * 100) % 100;
    	text.text = string.Format ("{0:00} : {1:00} : {2:00}", minutes, seconds, fraction);
  
        

        
    }
    public void EndGameWon()
    {
        int jump = PlayerPrefs.GetInt("Jump");
        int nut = PlayerPrefs.GetInt("Nuts");
        float time = PlayerPrefs.GetFloat("Time");
        string name = PlayerPrefs.GetString("PlayerName");
        highscore.AddHighScoreEntry(time,jump,name);
        SceneManager.LoadScene("StasScene");
    }
    
    public void EndGameLose()
    {
        SceneManager.LoadScene("StasScene");
    }


    public void StvoriIgraca(GameObject playerPrefab){
        planet_list1 = GameObject.FindGameObjectsWithTag("Planet");
        Instantiate(playerPrefab,planet_list1[0].transform.position + new Vector3(0,planet_list1[0].transform.localScale.y/2,0) ,Quaternion.identity);
         //postavi planet za gravitaciju
        Player player = playerPrefab.GetComponent<Player>();
        player.SetPlanet(planet_list1[0]);
        }

}       

