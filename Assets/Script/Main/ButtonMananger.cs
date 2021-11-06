using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ButtonMananger : MonoBehaviour
{
    [Header("Delimiter,SceneName")]
    public List<string> sceneNames = new List<string>();
    private Dictionary<string, string> sceneInfo = new Dictionary<string, string>();


    private void Start()
    {
        foreach(string name in sceneNames)
        {
            string[] splitName = name.Split(',');
            sceneInfo.Add(splitName[0], splitName[1]);
        }
    }
    public void OnPointerEnter(Button button) => button.transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    public void OnPointerExit(Button button) => button.transform.DORewind();
    //Button Event
    public void OnClickButton(Button button)
    {
        if (button.name.Contains("Start"))
            GotoScene(sceneInfo["Start"]);
        else if (button.name.Contains("Credit"))
            GotoScene(sceneInfo["Credit"]);
        else
            GameExit();
    }
    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
    public void GotoScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
