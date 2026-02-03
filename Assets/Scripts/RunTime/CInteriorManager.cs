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
    // 0 - 자리에 없
    // 1 - 자리에 있
    // 2 - 작동 불가능

    [Header("방 종류")]
    public ERoomType type;

    [Header("장비 위치")]
    public EEquipmentPosition equipmentPosition;

    [Header("시스템 타입 (서로 다른 스크립트에서 중복 검사를 위한 값)")]
    public CSystem.ESystemType systemType;
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
    [Header("Cloaking")]
    [SerializeField] private CInteriorPreset[] _cloakingData;
    [Header("Teleporter")]
    [SerializeField] private CInteriorPreset[] _teleporterData;
    #endregion

    #region 내부 변수
    private readonly Dictionary<CSystem.ESystemType, CInteriorPreset[]> _dataDic = new Dictionary<CSystem.ESystemType, CInteriorPreset[]>();
    #endregion

    public static CInteriorManager Instance { get; private set; }

    public Dictionary<CSystem.ESystemType, CInteriorPreset[]> DataDic { get { return _dataDic; } }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("중복 감지 → 기존 인스턴스가 있으므로 현재 오브젝트 제거");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _dataDic.Add(CSystem.ESystemType.Shields, _shieldsData);
        _dataDic.Add(CSystem.ESystemType.Engines, _enginesData);
        _dataDic.Add(CSystem.ESystemType.Oxygen, _oxygenData);
        _dataDic.Add(CSystem.ESystemType.Weapons, _weaponsData);
        _dataDic.Add(CSystem.ESystemType.Drones, _dronesData);
        _dataDic.Add(CSystem.ESystemType.Medbay, _medbayData);
        _dataDic.Add(CSystem.ESystemType.Pilot, _pilotData);
        _dataDic.Add(CSystem.ESystemType.Door, _doorData);
        _dataDic.Add(CSystem.ESystemType.Cloaking, _cloakingData);
        _dataDic.Add(CSystem.ESystemType.Teleporter, _teleporterData);
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
