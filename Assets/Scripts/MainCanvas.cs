using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCanvas : MonoBehaviour
{
    int levelNo;

    private void Start()
    {
        levelNo = PlayerPrefs.GetInt("Level", 1);
        SceneManager.LoadScene(levelNo);
    }
}
