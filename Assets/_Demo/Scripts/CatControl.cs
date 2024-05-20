using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CatControl : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    public LayerMask LayerSlot;
    public SlotControl ParentSlot;
    public GameObject CheckSlot;
    public int CatLevel = 0;
    public SpriteRenderer imCat;
 //   public List<Sprite> ListSpriteCat = new List<Sprite>();
    int OderLayer = 0;
    public Animator Anim;
    float timeMoney = 3f;
    public Text txtMoney;
    ulong _Money;
    // Start is called before the first frame update
    void Start()
    {
        OderLayer = imCat.sortingOrder;
        Debug.Log("oder:" + OderLayer);
        timeMoney = Random.Range(2.8f, 3.2f);

    }

    // Update is called once per frame
    void Update()
    {
        CheckRayCast();
        timeMoney -= Time.deltaTime;
        if (timeMoney < 0)
        {
            PlayAnim();
            timeMoney = Random.Range(2.8f, 3.2f);
            Gamecontrol.Instance.ChangeMoney(_Money);
            ChecktxtGold();
        }
    }
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        imCat.sortingOrder = OderLayer + 1;
    }
    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }
    private void OnMouseUp()
    {
        imCat.sortingOrder = OderLayer;
        CheckEndMove();
    }
    void CheckRayCast()
    {

        //Length of the ray
        float laserLength = 00.1f;
        Vector2 startPosition = (Vector2)transform.position;

        //Get the first object hit by the ray
        RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.right, laserLength, LayerSlot, 0);

        //If the collider of the object hit is not NUll
        if (hit.collider != null)
        {
            CheckSlot = hit.transform.gameObject;
        }
        else
        {
            CheckSlot = null;
        }

        //Method to draw the ray in scene for debug purpose
        Debug.DrawRay(startPosition, Vector2.right * laserLength, Color.red);
    }
    void CheckEndMove()
    {
        if (CheckSlot != null)
        {
            SlotControl scr = CheckSlot.GetComponent<SlotControl>();
            scr.CheckGetIn(this);
        }
        else
        {

            CancerMove();

        }

    }
    void CancerMove()
    {
        this.transform.position = new Vector3(ParentSlot.transform.position.x, ParentSlot.transform.position.y, -9);
    }
    public void SetValues(SlotControl _slot)
    {
        ParentSlot = _slot;
        CatLevel = _slot.LevelSlot;
        if (CatLevel < Gamecontrol.Instance.ListSpriteCat.Count)
        {
            imCat.sprite = Gamecontrol.Instance.ListSpriteCat[CatLevel];
        }
        ResetPosition();
        ChecktxtGold();
    }
    void ChecktxtGold()
    {
        _Money = Gamecontrol.Instance.checkMoneyLevel(CatLevel);
        ulong temp = (ulong)Gamecontrol.Instance.speedMoney * _Money;
        txtMoney.text = ConverMoney(temp);
    }
    string ConverMoney(ulong _mn)
    {
        string money = "";
        if (_mn < 1000)
        {
            money = "" + _mn;
        }
        else
        {
            if (_mn < 1000000)
            {
                int temp = (int)(_mn / 1000);
                money =  temp + "K";
            }
            else {

                if (_mn < 1000000000)
                {
                    int temp = (int)(_mn / 1000000);
                    money = temp + "M";
                }
                else
                {

                    if (_mn < 1000000000000)
                    {
                        int temp = (int)(_mn / 1000000000);
                        money = temp + "B";
                    }
                    else
                    {

                        if (_mn < 1000000000000000)
                        {
                            int temp = (int)(_mn / 1000000000000);
                            money = temp + "T";
                        }
                        else
                        {

                            if (_mn < 1000000000000000000)
                            {
                                int temp = (int)(_mn / 1000000000000000);
                                money = temp + "Q";
                            }
                        }
                    }
                }
            }
        }
        return money;
    
    }
    public void ResetPosition()
    {
        if (ParentSlot != null)
        {
            this.transform.parent = ParentSlot.transform;
            this.transform.position = new Vector3(ParentSlot.transform.position.x,ParentSlot.transform.position.y,-9);
        }
    }
    void PlayAnim()
    {
        Anim.Play("Cat_AddCoin");
    }
}
[System.Serializable]
public class InforPet {
    public int levelPet;
    public statusSlot statusSlot;
}
