using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    private static SoundControl instance;
    public static SoundControl Instance {
        get {
            return instance;
        }
    }
   // [HideInInspector]
    public AudioSource AudSound;
    public AudioSource AudMusic;
    public AudioClip Aud_Click;
    public AudioClip Aud_AddCat;
    public AudioClip Aud_Merge;
    public AudioClip Aud_Coin;
    public AudioClip Aud_BGR;

    

     public int isSound = 0;
    public int isMusic = 0;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       
        isSound = PlayerPrefs.GetInt(KeySave.KeySound,0);
        isMusic = PlayerPrefs.GetInt(KeySave.KeyMusic, 0);
        if (isMusic == 0)
        {
            AudMusic.Play();
        }

    }
    void CheckStatus()
    {
        
    }

    public void PlayClick()
    {
        if (isSound == 0)
        {
            AudSound.PlayOneShot(Aud_Click);
        }
    }
    public void PlayAddCat()
    {
        if (isSound == 0)
        {
            AudSound.PlayOneShot(Aud_AddCat);
        }
    }
         public void PlayMerge()
    {
        if (isSound == 0)
        {
            AudSound.PlayOneShot(Aud_Merge);
        }
    }
    public void PlayCoin()
    {
        if (isSound == 0)
        {
            AudSound.PlayOneShot(Aud_Coin);
        }
    }

    public void ChangeSettingSound()
    {
        if (isSound == 0)
        {
            isSound = 1;
            PlayerPrefs.SetInt(KeySave.KeySound, 1);
            PlayerPrefs.Save();
        }
        else
            {
            isSound = 0;
            PlayerPrefs.SetInt(KeySave.KeySound, 0);
            PlayerPrefs.Save();
        }
    }
    public void ChangeaSettingMusic()
    {
        if (isMusic == 0)
        {
            isMusic = 1;
            AudMusic.Stop();
            PlayerPrefs.SetInt(KeySave.KeyMusic, 1);

            PlayerPrefs.Save();
        }
        else
        {
            isMusic = 0;
            AudMusic.Play();
            PlayerPrefs.SetInt(KeySave.KeyMusic, 0);
            PlayerPrefs.Save();
        }
    }
    public void PlayOther(AudioClip _other)
    {
        if (isSound == 0)
        {
            AudSound.PlayOneShot(_other);
        }
    }
    public void PlayBGRMusic()
    {
        if (isSound == 0)
        {
            AudSound.PlayOneShot(Aud_BGR);
        }
    }


}
