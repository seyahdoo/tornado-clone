using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultGameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{

    public float carSpeed = 1f;

    public float tornadoEffectDistance;
    public float tornadoEffectPower;



}
