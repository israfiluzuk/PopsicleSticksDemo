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
    GameObject obj;
    
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

                            Debug.Log("PARENT : " + node.sticks[i].transform.parent.name);
                            //node.sticks[i].transform.parent = null;
                            //node.sticks[i].transform.SetParent("MoveableObject");
                            
                            

                        }
                        
                    }
                }
                else
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
                            Debug.Log("Node : "+nodes[i]);

                            //Debug.Log("Parent : "+ nodes[i].transform.parent);

                        }
                    }

                    rayhit.transform.position = nodes[smallestId].transform.position;

                    if(nodes[smallestId].transform.childCount > 0)
                    {
                        for (int i = 0; i < rayhit.transform.childCount; i++)
                        {
                            nodes[smallestId].transform.GetChild(0).GetComponent<Node>().sticks.Add(rayhit.transform.GetChild(i).GetComponent<Stick>());
                            rayhit.transform.GetChild(i).transform.SetParent(nodes[smallestId].transform.GetChild(0));
                        }
                        Destroy(rayhit.transform.gameObject);
                    }
                    else
                    { 
                        rayhit.transform.SetParent(nodes[smallestId].transform);
                    }



                }               
                
            }
            rayhit = new RaycastHit2D();

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


