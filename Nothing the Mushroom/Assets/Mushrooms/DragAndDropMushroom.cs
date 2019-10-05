using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropMushroom : MonoBehaviour
{
    Vector3 offset;
    
    public void OnMouseDrag()
    {
        transform.position = offset + Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void StartDrag()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mousePos;
        offset.z = 10;
    }
}
