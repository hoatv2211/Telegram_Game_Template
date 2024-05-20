using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoublleMoney : MonoBehaviour
{
    public static DoublleMoney Instance;
    public float timeGift = 0f;
    public Text txtTime;
    public GameObject panelTime;
    public GameObject imLock;
    bool stt = true;
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
            int temp = (int)timeGift;
            txtTime.text = "" + temp;
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
        Gamecontrol.Instance.ChangeSpeedMoney(2);
        panelTime.SetActive(true);imLock.SetActive(false);
        stt = true;
    }
}
