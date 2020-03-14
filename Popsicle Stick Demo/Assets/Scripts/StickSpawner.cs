using System.Collections.Generic;
using UnityEngine;


public class StickSpawner : MonoBehaviour
{
    
    public GameObject Stick;
    int[] degrees = { 0, 45, 90, 135 };
    int value;
    public Transform sticks;
    public Color[] stickColors;
    public Node[] nodes;
    public Node selectableNode;

    public List<int> degreeList = new List<int>();


    int maxNumbers = 4;

    void Start()
    {
        DegreeListAdder();
    }
    void DegreeListAdder()
    {
        degreeList.Add(0);
        degreeList.Add(45);
        degreeList.Add(90);
        degreeList.Add(135);
    }

    public void ReGenerate()
    { 

        selectableNode.sticks.Clear();
        for (int i = 0; i < sticks.childCount; i++)
        {
            Destroy(sticks.GetChild(i).gameObject);
        }
        degreeList.Clear();
        DegreeListAdder();


        for (int i = 1; i < Random.Range(1f, 4f); i++)
        {
            
           //Debug.Log("Rand : " + i);

            value = Random.Range(0,degreeList.Count);


            //Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            GameObject stickInit = Instantiate(Stick, transform.position, Quaternion.Euler(0, 0, (-1)*degreeList[value]), sticks);

            Debug.Log("__"+ degreeList[value]);
            
            
            int colorId = Random.Range(0, stickColors.Length);
            stickInit.GetComponent<Renderer>().material.color = stickColors[colorId];
            stickInit.GetComponent<Stick>().degree = degreeList[value];
            degreeList.RemoveAt(value);Debug.Log("UZUNLUK : " + degreeList.Count);
            stickInit.GetComponent<Stick>().color = stickColors[colorId];
            selectableNode.sticks.Add(stickInit.GetComponent<Stick>());


        }

       // degreeList = degreeList.OrderBy(tvz => System.Guid.NewGuid()).ToList();
    }

    public void MoveStick()
    {
        nodes[0].sticks[0] = selectableNode.sticks[0];
        selectableNode.sticks.RemoveAt(0);
    }

    }
     
