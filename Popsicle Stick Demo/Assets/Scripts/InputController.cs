using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{

    float clickTime;
    RaycastHit2D rayhit;
    float smallestDistance = 1.5f;
    int smallestId = 1;
    public Transform[] nodes;
    public LayerMask selectableObjLayerMask;
        
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DragControl();
            clickTime = Time.time;
            rayhit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, selectableObjLayerMask);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (rayhit)
            {
                if (Time.time - clickTime < .2f)
                {
                    Node node = rayhit.transform.GetComponent<Node>();
                    if (node != null)
                    {
                        for (int i = 0; i < node.sticks.Count; i++)
                        {
                            Vector3 newAngles = new Vector3(0, 0, (node.sticks[i].transform.localEulerAngles.z - 45));
                            newAngles.z = newAngles.z < 0 ? newAngles.z + 180 : newAngles.z;
                            newAngles.z = newAngles.z >180 ? newAngles.z - 180 : newAngles.z;
                            node.sticks[i].transform.localEulerAngles = newAngles;
                            node.sticks[i].degree = Mathf.CeilToInt(newAngles.z);
                            if (node.sticks[i].degree==1)
                            {
                                node.sticks[i].degree = 0;
                            }
                            else if (node.sticks[i].degree==180)
                            {
                                node.sticks[i].degree = 0;
                            }

                        }
                        
                    }
                }

                else 
                {
                    Node currNode = rayhit.transform.GetComponent<Node>();
                    if(currNode.isMoved == false)
                    {
                        smallestId = 0;
                        smallestDistance = 999;
                        for (int i = 0; i < nodes.Length; i++)
                        {
                            
                            float distance = Vector2.Distance(rayhit.transform.position, nodes[i].transform.position);
                            if (smallestDistance > distance)
                            {
                                smallestDistance = distance;
                                smallestId = i;
                                //Debug.Log(currNode.sticks[i].degree);
                            }

                        }
                        bool IsEmpty = true;
                        if (nodes[smallestId].transform.childCount > 0)
                        {

                            for (int i = 0; i < nodes[smallestId].transform.GetChild(0).childCount; i++)
                            {
                                for (int j = 0; j < currNode.sticks.Count; j++)
                                { 
                                    if (nodes[smallestId].transform.GetChild(0).GetChild(i).GetComponent<Stick>().degree == currNode.sticks[j].degree)
                                    {
                                        IsEmpty = false;
                                    }
                                }
                            }
                        }

                        if (IsEmpty)
                        {
                            rayhit.transform.position = nodes[smallestId].transform.position;
                            if (rayhit.transform.parent != nodes[smallestId].transform)
                            {
                                if (nodes[smallestId].transform.childCount > 0 && nodes[smallestId].transform != rayhit.transform.parent)
                                {
                                    if (currNode != null)
                                    {
                                        Debug.Log(nodes[smallestId].transform.gameObject.name);
                                        for (int i = 0; i < currNode.sticks.Count; i++)
                                        {

                                            nodes[smallestId].transform.GetChild(0).GetComponent<Node>().sticks.Add(currNode.sticks[i]);
                                            currNode.sticks[i].transform.SetParent(nodes[smallestId].transform.GetChild(0));
                                            Debug.Log(currNode.sticks[i].degree);
                                        }
                                        Destroy(rayhit.transform.gameObject);
                                    }
                                }
                                else
                                {
                                    if (currNode != null)
                                    {
                                        currNode.isMoved = true;
                                    }
                                    rayhit.transform.SetParent(nodes[smallestId].transform);
                                }
                            }
                        }
                        else
                        {
                            rayhit.transform.position = nodes[3].transform.position;
                        }


                    }
                    

                }               
                
            }
            rayhit = new RaycastHit2D();
            StickControl();
            DragControl();
        }
        else if (Input.GetMouseButton(0))
        {

            if (rayhit.transform != null)
            {
                Node currNode = rayhit.transform.GetComponent<Node>();
                if(currNode != null)
                    if (currNode.isMoved == false)
                    {
                        if (Time.time - clickTime >= 0.2f)
                        {
                            Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            rayhit.transform.position = newPos;
                        }
                    }
            }
        }
    }

    public bool StickControl()
    {

        bool areSameSticks = true;
        Color[] horizontalSticsColor = new Color[3];
        int selectedStickCount = 0; 
        for (int i = 0; i < 3; i++)
        {
            if(nodes[i].childCount > 0)
            {
                Node currNode = nodes[i].GetChild(0).GetComponent<Node>();
                if (currNode != null)
                {
                    for (int j = 0; j < currNode.sticks.Count; j++)
                    {
                        if (currNode.sticks[j].degree == 90)
                        { 
                            horizontalSticsColor[selectedStickCount] = currNode.sticks[j].color;
                            selectedStickCount++;
                        }
                    }
                }
            } 
        }
        if(selectedStickCount == 3)
        {
            for (int i = 1; i < 3; i++)
            {
                if(horizontalSticsColor[0] != horizontalSticsColor[i])
                {
                    areSameSticks = false;
                }
            }
        }
        else
        {
            areSameSticks = false;
        }

        if(areSameSticks)
        { 
            ScoreScript.scoreValue += 10;
            for (int j =0; j< 3; j++)
            {
                Node rStick = nodes[j].GetChild(0).GetComponent<Node>();
                for (int i = 0; i< rStick.sticks.Count; i++)
                {
                    if (rStick.sticks[i].degree == 90)
                    {
                        GameObject removedObj = rStick.sticks[i].gameObject;
                        rStick.sticks.RemoveAt(i);
                        Destroy(removedObj);
                    }
                }
                
            }

        }

        return areSameSticks;
    }
    void DragControl()
    {
        Node moveSticks = new Node();

        bool isdraggable = true;
        int[] directions= new int[4];
        int selectedStickCount = 0; 
        for (int i = 0; i < 3; i++)
        {
            if(rayhit.transform!=null)
            {
                Node currNode = rayhit.transform.GetComponent<Node>();
                Debug.Log("****"+moveSticks.sticks[i].degree+ "****");
                if (currNode != null)
                {
                    Debug.Log("Current : "+currNode.sticks[i].degree);
                    for (int j = 0; j < currNode.sticks.Count; j++)
                    {
                        directions[i] = currNode.sticks[i].degree;
                        Debug.Log(directions[i]);
                    }
                }
            } 
        }
        /*if(selectedStickCount == 3)
        {
            for (int i = 1; i < 3; i++)
            {
                if(directions[0] != directions[i])
                {
                    isdraggable = false;
                }
            }
        }
        else
        {
            isdraggable = false;
        }

        if(isdraggable)
        { 
            ScoreScript.scoreValue += 10;
            for (int j =0; j< 3; j++)
            {
                Node rStick = nodes[j].GetChild(0).GetComponent<Node>();
                for (int i = 0; i< rStick.sticks.Count; i++)
                {
                    if (rStick.sticks[i].degree == 90)
                    {
                        GameObject removedObj = rStick.sticks[i].gameObject;
                        rStick.sticks.RemoveAt(i);
                        Destroy(removedObj);
                    }
                }
                
            }

        }*/

        
    }

}


