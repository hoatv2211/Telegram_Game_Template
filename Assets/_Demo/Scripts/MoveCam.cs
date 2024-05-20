using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
         [SerializeField]
    private Camera cam;
    private Vector3 dragOrigin;
    public Transform Left;
    public Transform Right;
    float MinX = 0;
    float MaxX = 0;
    float PosY=0;

    // Start is called before the first frame update
    void Start()
    {
        CheckLimit();
        LimitPos(cam.transform.position);

    }
    void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
    Vector3 tempPos =        cam.transform.position + difference;
            LimitPos(tempPos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PanCamera();
     //   transform.position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }
    void CheckLimit()
    {

        Vector3 check = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        float sizeCame =Mathf.Abs( (float) check.x - (float)cam.transform.position.x);
        Debug.Log("SizeCam:" + sizeCame);
        MinX = Left.position.x + sizeCame;
        MaxX = Right.position.x - sizeCame;
        PosY = cam.transform.position.y;
    }
    void LimitPos(Vector3 _pos)
    {
        if (_pos.x < MinX) {
            _pos.x = MinX;
        }
        if (_pos.x > MaxX) {
            _pos.x = MaxX;
        }
        _pos.y = PosY;
        cam.transform.position = _pos;
    }
    private void OnMouseUp()
    {
        //AdsManager.Instance.ShowFull();
    }
}
