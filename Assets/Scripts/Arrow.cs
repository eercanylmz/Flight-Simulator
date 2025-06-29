using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform Target;
    private void Update()
    {
        transform.LookAt(Target);
    }
}
