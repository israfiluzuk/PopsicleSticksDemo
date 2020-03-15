﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStick : MonoBehaviour
{
    
    Stick SticksEl;
    Node nodeObj;
    RaycastHit hit;
    float clickTime;
    RaycastHit2D rayhit;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + 45));
            //transform.localRotation = Quaternion.EulerAngles(0,0,transform.localEulerAngles.z - 45);
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

    /*
     * float enkucuk = 999999;
     * int enkucukId = 0;
     * for{
     * MouseUp olduğunda -> Vector2.Distance(rayhit.transfrom.position,nodes[i].transform.position) < enkucuk { enkuguck = [distyance] , enkucukId = i } 
     * }
     *  raycast.transform.position = nodes[enkucukId].transfrom.position;
     * 
     * */


}