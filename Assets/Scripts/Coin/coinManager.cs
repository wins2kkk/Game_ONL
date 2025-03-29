using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinManager : MonoBehaviour
{
    public float ratationSpeed = 50f;
    private void Update()
    {
        transform.Rotate(Vector3.up * ratationSpeed * Time.deltaTime);
    }

}
