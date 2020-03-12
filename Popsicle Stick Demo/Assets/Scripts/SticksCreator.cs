using UnityEngine;

public class SticksCreator : MonoBehaviour
{
    public GameObject[] Sticks;
    public Color[] SticksColors = new Color[7];
    int[] StickDegrees = new int[4]{0,45,90,135};
    bool isNodeEmpty;
    //int randStickNumber = Random.Range(1,5);
    
    // Start is called before the first frame update
    void Start()
    {
        ColorSetter();
    }

    void ColorSetter()
    {
        GetComponent<Renderer>().material.color = SticksColors[Random.Range(0, SticksColors.Length)];

    }
}
