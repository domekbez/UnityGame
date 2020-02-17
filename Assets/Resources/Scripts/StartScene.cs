using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public Sprite audiooff;
    public Sprite audioon;
    Image dzwiekikona;
    Image[] zdjecia;
    int audioint;

    // Start is called before the first frame update
    void Start()
    {

        zdjecia = FindObjectsOfType<Image>();
        foreach (var el in zdjecia)
        {
            if (el.gameObject.tag == "SoundIcon")
                dzwiekikona = el;
        }
        audiooff = Resources.Load<Sprite>("Sprites/speaker-off");
        audioon = Resources.Load<Sprite>("Sprites/speaker");
        audioint = PlayerPrefs.GetInt("audio");
        if (audioint == 0)
        {
            dzwiekikona.sprite = audioon;
            AudioListener.pause = false;
        }
        else
        {
            dzwiekikona.sprite = audiooff;
            AudioListener.pause = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeMute()
    {
        audioint = PlayerPrefs.GetInt("audio");
        if(audioint==0)
        {
            AudioListener.pause = true;
            PlayerPrefs.SetInt("audio", 1);
            dzwiekikona.sprite = audiooff;
        }
        else
        {
            AudioListener.pause = false;
            PlayerPrefs.SetInt("audio", 0);
            dzwiekikona.sprite = audioon;

        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void SelectLevel()
    {
        SceneManager.LoadScene("SelectLevelScene");

    }
}
