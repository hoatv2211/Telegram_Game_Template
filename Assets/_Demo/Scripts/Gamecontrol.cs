using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamecontrol : MonoBehaviour
{
    public static Gamecontrol Instance;
    public GameObject SlotCat;
    public List<Sprite> ListSpriteCat = new List<Sprite>();
    [HideInInspector]
    public List<SlotControl> ListSlotCast = new List<SlotControl>();
    [HideInInspector]
    public List<ItemCatShop> ListItemShop = new List<ItemCatShop>();
    [HideInInspector]
    public List<InforShop> listInforShop = new List<InforShop>();

    public ulong Money;
    public Text txtMoney;
    [HideInInspector]
    public int levelCat = 0;
    float TimeAddCat = 5.9f;
    public Text txtTimeCat;
    public Button btCat;
    public Image imBlackCat;
    [Header("UI Shop")]
    public GameObject PItemCatShop;
    public Transform ParentShop;
    public GameObject PanelShop;
    public Button btOpenShop;
    public Button btCloseShop;
    [Header("UI New Cat")]
    public GameObject PanelNewCat;
    public Image imNewCat;
    public Button btCloseNewCat;

    [Header("Other")]

  //  [HideInInspector]
  public  int speedMoney = 1;
    [HideInInspector]
   public int speedAddCats = 1;
    [Header("Efect")]
    public GameObject EFAddCat;
    public GameObject EFMerge;
    public GameObject EFNewCat;



    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.DeleteAll();
        GetAllSlot();
        GetMoney();
        levelCat = PlayerPrefs.GetInt(KeySave.LevelCat, 0);
        btCat.onClick.AddListener(ClickBtCat);
        GetInforShop();
        AddItemShop();
        btCloseNewCat.onClick.AddListener(ClosePanelNewCat);
        btOpenShop.onClick.AddListener(OpenShop);
        btCloseShop.onClick.AddListener(CloseShop);
       

    }

    private void ClickBtCat()
    {
        TimeAddCat -= 1;
        SoundControl.Instance.PlayClick();
    }

    // Update is called once per frame
    void Update()
    {
        AutoAddCat();
    }
    void OpenShop()
    {
        PanelShop.SetActive(true);
        SoundControl.Instance.PlayClick();
    }
    void CloseShop()
    {
        PanelShop.SetActive(false);
        SoundControl.Instance.PlayClick();
    }
    void AutoAddCat()
    { if (TimeAddCat >= 0)
        {
            TimeAddCat -= speedAddCats * Time.deltaTime;
            int temp = (int)TimeAddCat;
            txtTimeCat.text = temp + "";
            CheckImBlackCat();
        }
        else
        {

            SlotControl temp = checkSlot();
            if (temp != null)
            {

                temp.AddCats();
                Gamecontrol.Instance.SaveInforCat();
                TimeAddCat = 5.9f;
            }
            else
            {
                txtTimeCat.text = "Full";
            }
        }
        //AdsManager.Instance.ShowFull();
    }

    public void BuyCat(SlotControl _slot, int _levelCat, ulong _mn)
    {
        _slot.BuyCats(_levelCat);
        Money -= _mn;
        txtMoney.text = "" + Money;
        PlayerPrefs.SetString(KeySave.Gold, txtMoney.text);
    }
    public void SaveInforCat()
    { List<InforSlot> ListInforSlot = new List<InforSlot>();

        for (int i = 0; i < ListSlotCast.Count; i++)
        {
            InforSlot temp = new InforSlot();
            temp.Level = ListSlotCast[i].LevelSlot;
            ListInforSlot.Add(temp);
        }
        string values = JsonHelper.ToJson<InforSlot>(ListInforSlot);
        PlayerPrefs.SetString(KeySave.InforSlotCat, values);
        PlayerPrefs.Save();
        Debug.Log("saveInforCat");
    }
    void GetInforShop()
    {
        string _infor = PlayerPrefs.GetString(KeySave.InforShop, "null");
        if (_infor == "null")
        {
            for (int i = 0; i < ListSpriteCat.Count; i++)
            {
                InforShop temp = new InforShop();
                listInforShop.Add(temp);
            }
        }
        else
        {
            listInforShop = JsonHelper.JsontoList<InforShop>(_infor);
        }

    }
    public void SaveInforShop(int _id)
    {
        listInforShop[_id].numBuy += 1;
        string vl = JsonHelper.ToJson<InforShop>(listInforShop);
        PlayerPrefs.SetString(KeySave.InforShop, vl);
        PlayerPrefs.Save();
    }
    void CheckImBlackCat()
    {
        float temp = (TimeAddCat / 5.9f);
        if (temp <= 0) { temp = 0; }
        else
        { if (temp >= 1)
            {

                temp = 1;
            }

        }
        imBlackCat.fillAmount = temp;
    }
    public SlotControl checkSlot()
    {
        SlotControl temp = null;
        for (int i = 0; i < ListSlotCast.Count; i++)
        {
            if (ListSlotCast[i]._status == statusSlot.Null)
            {
                temp = ListSlotCast[i];
                return temp;
            }
        }
        return temp;
    }

    void GetAllSlot()
    {
        Debug.Log("Get Data");
        string data = PlayerPrefs.GetString(KeySave.InforSlotCat, "null");
        if (data != "null")
        {
            Debug.Log("Data:" + data);
            InforSlot[] arrSlot = JsonHelper.FromJson<InforSlot>(data);
            for (int i = 0; i < arrSlot.Length; i++)
            {
                GameObject Slot = GameObject.Instantiate(SlotCat, null);
                SlotControl scr = Slot.GetComponent<SlotControl>();
                scr.LevelSlot = arrSlot[i].Level;
                scr._status = statusSlot.Null;
                ListSlotCast.Add(scr);
                Vector3 tempPos = new Vector3(0, 0, 0);

                tempPos.x = -3f + 2f * (i % 4);
                tempPos.y = 4f - 2f * ((int)(i / 4));
                Slot.transform.position = tempPos;
                Slot.gameObject.name = "SlotCat" + i;



            }
        }
        else
        {
            Debug.Log("Data = null");
            List<InforSlot> listSlot = new List<InforSlot>();
            for (int i = 0; i < 20; i++)
            {
                InforSlot temp = new InforSlot();
                if (i < 2)
                {
                    temp.Level = 0;
                }
                else
                {
                    temp.Level = -1;
                }

                listSlot.Add(temp);

            }
            string _data = JsonHelper.ToJson<InforSlot>(listSlot);
            PlayerPrefs.SetString(KeySave.InforSlotCat, _data);
            PlayerPrefs.Save();
            GetAllSlot();

        }
    }
    public void ChangeMoney(ulong _mn)
    {
        _mn = (ulong) speedMoney * _mn;
        Money += _mn;
        txtMoney.text = "" + Money;
        PlayerPrefs.SetString(KeySave.Gold, txtMoney.text);
    }
    void GetMoney()
    {
        string vl = PlayerPrefs.GetString(KeySave.Gold, "0");
        Money = ulong.Parse(vl);
        txtMoney.text = "" + Money;
    }
    void AddItemShop()
    {
        for (int i = 0; i < ListSpriteCat.Count; i++)
        {
            GameObject ItemCat = GameObject.Instantiate(PItemCatShop, null);
            ItemCat.transform.parent = ParentShop;
            ItemCat.transform.localScale = new Vector3(1, 1, 1);
            ItemCatShop scr = ItemCat.GetComponent<ItemCatShop>();
            scr.SetValues(i);
            ListItemShop.Add(scr);
        }
    }
    public void CheckNewCat(int Level)
    {

        if (levelCat < Level)
        {
            levelCat = Level;
            PlayerPrefs.SetInt(KeySave.LevelCat, Level);
            PlayerPrefs.Save();
            for (int i = 0; i < ListItemShop.Count; i++)
            {
                ListItemShop[i].CheckLock();
            }
            imNewCat.sprite = ListSpriteCat[levelCat];
            PanelNewCat.SetActive(true);
            AddEFNewCat(imNewCat.transform.position);
        }
    }
    void ClosePanelNewCat()
    {
        PanelNewCat.SetActive(false);
        SoundControl.Instance.PlayClick();
    }
    public ulong checkMoneyLevel(int _lv)
    {

        ulong _Money = 1;
        for (int i = 0; i < _lv; i++)
        {
            _Money *= 2;
        }
        _Money = 3 * _Money;
        return _Money;

    }
    public void CheckMerge()
    {
        for (int i = 0; i < ListSlotCast.Count; i++)
        {
            SlotControl temp = ListSlotCast[i];
            for (int j = i + 1; j < ListSlotCast.Count; j++)
            {
                if (temp.LevelSlot == ListSlotCast[j].LevelSlot)
                {
                    if (temp.LevelSlot >= 0) {

                        Destroy(ListSlotCast[j].DefaultPet.gameObject);
                        ListSlotCast[j].DefaultPet.ParentSlot.ClearSlot();
                        temp.MergeSlot();

                        return;
                    }
                }
            }
        }
    } public void ChangeSpeedMoney(int _speed)
    {
        speedMoney = _speed;
    }
    public void ChangeSpeedAddCat(int _speed)
    {
        speedAddCats = _speed;
    }
    public void AddEFNewCat(Vector3 _pos)
    {
        GameObject _g = GameObject.Instantiate(EFNewCat, null);
        _g.transform.position = _pos;
    }
    public void AddEFAddCat(Vector3 _pos)
    {
        GameObject _g = GameObject.Instantiate(EFAddCat, null);
        _g.transform.position = _pos;
    }
    public void AddEFMerge(Vector3 _pos)
    {
        GameObject _g = GameObject.Instantiate(EFMerge, null);
        _g.transform.position = _pos;
    }

}
