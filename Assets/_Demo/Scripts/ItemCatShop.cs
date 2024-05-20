using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCatShop : MonoBehaviour
{
    public Image imCat;
    public Button btBuy;
    public ulong Price;
    public int Level;
    public Text txtPirce;
    public Sprite spriteLock;
  public  int numbuy = 0;
    // Start is called before the first frame update
    void Start()
    {
        btBuy.onClick.AddListener(ClickBuyItem);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  public  void SetValues(int _lv)
    {
        Level = _lv;
        ChecknumBuy();
        CheckLock();
  
        


    }
    void ChecknumBuy()
    {
        if (Gamecontrol.Instance.listInforShop.Count > Level)
        {
            numbuy = Gamecontrol.Instance.listInforShop[Level].numBuy;
            Price = 1000 * Gamecontrol.Instance.checkMoneyLevel(Level);
            for (int i = 0; i < numbuy; i++)
            {
                Price *= 2;
            }
        }
    }
    public void CheckLock()
    {

        if (Level <= Gamecontrol.Instance.levelCat)
        {
            imCat.sprite = Gamecontrol.Instance.ListSpriteCat[Level];
            txtPirce.text = ConverMoney(Price);
        }
        else
        {
            imCat.sprite = spriteLock;
            txtPirce.text = "Lock";
        }
    }
    void ClickBuyItem()
    {
        SlotControl temp = Gamecontrol.Instance.checkSlot();
        if (temp != null)
        {
            if (Gamecontrol.Instance.Money >= Price)
            {
                Gamecontrol.Instance.SaveInforShop(Level);
                Gamecontrol.Instance.BuyCat(temp, Level,Price);
                ChecknumBuy();
                CheckLock();
            }
        }
        SoundControl.Instance.PlayClick();
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
                money = temp + "K";
            }
            else
            {

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
}
