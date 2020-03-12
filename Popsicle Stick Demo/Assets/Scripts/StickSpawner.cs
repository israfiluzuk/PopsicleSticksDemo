using System.Collections;
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
    void Start()
    {

    }

    public void ReGenerate()
    {

        for (int i = 0; i < sticks.childCount; i++)
        {
            Destroy(sticks.GetChild(i).gameObject);
        }
        selectableNode.sticks.Clear();

        for (int i = 1; i < Random.Range(1f, 4f); i++)
        {
            Debug.Log("Rand : " + i);
           
            value = (int)Random.Range(0f, 4f);
            Debug.Log(degrees[value]);
            //Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            GameObject stickInit = Instantiate(Stick, transform.position, Quaternion.Euler(0, 0, degrees[value]), sticks);
            int colorId = Random.Range(0, stickColors.Length);
            stickInit.GetComponent<Renderer>().material.color = stickColors[colorId];
            stickInit.GetComponent<Stick>().degree = degrees[value];
            stickInit.GetComponent<Stick>().color = stickColors[colorId];
            selectableNode.sticks.Add(stickInit.GetComponent<Stick>());
        }
    }

    public void MoveStick()
    {
        nodes[0].sticks[0] = selectableNode.sticks[0];
        selectableNode.sticks.RemoveAt(0);
    }

    }
     
