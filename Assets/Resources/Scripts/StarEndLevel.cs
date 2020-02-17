using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarEndLevel : MonoBehaviour
{
    public int howmanystars;
    public string nextlevel;
    public AudioSource starsound;
    public AudioSource winsound;


    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine("EndingLevel");

    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine("EndingLevel");
        //if (howmanystars == AllBoxesLvls.currstars)
           // winsound.Play();
    }
    public IEnumerator EndingLevel()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.03f);

            if (howmanystars == AllBoxesLvls.currstars)
            {
                howmanystars = 0;
                AllBoxesLvls.isFree = true;
                AllBoxesLvls.moves.Clear();
                winsound.Play();
                yield return new WaitForSeconds(3f);
                AllBoxesLvls.howmanymoves = 0;
                //winsound.Play();

                SceneManager.LoadScene(nextlevel);
                //AllBoxesLvls.howmanymoves = 0;

            }
            yield return new WaitForSeconds(0.03f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Box")
        {
            starsound.Play();
            collision.GetComponent<SpriteRenderer>().sprite = AllBoxesLvls.zlote;
            AllBoxesLvls.currstars++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Box")
        {
            collision.GetComponent<SpriteRenderer>().sprite = AllBoxesLvls.pudelko;
            AllBoxesLvls.currstars--;
        }
    }
}
