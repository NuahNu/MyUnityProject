using System;
using UnityEngine;


#region CInterior
/*

*/
#endregion

public class CInterior : MonoBehaviour
{
    public enum EInterState
    {
        Offline,
        Online,
        Disabled,
        Count
    }

    [Flags]
    public enum EEquipmentPosition
    {
        UpLeft = 1 << 0,
        UpRight = 1 << 1,
        RightUp = 1 << 2,
        RightDown = 1 << 3,
        DownLeft = 1 << 4,
        DownRight = 1 << 5,
        LeftUp = 1 << 6,
        LeftDown = 1 << 7,
    }

    #region 인스펙터
    [Header("메인 이미지")]
    [SerializeField] private SpriteRenderer _mainSpriteRenderer;
    [SerializeField] private Sprite _mainSprite;

    [Header("글로우 이미지")]
    [SerializeField] private bool _isUseGlow = true;
    [SerializeField] private SpriteRenderer _glowSpriteRenderer;
    [SerializeField] private Sprite[] _glowSprites;
    // 0 - 자리에 없
    // 1 - 자리에 있
    // 2 - 작동 불가능

    [Header("사이즈 (방처럼 2, 4만 존재)")]
    [SerializeField] private int _size;

    [Header("장비 위치")]
    [SerializeField] private EEquipmentPosition _equipmentPosition;
    #endregion

    #region 내부 변수
    private EInterState _currentState = EInterState.Offline;
    #endregion

    void Awake()
    {
        if (_mainSpriteRenderer == null)
            Debug.LogWarning($"At {gameObject.name} : _mainSpriteRenderer == null");
        if (_mainSprite == null)
            Debug.LogWarning($"At {gameObject.name} : _mainSprite == null");

        if (_isUseGlow == true)
        {
            if (_glowSpriteRenderer == null)
                Debug.LogWarning($"At {gameObject.name} : _glowSpriteRenderer == null");
            if (_glowSprites == null || _glowSprites.Length < 3)
                Debug.LogWarning($"At {gameObject.name} : _glowSprites == null || _glowSprites.Length < 3");
        }

        if (_size != 2 && _size != 4)
        {
            Debug.LogWarning($"At {gameObject.name} : 방의 크기는 무조건 2 아니면 4이여야 합니다. 일단은");
        }

    }

    void Start()
    {
        _mainSpriteRenderer.sprite = _mainSprite;
        if (_isUseGlow == true)
        {
            _glowSpriteRenderer.sprite = _glowSprites[0];
        }
    }

    void Update()
    {

    }

    public void ChangeState(EInterState state)
    {
        if (!_isUseGlow) return; // 점등 효과가 없는 것들도 있더라..

        if (_currentState != state)
        {
            _currentState = state;
            _glowSpriteRenderer.sprite = _glowSprites[(int)_currentState];
        }
    }
}
