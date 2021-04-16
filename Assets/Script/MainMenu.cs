using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void OnclickOnlineButton()
    {
        Debug.Log("click Online");
        LoadingsceneController.LoadScene("123");

    }
    public void OnclickQuitButton()
        {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit() // 어플리케이션 종료
#endif
    }
}