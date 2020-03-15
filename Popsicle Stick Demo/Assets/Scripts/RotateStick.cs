using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStick : MonoBehaviour
{

    RaycastHit hit;
    float clickTime;
    RaycastHit2D rayhit;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            clickTime = Time.time;
            rayhit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - clickTime < .2f) { 
                
                if (rayhit)
                {
                    Node node = rayhit.transform.GetComponent<Node>();
                    if (node != null)
                    {
                        for (int i = 0; i < node.sticks.Count; i++)
                        {
                            Vector3 newAngles = new Vector3(0, 0, (node.sticks[i].transform.localEulerAngles.z - 45));
                            newAngles.z = newAngles.z < 0 ? newAngles.z + 180 : newAngles.z;
                            node.sticks[i].transform.localEulerAngles = newAngles;
                            node.sticks[i].degree = Mathf.CeilToInt(newAngles.z);
                        }
                    }
                }
           }
        }
        else if (Input.GetMouseButton(0))
        {
            if (Time.time - clickTime >= 0.2f)
            {
                if (rayhit)
                {
                    Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    rayhit.transform.position = newPos;
                }
            }
        }
    }



}