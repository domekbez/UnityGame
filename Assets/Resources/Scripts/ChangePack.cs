using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangePack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextPack(string lvl)
    {
        SceneManager.LoadScene(lvl);
    }
    public void PrevPack(string lvl)
    {
        SceneManager.LoadScene(lvl);
    }
}
