using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GiftControl : MonoBehaviour
{
    public Button btGift;
    public Image imBlack;
    float timeGift = 60f;
    public GameObject PanelGift;
    public Image imItemGift;
    public Button btGetGift;
    public Text txtInforItem;
    public List<Sprite> ListItem = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        btGift.onClick.AddListener(ClickOpenGift);
        btGetGift.onClick.AddListener(CloseGift);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeGift >= 0)
        {
            timeGift -= Time.deltaTime;
            CheckTimeGift();
        }
      
    }
    void CheckTimeGift()
    {
        float temp = (float)timeGift / 60f;
        if (temp < 0)
        {
            temp = 0;
        }
        if (temp > 1)
        {
            temp = 1;
        }
        imBlack.fillAmount = temp;
    }
    void ClickOpenGift()
    {
        if (timeGift < 0)
        {
            int _gift = Random.Range(0, 3);
            switch (_gift)
            {
                case 0:
                    DoubleMoney();
                    break;
                case 1:
                    Magic();
                    break;
                case 2:
                    SpeedAddCat();
                    break;
       
                default:
                    break;
            }
            timeGift = 60;
        }
        SoundControl.Instance.PlayClick();
    }
    void DoubleMoney()
    {
        PanelGift.SetActive(true);
        imItemGift.sprite = ListItem[0];
        txtInforItem.text = "Double the money received";
        DoublleMoney.Instance.SetValues(15f);
    }
    void Magic()
    {
        PanelGift.SetActive(true);
        imItemGift.sprite = ListItem[1];
        txtInforItem.text = "Automatically merge cats";
        MagicControl.Instance.SetValues(30f);
    }
    void SpeedAddCat()
    {
        PanelGift.SetActive(true);
        imItemGift.sprite = ListItem[2];
        txtInforItem.text = "Reduce the time to add cats";
        UpSpeedAddCat.Instance.SetValues(30);
    }

    void CloseGift()
    {
        PanelGift.SetActive(false);
        SoundControl.Instance.PlayClick();
    }
}
