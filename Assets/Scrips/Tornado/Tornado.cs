using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public Transform tr;

    public GameSettings gameSettings;
    public FlyableRuntimeSet flyableRuntimeSet;

    void FixedUpdate()
    {
        foreach (Flyable flyable in flyableRuntimeSet.flyables)
        {
            flyable.rb.AddForce
                (
                    (flyable.tr.position - tr.position).normalized
                    * (Mathf.Clamp(gameSettings.tornadoEffectDistance / Vector3.Distance(flyable.tr.position, tr.position), 1f, float.MaxValue) - 1f)
                    * gameSettings.tornadoEffectPower
                    , ForceMode.Force
                );

        }
    }
}
