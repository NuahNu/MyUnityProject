using System;
using System.Collections.Generic;
using UnityEngine;


#region CButtonUI
/*

*/
#endregion

public class CSystemUI : MonoBehaviour
{
    #region 인스펙터
    [Header("연결된 시스템")]
    //[ReadOnly]
    [SerializeField] private CSystem _system;


    [Header("RawImagePregab")]
    [SerializeField] private GameObject _RawImagePrefab;

    [Header("바 Texture")]
    [SerializeField] private Texture _lineTexture;
    [SerializeField] private Texture _barTexture;
    [SerializeField] private Texture _barGlowTexture;

    [Header("바 정보")]
    [SerializeField] private List<CRawImageUI> _barUIList;
    [SerializeField] private Vector2 _barOffset = Vector2.zero;
    [SerializeField] private Vector2 _barInterval = Vector2.zero;

    [Header("아이콘 texture")]
    [ReadOnly]
    [SerializeField] private Texture _iconTexture;
    [ReadOnly]
    [SerializeField] private Texture _iconGlowTexture;

    [Header("아이콘 정보")]
    [SerializeField] private CRawImageUI _iconObject;
    [SerializeField] private Vector2 _iconOffset = Vector2.zero;

    [Header("캔버스")]
    [SerializeField] private Transform _canvas;

    [Header("색(100, 255, 100)")]
    [SerializeField] private Color _green;
    #endregion

    #region 내부 변수
    // 그릴 아이콘
    // 대충 시스템의 정보들 긁어와서 그려야 한다.how?
    private int _barCount = 0;

    private ESystemState _currentState = ESystemState.PowerOff;
    #endregion

    public CSystem System { get { return _system; } set { _system = value; _system.SystemUI = this; } }

    void Awake()
    {

    }

    void Start()
    {
        if (_system == null)
        {
            Debug.LogWarning($"{gameObject.name} _system == null");
        }
    }

    void Update()
    {
        if (_system == null)
            return;

        // 임시 코드.
        if (_system.SystemUI == null)
        {
            Debug.Log($"{gameObject.name}과 {_system.gameObject.name} 임시연결.");
            _system.SystemUI = this;
        }

        // 가능한 전부 여기서 빼버린다.
        UpdateBar();
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (_iconObject == null)
        {
            _iconTexture = _system.IconPreset.uiSprites[(int)CIconPreset.ESpriteID.Grey].texture;
            _iconGlowTexture = _system.IconPreset.uiSprites[(int)CIconPreset.ESpriteID.GreyGlow].texture;


            GameObject go = Instantiate(_RawImagePrefab, this.transform);
            if (go.TryGetComponent(out CRawImageUI rawImageUI))
            {
                _iconObject = rawImageUI;
                // 위치 조정 필수.
                _iconObject.transform.localPosition = _iconOffset;

                // 이미지 바꾸기. 기본이 bar 이다.
                _iconObject.MainRawImage.texture = _iconTexture;
                _iconObject.GlowRawImage.texture = _iconGlowTexture;

                _iconObject.MainRawImage.SetNativeSize();
                _iconObject.GlowRawImage.SetNativeSize();
            }
        }

        if (_iconObject == null)
        {
            Debug.LogWarning($"{gameObject.name} _iconObject == null");
            return;
        }
    }

    private void UpdateBar()
    {
        if (_system.CurrentPower != _barCount)
        {
            _barCount = _system.CurrentPower;
        }


        while (_system.MaxPower != _barUIList.Count)
        {
            AddBar();
        }

        for (int i = 0; i < _barUIList.Count; i++)
        {
            if (i < _barCount)
            {
                _barUIList[i].MainRawImage.texture = _barTexture;

                _barUIList[i].AllColor = _green;
            }
            else
            {
                _barUIList[i].MainRawImage.texture = _lineTexture;

                _barUIList[i].AllColor = Color.white;
            }
        }
    }

    private void AddBar()
    {
        // 새로운 프리팹을 만들고 배열에 추가한다.
        GameObject tmp = Instantiate(_RawImagePrefab, this.transform);

        // 위치 지정.(상대위치)
        tmp.transform.localPosition = new Vector2(_barOffset.x + _barInterval.x, _barOffset.y + _barInterval.y * _barUIList.Count);

        if (tmp.TryGetComponent(out CRawImageUI barUI))
        {
            barUI.GlowRawImage.texture = _barGlowTexture;
            _barUIList.Add(barUI);
        }
    }

    public void ChangeState(ESystemState state)
    {
        if (_currentState == state) return;

        _currentState = state;

        Debug.Log($"{gameObject.name} ChangeState => {_currentState}");

        switch (_currentState)
        {
            case ESystemState.PowerOff:
                _iconTexture = _system.IconPreset.uiSprites[(int)CIconPreset.ESpriteID.Grey].texture;
                _iconGlowTexture = _system.IconPreset.uiSprites[(int)CIconPreset.ESpriteID.GreyGlow].texture;
                break;
            case ESystemState.PowerOn:
                _iconTexture = _system.IconPreset.uiSprites[(int)CIconPreset.ESpriteID.Green].texture;
                _iconGlowTexture = _system.IconPreset.uiSprites[(int)CIconPreset.ESpriteID.GreenGlow].texture;
                break;
            case ESystemState.Damaged:
                _iconTexture = _system.IconPreset.uiSprites[(int)CIconPreset.ESpriteID.Orange].texture;
                _iconGlowTexture = _system.IconPreset.uiSprites[(int)CIconPreset.ESpriteID.OrangeGlow].texture;
                break;
            case ESystemState.FatalDamage:
                _iconTexture = _system.IconPreset.uiSprites[(int)CIconPreset.ESpriteID.Red].texture;
                _iconGlowTexture = _system.IconPreset.uiSprites[(int)CIconPreset.ESpriteID.RedGlow].texture;
                break;
        }
        _iconObject.MainRawImage.texture = _iconTexture;
        _iconObject.GlowRawImage.texture = _iconGlowTexture;
    }
}
