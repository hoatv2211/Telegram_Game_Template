using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPanel : MonoBehaviour
{
    public GameObject PanelDaily;
    public Button btClose;
    public Button btShow;
    void Start()
    {


        btShow.onClick.AddListener(ShowPanelOB);
        btClose.onClick.AddListener(ClosePanel);

    }
    public void ClosePanel()
    {
        PanelDaily.SetActive(false);
        SoundControl.Instance.PlayClick();
    }
    public void ShowPanelOB()
    {
        SoundControl.Instance.PlayClick();
        PanelDaily.SetActive(true);
    }



}
