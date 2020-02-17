using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestartLvl(string lvl)
    {
        SceneManager.LoadScene(lvl);
    }
    public void MenuBtn()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void UndoBtn()
    {
        AllBoxesLvls.UndoMove();
    }
    public void Next(string nlvl)
    {
        SceneManager.LoadScene(nlvl);
    }
    public void Prev(string plvl)
    {
        SceneManager.LoadScene(plvl);
    }
}
