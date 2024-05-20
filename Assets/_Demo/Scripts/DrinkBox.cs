using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkBox : MonoBehaviour
{
    public SpriteRenderer imDrink;
    int idDrink =0;
    public List<Sprite> ListSpriteDrink = new List<Sprite>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

  public  void SetValuseBox(int _idBox)
    {
        idDrink = _idBox;
        if (ListSpriteDrink.Count > 0)
        {
            int temp = idDrink% ListSpriteDrink.Count;
            imDrink.sprite = ListSpriteDrink[temp];

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
