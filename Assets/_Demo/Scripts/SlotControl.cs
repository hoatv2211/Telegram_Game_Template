using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum statusSlot { Lock,Null,Full}

public class SlotControl : MonoBehaviour
{
    public statusSlot _status;
    public int LevelSlot;
    public CatControl DefaultPet;
    public GameObject PrefabCat;

    // Start is called before the first frame update
    void Start()
    {
        CheckSlot();
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetPet(CatControl _df)
    {
        DefaultPet = _df;
    }

    public void CheckGetIn(CatControl _Cat)
    {
        if (_Cat.ParentSlot != this)
        {
            if (_status == statusSlot.Lock)
            {
                return;
            }
            else
            {
                if (_status == statusSlot.Null)
                {
                    _Cat.ParentSlot.ClearSlot();
                    this.LevelSlot = _Cat.CatLevel;
                    this.DefaultPet = _Cat;
                    this._status = statusSlot.Full;
                    _Cat.ParentSlot = this;
                    _Cat.ResetPosition();
                }
                else
                {
                    if (_status == statusSlot.Full)
                    {
                        if (_Cat.ParentSlot.LevelSlot == this.LevelSlot)
                        {
                            _Cat.ParentSlot.ClearSlot();
                            Destroy(_Cat.gameObject);
                            MergeSlot();
                        }
                        else
                        {
                            ChangeCat(_Cat);
                        }

                    }
                }
            }
        }
        else
        {
            _Cat.ResetPosition();
        }
    }
    void ChangeCat(CatControl _Cat)
    {
       // CatControl tempCat = _Cat;
        SlotControl tempSlot = _Cat.ParentSlot;
        tempSlot.DefaultPet = DefaultPet;

        DefaultPet = _Cat;
        this.LevelSlot = DefaultPet.CatLevel;
        tempSlot.LevelSlot = tempSlot.DefaultPet.CatLevel;
       
        tempSlot.ResetCat();
        this.ResetCat();


    }
    void ResetCat()
    {
        if (DefaultPet != null)
        {
            DefaultPet.SetValues(this);
        }
    }
    void CheckSlot()
    {
        if (LevelSlot < 0)
        {
            LevelSlot = -1;
        }
        else
        {
            AddCats();
        }
    }
    public void ClearSlot()
    {
        LevelSlot = -1;
        _status = statusSlot.Null;
        DefaultPet = null;
    }
    public void MergeSlot()
    {
        Debug.Log("Merge Cat");
        Destroy(DefaultPet.gameObject);
        LevelSlot += 1;
        _status = statusSlot.Null;
        DefaultPet = null;
        AddCats();
        Gamecontrol.Instance.SaveInforCat();
        Gamecontrol.Instance.CheckNewCat(LevelSlot);
        Gamecontrol.Instance.AddEFMerge(this.transform.position);
        SoundControl.Instance.PlayMerge();
    }

    public void BuyCats(int Lv)
    {
        if (_status == statusSlot.Null)
        {
            if (Lv < 0)
            {
                Lv = 0;
            }
            LevelSlot = Lv;

            GameObject Cat = GameObject.Instantiate(PrefabCat, null);
            Cat.name = "Cat Level" + LevelSlot;
            Cat.transform.parent = this.transform;
            Vector3 POs = transform.position;
            POs.z = -9;
            Cat.transform.position = POs;
            CatControl scr = Cat.GetComponent<CatControl>();
            scr.SetValues(this);
            _status = statusSlot.Full;
            DefaultPet = scr;
            Gamecontrol.Instance.AddEFAddCat(this.transform.position);
            SoundControl.Instance.PlayAddCat();
        }
    }

    public  void AddCats()
    {
        if (_status == statusSlot.Null)
        {
            if (LevelSlot < 0)
            {
                LevelSlot = 0;
            }
           
            GameObject Cat = GameObject.Instantiate(PrefabCat, null);
            Cat.name = "Cat Level" + LevelSlot;
            Cat.transform.parent = this.transform;
            Vector3 POs = transform.position;
            POs.z = -9;
            Cat.transform.position = POs;
            CatControl scr = Cat.GetComponent<CatControl>();
            scr.SetValues(this);
            _status = statusSlot.Full;
            DefaultPet = scr;
            Gamecontrol.Instance.AddEFAddCat(this.transform.position);
            SoundControl.Instance.PlayAddCat();
        }
    }

}
[System.Serializable]
public class InforSlot {
    public int id;
    public int Level;
}
[System.Serializable]
public class InforShop
{
    public int id;
    public int numBuy;
}
