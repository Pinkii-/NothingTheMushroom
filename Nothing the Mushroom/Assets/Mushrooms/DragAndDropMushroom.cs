using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropMushroom : MonoBehaviour
{
    Vector3 offset;

    private void OnMouseDown()
    {
        Debug.Log("Down");
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mousePos;
        offset.z = 10;

        transform.parent.GetComponent<PotScript>().OnMouseDown();

        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnMouseDrag()
    {
        transform.position = offset + Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
