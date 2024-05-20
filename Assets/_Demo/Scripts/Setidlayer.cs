using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setidlayer : MonoBehaviour
{
    public MeshRenderer Mesh;
    public int orderinlayer;
    // Start is called before the first frame update
    void Start()
    {
        Mesh.sortingOrder = orderinlayer;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
