using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicControl : MonoBehaviour
{
    public static MagicControl Instance;
    public float timeGift = 0f;
    public Text txtTime;
    public GameObject panelTime;
    public GameObject imLock;
    bool stt = true;
    float timeCheckMerge = 1f;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeGift > 0)
        {
            timeGift -= Time.deltaTime;
            timeCheckMerge -= Time.deltaTime;
       

            txtTime.text = "" + (int) timeGift;
            if (timeCheckMerge < 0)
            {
                Gamecontrol.Instance.CheckMerge();
                timeCheckMerge = 1f;
            }
        }
        else
        {
            if (stt)
            {
                Gamecontrol.Instance.ChangeSpeedMoney(1);
                panelTime.SetActive(false);
                imLock.SetActive(true);
                stt = false;
            }
        }
    }
    public void SetValues(float _time)
    {
        timeGift = _time;
        //Gamecontrol.Instance.ChangeSpeedMoney(2);
        panelTime.SetActive(true);imLock.SetActive(false);
        stt = true;
    }
}
