using UnityEngine;

[CreateAssetMenu(fileName = "DefaultGameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{

    public float carSpeed = 1f;
    public float carGuideDistance = 1f;

    public float tornadoEffectDistance;
    public float tornadoEffectPower;

    public float tornadoControllSpeed = 400f;
    public float tornadoControllXSpeed = 1f;
    public float tornadoControllYSpeed = 5f;
    public float tornadoStaticForwardSpeed = 1f;

    public SceneReference[] levels;

}
