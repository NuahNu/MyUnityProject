using System.Collections.Generic;
using UnityEngine;


#region CInteriorManager
/*

*/
#endregion

[System.Serializable]
public class CInteriorPreset
{
    [Header("메인 이미지")]
    public Sprite mainSprite;

    [Header("글로우 이미지")]
    public bool isUseGlow = true;
    public Sprite[] glowSprites;

    [Header("방 종류")] // 여기서 필요한가?
    public ERoomType type;

    [Header("장비 위치")]
    public EEquipmentPosition equipmentPosition;

    [Header("시스템 타입 (서로 다른 스크립트에서 중복 검사를 위한 값)")]
    public CSystem.ESystemType systemType;
}

[System.Serializable]
public class CIconPreset
{
    [Header("오버레이 이미지")]
    public Sprite overlaySprite;

    [Header("UI 이미지 (블루, 그린, 오렌지, 레드 (1,2) 순서로 넣을 것.)")]
    public Sprite[] uiSprites;
}

public class CInteriorManager : MonoBehaviour
{
    #region 인스펙터
    [Header("Shields")]
    [SerializeField] private CInteriorPreset[] _shieldsData;
    [Header("Engines")]
    [SerializeField] private CInteriorPreset[] _enginesData;
    [Header("Oxygen")]
    [SerializeField] private CInteriorPreset[] _oxygenData;
    [Header("Weapons")]
    [SerializeField] private CInteriorPreset[] _weaponsData;
    [Header("Drones")]
    [SerializeField] private CInteriorPreset[] _dronesData;
    [Header("Medbay")]
    [SerializeField] private CInteriorPreset[] _medbayData;
    [Header("Pilot")]
    [SerializeField] private CInteriorPreset[] _pilotData;
    [Header("Door")]
    [SerializeField] private CInteriorPreset[] _doorData;
    [Header("Sensors")]
    [SerializeField] private CInteriorPreset[] _sensorsData;
    [Header("Cloaking")]
    [SerializeField] private CInteriorPreset[] _cloakingData;
    [Header("Teleporter")]
    [SerializeField] private CInteriorPreset[] _teleporterData;

    [Header("아이콘 정보 (CSystem.ESystemType 의 순서로 작성할것.")]
    [SerializeField] private CIconPreset[] _iconPresets;
    #endregion

    #region 내부 변수
    private readonly Dictionary<CSystem.ESystemType, CInteriorPreset[]> _interiorDataDic = new Dictionary<CSystem.ESystemType, CInteriorPreset[]>();

    private readonly Dictionary<CSystem.ESystemType, CIconPreset> _iconDic = new();
    #endregion

    public static CInteriorManager Instance { get; private set; }

    public Dictionary<CSystem.ESystemType, CInteriorPreset[]> DataDic { get { return _interiorDataDic; } }
    public Dictionary<CSystem.ESystemType, CIconPreset> IconDic { get { return _iconDic; } }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("중복 감지 → 기존 인스턴스가 있으므로 현재 오브젝트 제거");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _interiorDataDic.Add(CSystem.ESystemType.Shields, _shieldsData);
        _interiorDataDic.Add(CSystem.ESystemType.Engines, _enginesData);
        _interiorDataDic.Add(CSystem.ESystemType.Oxygen, _oxygenData);
        _interiorDataDic.Add(CSystem.ESystemType.Weapons, _weaponsData);
        _interiorDataDic.Add(CSystem.ESystemType.Drones, _dronesData);
        _interiorDataDic.Add(CSystem.ESystemType.Medbay, _medbayData);
        _interiorDataDic.Add(CSystem.ESystemType.Pilot, _pilotData);
        _interiorDataDic.Add(CSystem.ESystemType.Door, _doorData);
        _interiorDataDic.Add(CSystem.ESystemType.Sensors, _sensorsData);
        _interiorDataDic.Add(CSystem.ESystemType.Cloaking, _cloakingData);
        _interiorDataDic.Add(CSystem.ESystemType.Teleporter, _teleporterData);

        for (int i = 0; i < _iconPresets.Length; i++)
        {
            _iconDic.Add((CSystem.ESystemType)i, _iconPresets[i]);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
    void OnDestroy()
    {
        if (Instance == this)
        {
            Debug.LogWarning("CInteriorManager.Instance 삭제.");
            Instance = null;
        }
    }
}
