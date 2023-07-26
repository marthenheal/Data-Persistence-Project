using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)] 
public class MenuUIHandler : MonoBehaviour
{
    public string playerName;
    public TextMeshProUGUI bestScoreText;

    public string bestScoreName;
    public int bestScore;

    private void Start()
    {
        bestScoreText.text = LoadNameAndScore();
        if (bestScoreText.text == null)
        {
            bestScoreText.text = "There is no best score yet!";
        }
    }

    public void GetPlayerName(string inputName)
    {
        playerName = inputName;
        DataManager.instance.playerName = playerName;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
    Application.Quit;
#endif
    }

    [System.Serializable]
    class SaveData
    {
        public string name;
        public int highScore;
    }

    public string LoadNameAndScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScoreName = data.name;
            bestScore = data.highScore;
            return "Best score: " + bestScoreName + " : " + bestScore;
        }
        return null;
    }

    public void DeleteData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            File.Delete(path);
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
            bestScoreText.text = "Best score: ";
        }
    }
}
