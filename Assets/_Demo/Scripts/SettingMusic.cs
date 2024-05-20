using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMusic : MonoBehaviour
{
    public GameObject PanelSetting;
    public Button btClose;
    public Button btShow;
    public Button btMusic;
    public Button btSounds;
    public Sprite imOn,imOff;
    void Start()
    {


        btShow.onClick.AddListener(ShowPanelOB);
        btClose.onClick.AddListener(ClosePanel);
        btMusic.onClick.AddListener(ClickMusic);
        btSounds.onClick.AddListener(ClickSound);
        CHeckStatus();

    }
    void CHeckStatus()
    {
        int isSound = PlayerPrefs.GetInt(KeySave.KeySound, 0);
        int isMusic = PlayerPrefs.GetInt(KeySave.KeyMusic, 0);
        if (isSound == 0)
        {
            btSounds.GetComponent<Image>().sprite = imOn;
        }
        else
        {
            btSounds.GetComponent<Image>().sprite = imOff;
        }
        if (isMusic == 0)
        {
            btMusic.GetComponent<Image>().sprite = imOn;
        }
        else
        {
            btMusic.GetComponent<Image>().sprite = imOff;
        }
    }
    void ClickMusic()
    {
        SoundControl.Instance.ChangeaSettingMusic();
        CHeckStatus();
        SoundControl.Instance.PlayClick();
    }
    void ClickSound()
    {
        SoundControl.Instance.ChangeSettingSound();
        CHeckStatus();
        SoundControl.Instance.PlayClick();
    }
    public void ClosePanel()
    {
        PanelSetting.SetActive(false);
        SoundControl.Instance.PlayClick();
    }
    public void ShowPanelOB()
    {
        SoundControl.Instance.PlayClick();
        PanelSetting.SetActive(true);
    }



}
