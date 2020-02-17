using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllBoxesLvls : MonoBehaviour
{
    public AudioSource transpsound;

    public static AllBoxesLvls instance;
    public static bool isFree=true;
    private IEnumerator coroutine;

    public static int currstars;
    public static int howmanymoves;
    public static Text[] texts;
    public static Text mcounter;
    public static Sprite zlote;
    public static Sprite pudelko;
    public static Sprite fioletowe;
    public static Sprite zielone;
    public static Sprite niebieskie;



    public static List<(GameObject,bool)> boxes;
    public static GameObject[] boxestemp;
    public static List<List<((float, float), (float, float))>> moves;
    public static ((float, float), (float, float)) transport1;
    public static GameObject[] transport1temp;
    public static ((float, float), (float, float)) transport2;
    public static GameObject[] transport2temp;
    public static ((float, float), (float, float)) transport3;
    public static GameObject[] transport3temp;
    public static ParticleSystem prefab;


    void Awake()
    { //called when an instance awakes in the game
        instance = this; //set our static reference to our newly initialized instance
    }
    // Start is called before the first frame update
    void Start()
    {
        prefab = FindObjectOfType<ParticleSystem>();
        howmanymoves = 0;
        zlote = Resources.Load<Sprite>("Sprites/zlotepudelko1");
        pudelko = Resources.Load<Sprite>("Sprites/Pudelko1");
        fioletowe = Resources.Load<Sprite>("Sprites/Violet");
        zielone = Resources.Load<Sprite>("Sprites/Green");
        niebieskie = Resources.Load<Sprite>("Sprites/Blue");


        texts = FindObjectsOfType<Text>();
        foreach (var el in texts)
        {
            if (el.tag == "MoveCounter")
            {
                mcounter = el;
                break;
            }
        }
        moves = new List<List<((float, float), (float, float))>>();
        currstars = 0;
        boxes = new List<(GameObject,bool)>();
        boxestemp = GameObject.FindGameObjectsWithTag("Box");
        for (int i = 0; i < boxestemp.Length; i++)
        {
            boxes.Add((boxestemp[i],false));
        }

        InitializeTransport();
    }
    private void InitializeTransport()
    {
        transport1temp = GameObject.FindGameObjectsWithTag("Teleporter1");
        if (transport1temp.Length != 0)
        {
            transport1.Item1.Item1 = transport1temp[0].transform.position.x;
            transport1.Item1.Item2 = transport1temp[0].transform.position.y;
            transport1.Item2.Item1 = transport1temp[1].transform.position.x;
            transport1.Item2.Item2 = transport1temp[1].transform.position.y;
        }


        transport2temp = GameObject.FindGameObjectsWithTag("Teleporter2");
        if (transport2temp.Length != 0)
        {
            transport2.Item1.Item1 = transport2temp[0].transform.position.x;
            transport2.Item1.Item2 = transport2temp[0].transform.position.y;
            transport2.Item2.Item1 = transport2temp[1].transform.position.x;
            transport2.Item2.Item2 = transport2temp[1].transform.position.y;
        }
        transport3temp = GameObject.FindGameObjectsWithTag("Teleporter3");
        if (transport3temp.Length != 0)
        {
            transport3.Item1.Item1 = transport3temp[0].transform.position.x;
            transport3.Item1.Item2 = transport3temp[0].transform.position.y;
            transport3.Item2.Item1 = transport3temp[1].transform.position.x;
            transport3.Item2.Item2 = transport3temp[1].transform.position.y;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void UndoMove()
    {
        if (moves.Count == 0) return;
        howmanymoves--;
        mcounter.text = "MOVES: " + howmanymoves.ToString();

        List<((float, float), (float, float))> temp = moves[moves.Count - 1];
        moves.RemoveAt(moves.Count - 1);
        foreach (var el in temp)
        {
            bool painted = false;
            Collider2D hitColliders = Physics2D.OverlapCircle(new Vector2(el.Item2.Item1, el.Item2.Item2), 0.05f);
            if (hitColliders == null)
            {
            }
            else
            {
                hitColliders.transform.position = new Vector3(el.Item1.Item1, el.Item1.Item2);
                if (transport1temp.Length > 0)
                {
                    if (hitColliders.transform.position.x == transport1.Item1.Item1 && hitColliders.transform.position.y == transport1.Item1.Item2)
                    {
                        hitColliders.GetComponent<SpriteRenderer>().sprite = fioletowe;
                        painted = true;
                    }
                    if (hitColliders.transform.position.x == transport1.Item2.Item1 && hitColliders.transform.position.y == transport1.Item2.Item2)
                    {
                        painted = true;
                        hitColliders.GetComponent<SpriteRenderer>().sprite = fioletowe;
                    }
                }
                if (transport2temp.Length > 0)
                {
                    if (hitColliders.transform.position.x == transport2.Item1.Item1 && hitColliders.transform.position.y == transport2.Item1.Item2)
                    {
                        painted = true;
                        hitColliders.GetComponent<SpriteRenderer>().sprite = zielone;
                    }
                    if (hitColliders.transform.position.x == transport2.Item2.Item1 && hitColliders.transform.position.y == transport2.Item2.Item2)
                    {
                        painted = true;
                        hitColliders.GetComponent<SpriteRenderer>().sprite = zielone;
                    }
                }
                if (transport3temp.Length > 0)
                {
                    if (hitColliders.transform.position.x == transport3.Item1.Item1 && hitColliders.transform.position.y == transport3.Item1.Item2)
                    {
                        painted = true;
                        hitColliders.GetComponent<SpriteRenderer>().sprite = niebieskie;
                    }
                    if (hitColliders.transform.position.x == transport3.Item2.Item1 && hitColliders.transform.position.y == transport3.Item2.Item2)
                    {
                        painted = true;
                        hitColliders.GetComponent<SpriteRenderer>().sprite = niebieskie;
                    }
                }
                if(!painted)
                {
                    hitColliders.GetComponent<SpriteRenderer>().sprite = pudelko;

                }

            }

        }
    }

    public static void MoveBoxes(string a)
    {
        instance.StartCoroutine("MoveBoxes2", a);
    }
    public IEnumerator MoveBoxes2(string a)
    {
        //foreach (var el in boxes)
        //{
        //    if (el.Item1.GetComponent<SpriteRenderer>().sprite != zlote)
        //        el.Item1.GetComponent<SpriteRenderer>().sprite = pudelko;

        //}

        isFree = false;

        List<((float, float), (float, float))> oneMove = new List<((float, float), (float, float))>();
        if (a == "down")
        {
            bool isEmpty = false;
            for(int i=0;i<5;i++)
            {
                isEmpty = false;

                for (int j=0;j<7;j++)
                {
                    Collider2D hitColliders = Physics2D.OverlapCircle(new Vector2(-2f + i, -3f + j), 0.05f);
                    if(isEmpty)
                    {
                       

                        if (hitColliders == null) continue;
                        if (hitColliders.tag == "Box")
                        {
                            (GameObject, bool) tempp = (null, false);
                            for(int zz=0;zz<boxes.Count;zz++)
                            {
                                if (boxes[zz].Item1.transform.position == hitColliders.transform.position)
                                {
                                    tempp.Item1 = boxes[zz].Item1;
                                    tempp.Item2 = true;

                                    boxes[zz] = tempp;
                                    break;
                                }
                            }
                            oneMove.Add(((hitColliders.transform.position.x, hitColliders.transform.position.y),
                                (hitColliders.transform.position.x, hitColliders.transform.position.y - 1)));
                            hitColliders.transform.position = new Vector3(hitColliders.transform.position.x, hitColliders.transform.position.y - 1);
                            
                            continue;
                        }
                        if (hitColliders.tag == "BoxObstacle")
                        {
                            isEmpty = false;
                            continue;
                        }
                    }
                    if(hitColliders==null || (hitColliders.tag != "Box"&&hitColliders.tag!="BoxObstacle"))
                    {
                        isEmpty = true;

                    }

                }
            }
        }
        else if(a=="up")
        {
            bool isEmpty = false;
            for (int i = 0; i < 5; i++)
            {
                isEmpty = false;
                for (int j = 0; j < 7; j++)
                {

                    Collider2D hitColliders = Physics2D.OverlapCircle(new Vector2(-2f + i, 3f - j), 0.05f);
                    if (isEmpty)
                    {
                        if (hitColliders == null)
                        {
                            continue;
                        }
                        if (hitColliders.tag == "Box")
                        {
                            (GameObject, bool) tempp = (null, false);
                            for (int zz = 0; zz < boxes.Count; zz++)
                            {
                                if (boxes[zz].Item1.transform.position == hitColliders.transform.position)
                                {
                                    tempp.Item1 = boxes[zz].Item1;
                                    tempp.Item2 = true;

                                    boxes[zz] = tempp;
                                    break;
                                }
                            }
                            oneMove.Add(((hitColliders.transform.position.x, hitColliders.transform.position.y),
                                (hitColliders.transform.position.x, hitColliders.transform.position.y + 1)));
                            hitColliders.transform.position = new Vector3(hitColliders.transform.position.x, hitColliders.transform.position.y + 1);
                            
                            continue;
                        }
                        if(hitColliders.tag=="BoxObstacle")
                        {
                            isEmpty = false;
                            continue;
                        }
                    }
                    if (hitColliders == null || (hitColliders.tag != "Box" && hitColliders.tag != "BoxObstacle"))
                    {
                        isEmpty = true;

                    }

                }
            }
        }
    
        else if(a=="left")
        {
            bool isEmpty = false;
            for (int j = 0; j < 7; j++)
            {
                isEmpty = false;

                for (int i = 0; i < 5; i++)
                {
                    Collider2D hitColliders = Physics2D.OverlapCircle(new Vector2(-2f + i, 3f - j), 0.05f);
                    if (isEmpty)
                    {
                        if (hitColliders == null) continue;
                        if (hitColliders.tag == "Box")
                        {
                            (GameObject, bool) tempp = (null, false);
                            for (int zz = 0; zz < boxes.Count; zz++)
                            {
                                if (boxes[zz].Item1.transform.position == hitColliders.transform.position)
                                {
                                    tempp.Item1 = boxes[zz].Item1;
                                    tempp.Item2 = true;

                                    boxes[zz] = tempp;
                                    break;
                                }
                            }
                            oneMove.Add(((hitColliders.transform.position.x, hitColliders.transform.position.y),
                                (hitColliders.transform.position.x - 1, hitColliders.transform.position.y)));
                            hitColliders.transform.position = new Vector3(hitColliders.transform.position.x-1, hitColliders.transform.position.y);
                            
                            continue;
                        }
                        if (hitColliders.tag == "BoxObstacle")
                        {
                            isEmpty = false;
                            continue;
                        }
                    }
                    if (hitColliders == null|| (hitColliders.tag != "Box" && hitColliders.tag != "BoxObstacle"))
                    {
                        isEmpty = true;

                    }

                }
            }
        }
        else if(a=="right")
        {
            bool isEmpty = false;
            for (int j = 0; j < 7; j++)
            {
                isEmpty = false;

                for (int i = 0; i < 5; i++)
                {
                    Collider2D hitColliders = Physics2D.OverlapCircle(new Vector2(2f - i, 3f - j), 0.05f);
                    if (isEmpty)
                    {
                        if (hitColliders == null) continue;
                        if (hitColliders.tag == "Box")
                        {
                            (GameObject, bool) tempp = (null, false);
                            for (int zz = 0; zz < boxes.Count; zz++)
                            {
                                if (boxes[zz].Item1.transform.position == hitColliders.transform.position)
                                {
                                    tempp.Item1 = boxes[zz].Item1;
                                    tempp.Item2 = true;

                                    boxes[zz] = tempp;
                                    break;
                                }
                            }
                            oneMove.Add(((hitColliders.transform.position.x, hitColliders.transform.position.y),
                                (hitColliders.transform.position.x+1, hitColliders.transform.position.y)));
                            hitColliders.transform.position = new Vector3(hitColliders.transform.position.x+1, hitColliders.transform.position.y);
                            continue;
                        }
                        if (hitColliders.tag == "BoxObstacle")
                        {
                            isEmpty = false;
                            continue;
                        }
                    }
                    if (hitColliders == null || (hitColliders.tag != "Box" && hitColliders.tag != "BoxObstacle"))
                    {
                        isEmpty = true;

                    }

                }
            }
        }
        yield return new WaitForSecondsRealtime(0.03f);
        foreach (var el in boxes)
        {
            if (el.Item1.GetComponent<SpriteRenderer>().sprite != zlote)
                el.Item1.GetComponent<SpriteRenderer>().sprite = pudelko;

        }

        (GameObject,bool) boxintransp11 = (null,false);
        (GameObject,bool) boxintransp12 = (null,false);

        (GameObject,bool) boxintransp21 = (null,false);
        (GameObject,bool) boxintransp22= (null,false);

        (GameObject,bool) boxintransp31 = (null,false);
        (GameObject,bool) boxintransp32 = (null,false);
        foreach (var el in boxes)
        {
            if (transport1temp.Length > 0)
            {
                if (el.Item1.transform.position.x == transport1.Item1.Item1 && el.Item1.transform.position.y == transport1.Item1.Item2)
                {
                    boxintransp11 = el;
                    el.Item1.GetComponent<SpriteRenderer>().sprite = fioletowe;

                }
                if (el.Item1.transform.position.x == transport1.Item2.Item1 && el.Item1.transform.position.y == transport1.Item2.Item2)
                {
                    boxintransp12 = el;
                    el.Item1.GetComponent<SpriteRenderer>().sprite = fioletowe;

                }
            }
            if (transport2temp.Length > 0)
            {
                if (el.Item1.transform.position.x == transport2.Item1.Item1 && el.Item1.transform.position.y == transport2.Item1.Item2)
                {
                    el.Item1.GetComponent<SpriteRenderer>().sprite = zielone;

                    boxintransp21 = el;
                }
                if (el.Item1.transform.position.x == transport2.Item2.Item1 && el.Item1.transform.position.y == transport2.Item2.Item2)
                {
                    el.Item1.GetComponent<SpriteRenderer>().sprite = zielone;
                    boxintransp22 = el;
                }
            }
            if (transport3temp.Length > 0)
            {
                if (el.Item1.transform.position.x == transport3.Item1.Item1 && el.Item1.transform.position.y == transport3.Item1.Item2)
                {
                    el.Item1.GetComponent<SpriteRenderer>().sprite = niebieskie;
                    boxintransp31 = el;
                }
                if (el.Item1.transform.position.x == transport3.Item2.Item1 && el.Item1.transform.position.y == transport3.Item2.Item2)
                {
                    el.Item1.GetComponent<SpriteRenderer>().sprite = niebieskie;
                    boxintransp32 = el;
                }
            }
        }
        if(!boxintransp11.Item1&&!boxintransp12.Item1&&!boxintransp21.Item1&&!boxintransp22.Item1&&!boxintransp31.Item1&&!boxintransp32.Item1)
            yield return new WaitForSecondsRealtime(0.01f);
        else
            yield return new WaitForSecondsRealtime(0.5f);


        if (transport1temp.Length > 0)
        {
            if (boxintransp11.Item1 != null && boxintransp12.Item1 == null && boxintransp11.Item2)
            {
                transpsound.Play();
                boxintransp11.Item1.GetComponent<SpriteRenderer>().sprite = fioletowe;
                
                if (a=="down")
                    oneMove.Add(((transport1.Item1.Item1, transport1.Item1.Item2+1), (transport1.Item2.Item1, transport1.Item2.Item2)));
                if (a == "up")
                    oneMove.Add(((transport1.Item1.Item1, transport1.Item1.Item2-1), (transport1.Item2.Item1, transport1.Item2.Item2)));
                if (a == "left")
                    oneMove.Add(((transport1.Item1.Item1+1, transport1.Item1.Item2), (transport1.Item2.Item1, transport1.Item2.Item2)));
                if (a == "right")
                    oneMove.Add(((transport1.Item1.Item1-1, transport1.Item1.Item2), (transport1.Item2.Item1, transport1.Item2.Item2)));

                boxintransp11.Item1.transform.position = new Vector3(transport1.Item2.Item1, transport1.Item2.Item2);
            }
            if (boxintransp12.Item1 != null && boxintransp11.Item1 == null && boxintransp12.Item2)
            {
                transpsound.Play();

                boxintransp12.Item1.GetComponent<SpriteRenderer>().sprite = fioletowe;

                if (a=="down")
                    oneMove.Add(((transport1.Item2.Item1, transport1.Item2.Item2+1), (transport1.Item1.Item1, transport1.Item1.Item2)));
                if (a == "up")
                    oneMove.Add(((transport1.Item2.Item1, transport1.Item2.Item2-1), (transport1.Item1.Item1, transport1.Item1.Item2)));
                if (a == "left")
                    oneMove.Add(((transport1.Item2.Item1+1, transport1.Item2.Item2), (transport1.Item1.Item1, transport1.Item1.Item2)));
                if (a == "right")
                    oneMove.Add(((transport1.Item2.Item1-1, transport1.Item2.Item2), (transport1.Item1.Item1, transport1.Item1.Item2)));

                boxintransp12.Item1.transform.position = new Vector3(transport1.Item1.Item1, transport1.Item1.Item2);

            }
        }
        if (transport2temp.Length > 0)
        {
            if (boxintransp21.Item1 != null && boxintransp22.Item1 == null && boxintransp21.Item2)
            {
                transpsound.Play();

                boxintransp21.Item1.GetComponent<SpriteRenderer>().sprite = zielone;

                if (a == "down")
                    oneMove.Add(((transport2.Item1.Item1, transport2.Item1.Item2 + 1), (transport2.Item2.Item1, transport2.Item2.Item2)));
                if (a == "up")
                    oneMove.Add(((transport2.Item1.Item1, transport2.Item1.Item2 - 1), (transport2.Item2.Item1, transport2.Item2.Item2)));
                if (a == "left")
                    oneMove.Add(((transport2.Item1.Item1 + 1, transport2.Item1.Item2), (transport2.Item2.Item1, transport2.Item2.Item2)));
                if (a == "right")
                    oneMove.Add(((transport2.Item1.Item1 - 1, transport2.Item1.Item2), (transport2.Item2.Item1, transport2.Item2.Item2)));
                boxintransp21.Item1.transform.position = new Vector3(transport2.Item2.Item1, transport2.Item2.Item2);
            }
            if (boxintransp22.Item1 != null && boxintransp21.Item1 == null && boxintransp22.Item2)
            {

                transpsound.Play();
                boxintransp22.Item1.GetComponent<SpriteRenderer>().sprite = zielone;

                if (a == "down")
                    oneMove.Add(((transport2.Item2.Item1, transport2.Item2.Item2 + 1), (transport2.Item1.Item1, transport2.Item1.Item2)));
                if (a == "up")
                    oneMove.Add(((transport2.Item2.Item1, transport2.Item2.Item2 - 1), (transport2.Item1.Item1, transport2.Item1.Item2)));
                if (a == "left")
                    oneMove.Add(((transport2.Item2.Item1 + 1, transport2.Item2.Item2), (transport2.Item1.Item1, transport2.Item1.Item2)));
                if (a == "right")
                    oneMove.Add(((transport2.Item2.Item1 - 1, transport2.Item2.Item2), (transport2.Item1.Item1, transport2.Item1.Item2)));
                boxintransp22.Item1.transform.position = new Vector3(transport2.Item1.Item1, transport2.Item1.Item2);
            }
        }
        if (transport3temp.Length > 0)
        {
            if (boxintransp31.Item1 != null && boxintransp32.Item1 == null && boxintransp31.Item2)
            {
                transpsound.Play();
                boxintransp31.Item1.GetComponent<SpriteRenderer>().sprite = niebieskie;

                if (a == "down")
                    oneMove.Add(((transport3.Item1.Item1, transport3.Item1.Item2 + 1), (transport3.Item2.Item1, transport3.Item2.Item2)));
                if (a == "up")
                    oneMove.Add(((transport3.Item1.Item1, transport3.Item1.Item2 - 1), (transport3.Item2.Item1, transport3.Item2.Item2)));
                if (a == "left")
                    oneMove.Add(((transport3.Item1.Item1 + 1, transport3.Item1.Item2), (transport3.Item2.Item1, transport3.Item2.Item2)));
                if (a == "right")
                    oneMove.Add(((transport3.Item1.Item1 - 1, transport3.Item1.Item2), (transport3.Item2.Item1, transport3.Item2.Item2)));

                boxintransp31.Item1.transform.position = new Vector3(transport3.Item2.Item1, transport3.Item2.Item2);
            }
            if (boxintransp32.Item1 != null && boxintransp31.Item1 == null && boxintransp32.Item2)
            {
                transpsound.Play();
                boxintransp32.Item1.GetComponent<SpriteRenderer>().sprite = niebieskie;
                if (a == "down")
                    oneMove.Add(((transport3.Item2.Item1, transport3.Item2.Item2 + 1), (transport3.Item1.Item1, transport3.Item1.Item2)));
                if (a == "up")
                    oneMove.Add(((transport3.Item2.Item1, transport3.Item2.Item2 - 1), (transport3.Item1.Item1, transport3.Item1.Item2)));
                if (a == "left")
                    oneMove.Add(((transport3.Item2.Item1 + 1, transport3.Item2.Item2), (transport3.Item1.Item1, transport3.Item1.Item2)));
                if (a == "right")
                    oneMove.Add(((transport3.Item2.Item1 - 1, transport3.Item2.Item2), (transport3.Item1.Item1, transport3.Item1.Item2)));
                boxintransp32.Item1.transform.position = new Vector3(transport3.Item1.Item1, transport3.Item1.Item2);
            }
        }
        if (oneMove.Count > 0)
        {
            howmanymoves++;
            moves.Add(oneMove);
            mcounter.text = "MOVES: " + howmanymoves.ToString();
        }
        isFree = true;
        for(int zzz=0;zzz<boxes.Count;zzz++)
        {
            boxes[zzz] = (boxes[zzz].Item1, false);
        }
      
    }
}
