using UnityEngine;


[System.Serializable]
public class CInteriorPreset
{
    [Header("메인 이미지")]
    public Sprite mainSprite;

    [Header("글로우 이미지")]
    public bool isUseGlow = true;
    public Sprite[] glowSprites;
    // 0 - 자리에 없
    // 1 - 자리에 있
    // 2 - 작동 불가능

    [Header("사이즈 (방처럼 2, 4만 존재)")]
    public int _size;

    [Header("장비 위치")]
    public EEquipmentPosition equipmentPosition;

    [Header("시스템 타입 (서로 다른 스크립트에서 중복 검사를 위한 값)")]
    public CSystem.ESystemType systemType;
}

// fileName : 파일 생성시 기본 이름
// menuName Creata 에 새로운 경로를 만들고, 생성이 가능해진다.
[CreateAssetMenu(fileName = "Interior", menuName = "ScriptableObject/InteriorData")]
public class CInteriorDataAsset : ScriptableObject
{
    public CInteriorPreset preset;
}

public class CInteriorDatabase : MonoBehaviour
{
    [Header("미리 다 올리기")]
    [SerializeField] private bool _preLoad = true;
}
