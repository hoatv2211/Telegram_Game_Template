using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    public static MapControl Instance;
    public Transform topPos;
    public Transform downPos;
    public Transform rightPos;
    public Transform leftPos;
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
        
    }
}
