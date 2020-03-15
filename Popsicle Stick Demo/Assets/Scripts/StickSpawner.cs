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

    public List<int> degreeList = new List<int>();


    int maxNumbers = 4;
    int generatedNodeCount = 0;

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
        generatedNodeCount++;
        GameObject generatedNode = new GameObject("MoveableObject");
        generatedNode.transform.position = new Vector2(0, -3.25f);
        Node selectableNode =  generatedNode.AddComponent<Node>(); 
        generatedNode.AddComponent<BoxCollider2D>();
        generatedNode.layer = 8;
       // selectableNode.sticks.Clear();

        degreeList.Clear();
        DegreeListAdder();
      //  print(selectableNode.name);

        for (int i = 1; i < Random.Range(1f, 4f); i++)
        {
           //Debug.Log("Rand : " + i);

            value = Random.Range(0,degreeList.Count);

            GameObject stickInit = Instantiate(Stick, transform.position, Quaternion.Euler(0, 0, (-1)*degreeList[value]), generatedNode.transform);

            Debug.Log("__"+ degreeList[value]);
            
            int colorId = Random.Range(0, stickColors.Length);
            //---

            stickInit.GetComponent<Renderer>().material.color = stickColors[colorId];
            stickInit.GetComponent<Stick>().degree = degreeList[value];
            //remove value at list
            degreeList.RemoveAt(value);Debug.Log("UZUNLUK : " + degreeList.Count);

            stickInit.GetComponent<Stick>().color = stickColors[colorId];
            selectableNode.sticks.Add(stickInit.GetComponent<Stick>()); 
        }

        

       // degreeList = degreeList.OrderBy(tvz => System.Guid.NewGuid()).ToList();
    }
     

    }
     
