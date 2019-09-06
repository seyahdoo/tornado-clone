using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyable : MonoBehaviour
{

    public FlyableRuntimeSet runtimeSet;
    public Rigidbody rb;
    public Transform tr;

    protected bool isPhysicsDisabled = true;

    public void EnablePhysics()
    {
        rb.isKinematic = false;
        isPhysicsDisabled = false;
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
