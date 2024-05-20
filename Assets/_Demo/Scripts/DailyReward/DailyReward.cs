using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    public GameObject PanelDaily;
    public Text txtBonues;
    public Button btOK;
    public Button btShowDaily;
    int numday = 0;
    bool isGetGold = false;
    // Start is called before the first frame update
    void Start()
    {

        int numDay = PlayerPrefs.GetInt(KeySave.numDay, 0);
        btShowDaily.onClick.AddListener(ShowPanelReward);

        btOK.onClick.AddListener(GetReward);
        CheckReward();
    }
    public void ClosePanelReward()
    {
        PanelDaily.SetActive(false);
        SoundControl.Instance.PlayClick();
    }
    public void ShowPanelReward()
    {
        SoundControl.Instance.PlayClick();
        PanelDaily.SetActive(true);
    }


    void CheckReward()
    {
        int _time = System.DateTime.Now.Year * 10000 + System.DateTime.Now.Month * 100 + System.DateTime.Now.Day;
        int OldDay = PlayerPrefs.GetInt(KeySave.DayLogin, 0);

        if (_time > OldDay)
        {
            txtBonues.text = "You will get 500 gold !";

        }
        else
        {
            txtBonues.text = "You got your reward today!";
        }
    }
    public void GetReward()
    {
        int _time = System.DateTime.Now.Year*10000 +System.DateTime.Now.Month*100+System.DateTime.Now.Day;
        int OldDay = PlayerPrefs.GetInt(KeySave.DayLogin, 0);



        if (_time > OldDay)
        {
            //numDay += 1;
         //   GameControl.Instance.ChangeGold(500);
            PlayerPrefs.SetInt(KeySave.DayLogin, _time);
            PlayerPrefs.Save();
            CheckReward();

        }
        ClosePanelReward();
    }

}
