using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxReward : MonoBehaviour
{
    public Text txtTime;
    float TimeReward =300.4f;
    public Button btGetGold;
    // Start is called before the first frame update
    void Start()
    {
        btGetGold.onClick.AddListener(ClickGetGold);
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeReward > 0)
        {
            TimeReward -= Time.deltaTime;
            string vl = "" + ((int)(TimeReward / 60)).ToString("00") + ":" + ((int)(TimeReward % 60)).ToString("00"); ;
            txtTime.text = vl;
        }
        else
        {
            TimeReward = 0;
            txtTime.text = "Get Gold";
        }
    }
    void ClickGetGold()
    {
        if (TimeReward <= 0)
        {
           // GameControl.Instance.ChangeGold(100);
            TimeReward = 300.4f;
        }
        SoundControl.Instance.PlayClick();
    }
}
