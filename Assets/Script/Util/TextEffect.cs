using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TextEffect : MonoBehaviour
{
    public string gameSceneName;
    public PlayableDirector director;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(1.1f, 0.5f).SetLoops(-1,LoopType.Yoyo);
        director.gameObject.SetActive(true);
        director.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (director.state == PlayState.Paused && Input.anyKey) {
            director.gameObject.SetActive(false);
            transform.DOKill();
            SceneManager.LoadScene(gameSceneName); 
        }
    }
}
