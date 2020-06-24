using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public int SceneNum = 0;



    public void PlayScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNum, LoadSceneMode.Single);
    }
}