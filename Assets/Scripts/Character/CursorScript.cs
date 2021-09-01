using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public float centeringNumber;
    // Start is called before the first frame update
    void Start()
    {
        TurnOnGameCursor();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (this.gameObject.activeInHierarchy)
        {
            Vector3 mousePos = Input.mousePosition - new Vector3(centeringNumber, centeringNumber);
            this.gameObject.transform.position = mousePos;
        }
    }
    public void TurnOnGameCursor()
    {
        Cursor.visible = false;
        this.gameObject.SetActive(true);
    }
    public void TurnOffGameCursor()
    {
        Cursor.visible = true;
        this.gameObject.SetActive(false);
    }
}
