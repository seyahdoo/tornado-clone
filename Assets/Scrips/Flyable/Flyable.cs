using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyable : MonoBehaviour
{

    public Transform tr;
    public Rigidbody rb;
    public FlyableRuntimeSet runtimeSet;

    protected bool isKinematic = true;

    public void EnablePhysics()
    {
        rb.isKinematic = false;
        isKinematic = false;
    }

    private void OnEnable()
    {
        runtimeSet.flyables.Add(this);
    }

    private void OnDisable()
    {
        runtimeSet.flyables.Remove(this);
    }

}
