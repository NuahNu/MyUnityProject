using UnityEngine;


[System.Serializable]
public class CInteriorPreset
{ 

}

[CreateAssetMenu]
public class CInteriorDataAsset : ScriptableObject
{
    public CInteriorPreset preset;
}

public class CInteriorDatabase : MonoBehaviour
{
    [Header("미리 다 올리기")]
    [SerializeField] private bool _preLoad = true;
}
