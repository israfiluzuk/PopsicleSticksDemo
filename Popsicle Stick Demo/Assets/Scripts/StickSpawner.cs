using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StickSpawner : MonoBehaviour
{
    
    public GameObject Stick;
    //int[] degrees = { 0, 45, 90, 135 };
    int value;
    public Transform sticks;
    public Color[] stickColors;
    public Node[] nodes;
    public Transform generatedNodeParent;

    public List<int> degreeList = new List<int>();

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
        generatedNode.transform.position = new Vector2(0, -3.34f);
        Node selectableNode = generatedNode.AddComponent<Node>(); 

        generatedNode.AddComponent<BoxCollider2D>();
        generatedNode.layer = 8;
        generatedNode.transform.SetParent(generatedNodeParent);
       // selectableNode.sticks.Clear();

        degreeList.Clear();
        DegreeListAdder();
      //  print(selectableNode.name);

        for (int i = 1; i < Random.Range(1f, 4f); i++)
        {
           //Debug.Log("Rand : " + i);

            value = Random.Range(0,degreeList.Count);

            GameObject stickInit = Instantiate(Stick, transform.position, Quaternion.Euler(0, 0, (-1)*degreeList[value]), generatedNode.transform);

            //Debug.Log("__"+ degreeList[value]);
            
            int colorId = Random.Range(0, stickColors.Length);
            //---

            stickInit.GetComponent<Renderer>().material.color = stickColors[colorId];
            stickInit.GetComponent<Stick>().degree = degreeList[value];
            //remove value at list
            degreeList.RemoveAt(value);


            stickInit.GetComponent<Stick>().color = stickColors[colorId];
            selectableNode.sticks.Add(stickInit.GetComponent<Stick>()); 
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

}
     
