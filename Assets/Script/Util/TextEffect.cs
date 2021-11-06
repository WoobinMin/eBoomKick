using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TextEffect : MonoBehaviour
{
    public string gameSceneName;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.DOScale(1.1f, 0.5f).SetLoops(-1,LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) { SceneManager.LoadScene(gameSceneName); }
    }
}
