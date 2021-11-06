using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TextEffect : MonoBehaviour
{
    public int nextSceneInd;
    public PlayableDirector director;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(1.1f, 0.5f).SetLoops(-1,LoopType.Yoyo);

        if (director.ToString() == "null")
            return;
        director.gameObject.SetActive(true);
        director.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) {
            if (director.ToString() != "null")
                director.gameObject.SetActive(false);
            SoundController.instance.SoundControll("Eff_Accept");
            transform.DOKill();
            SceneManager.LoadScene(nextSceneInd);
        }
    }
}
