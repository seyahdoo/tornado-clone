using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{

    public FlyableRuntimeSet flyableRuntimeSet;
    public Transform tr;

    public float tornadoEffectDistance;
    public float tornadoEffectPower;

    void FixedUpdate()
    {
        foreach (Flyable flyable in flyableRuntimeSet.flyables)
        {
            flyable.rb.AddForce
                (
                    (flyable.tr.position - tr.position).normalized
                    * (Mathf.Clamp(tornadoEffectDistance / Vector3.Distance(flyable.tr.position, tr.position), 1f, float.MaxValue) - 1f)
                    * tornadoEffectPower
                    , ForceMode.Force
                );

        }
    }
}
