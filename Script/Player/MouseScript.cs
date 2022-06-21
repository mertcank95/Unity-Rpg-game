using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Texture2D cursorTextureEnemy;
    CursorMode mode = CursorMode.ForceSoftware;
    Vector2 hotSpot = Vector2.zero;
    public GameObject mousePoint;


    
    void Update()
    {
        CursorChange();
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    Vector3 lastPos = hit.point;
                    //lastPos.y = 0.10f;
                    Instantiate(mousePoint, lastPos, Quaternion.identity);
                }
            }
        }


    }


    void CursorChange()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Target"))
            {
                Cursor.SetCursor(cursorTextureEnemy, hotSpot, mode);
            }
            else
            {
                Cursor.SetCursor(cursorTexture, hotSpot, mode);

            }
        }
       



    }




}
