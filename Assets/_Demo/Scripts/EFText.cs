using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EFText : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro txtGold;
    void Start()
    {
       // SetValues(99);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetValues(int _Values)
    {
        if (_Values > 0)
        {
            txtGold.text = "+" + _Values;
        }
        else
        {
            txtGold.text = "" + _Values;
        }
    }
}
