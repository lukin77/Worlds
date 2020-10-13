using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List <Transform> highscoreEntryTransformList;
    
    

    private void Awake(){
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        //sort
        for (int i=0; i < highscores.highscoreEntryList.Count;i++) {
            for (int j=0; j < highscores.highscoreEntryList.Count;j++ ){
                if(highscores.highscoreEntryList[i].time > highscores.highscoreEntryList[j].time){
                    //swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }   
        }


        highscoreEntryTransformList = new List<Transform>();

        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList ){
            CreateHighScoreEntryTransform(highscoreEntry, entryContainer,highscoreEntryTransformList );
        }

    }

    private void CreateHighScoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList){

        float templeHeight = 50f;
        Transform entrytransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entrytransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2 (0 , -templeHeight * transformList.Count);
        entrytransform.gameObject.SetActive(true);

        int rank = transformList.Count+1;
        string rankString;
        switch(rank){
        default:
            rankString = rank + "th"; break;
            case 1: rankString = "1st"; break;
            case 2: rankString = "2nd"; break;
            case 3: rankString = "3rd"; break;
        }
        float time = highscoreEntry.time;
        int jumps = highscoreEntry.jumps;
        string name = highscoreEntry.name;

        entrytransform.Find("posText").GetComponent<Text>().text = rankString;
        entrytransform.Find("nameText").GetComponent<Text>().text = name;
        entrytransform.Find("timeText").GetComponent<Text>().text = time.ToString() + " sec";
        entrytransform.Find("jump").GetComponent<Text>().text = jumps.ToString();

        entrytransform.Find("background").gameObject.SetActive(rank % 2 == 1);
        transformList.Add(entrytransform);

    }

    private class Highscores {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry{
        public float time;
        public int jumps;
        public string name;
    }

    public void AddHighScoreEntry(float time, int jumps, string name){
        //create highscoreentry
        HighscoreEntry highscoreEntry = new HighscoreEntry {time = time, jumps = jumps, name = name};
        //load saved highscore
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        //add new entry to highscores
        highscores.highscoreEntryList.Add(highscoreEntry);
        //save updated highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    public void tryAgain(){
        SceneManager.LoadScene("NewGame");
    }

}
