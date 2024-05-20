using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    float angle = 0f;     
    [Range(0,2)]
    public float MinScale;
    [Range(0, 2)]
    public float MaxScale;
    float tempScale;
    // Start is called before the first frame update
    void Start()
    {
        if (MaxScale < MinScale) {
            MinScale = 0;
            MaxScale = 1;
        }
        
    }

    // Update is called once per frame
    void Update()
    { angle += Time.deltaTime;
        tempScale =MinScale +Mathf.Abs( Mathf.Sin(angle)*(MaxScale -MinScale));
        this.transform.localScale = new Vector3(tempScale,tempScale,0);
    }
}
