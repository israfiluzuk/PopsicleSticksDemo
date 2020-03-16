using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
     
    float clickTime;
    RaycastHit2D rayhit;
    float smallestDistance = 1.5f;
    int smallestId = 1;
    public Transform[] nodes;
    public LayerMask selectableObjLayerMask;

   // private List<Transform> toMove = new List<Transform>();

    
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {

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
                            node.sticks[i].transform.localEulerAngles = newAngles;
                            node.sticks[i].degree = Mathf.CeilToInt(newAngles.z);

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


                            }
                        }

                        rayhit.transform.position = nodes[smallestId].transform.position;
                        if (rayhit.transform.parent != nodes[smallestId].transform)
                        { 
                            if (nodes[smallestId].transform.childCount > 0 && nodes[smallestId].transform != rayhit.transform.parent)
                            {
                                if (currNode != null)
                                {
                                    for (int i = 0; i < currNode.sticks.Count; i++)
                                    {
                                        nodes[smallestId].transform.GetChild(0).GetComponent<Node>().sticks.Add(currNode.sticks[i]);
                                        currNode.sticks[i].transform.SetParent(nodes[smallestId].transform.GetChild(0));
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
                   


                    //foreach (Transform child in rayhit.transform)
                    //{
                    //    nodes[smallestId].transform.GetChild(0).GetComponent<Node>().sticks.Add(child.GetComponent<Stick>());
                    //    toMove.Add(child);
                    //}
                    //Destroy(rayhit.transform.gameObject);
                    //foreach (var child in toMove)
                    //{
                    //    child.SetParent(nodes[smallestId].transform.GetChild(0));
                    //}

                    




                }               
                
            }
            rayhit = new RaycastHit2D();

        }
        else if (Input.GetMouseButton(0))
        {
            if(rayhit.transform != null)
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
}


