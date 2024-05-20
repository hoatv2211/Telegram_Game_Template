using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float timeDelay= 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, timeDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
