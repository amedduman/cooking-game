using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OnClick_LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
