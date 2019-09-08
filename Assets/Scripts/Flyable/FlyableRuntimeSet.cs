using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlyableRuntimeSet", menuName = "ScriptableObjects/FlyableRuntimeSetObject", order = 1)]
public class FlyableRuntimeSet : ScriptableObject
{
    public HashSet<Flyable> flyables = new HashSet<Flyable>();
}
