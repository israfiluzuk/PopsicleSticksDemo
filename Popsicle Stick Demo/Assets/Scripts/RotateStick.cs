using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStick : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           // transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + 45));
            transform.localRotation = Quaternion.EulerAngles(0,0,transform.localEulerAngles.z - 45);
        }
    }

}
