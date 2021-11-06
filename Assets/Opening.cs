using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class Opening : MonoBehaviour
{
    public PlayableDirector director;
    // Start is called before the first frame update
    void Start()
    {
        director.gameObject.SetActive(true);
        director.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (director.state == PlayState.Paused)
            SceneManager.LoadScene("GameScene");
    }
}
