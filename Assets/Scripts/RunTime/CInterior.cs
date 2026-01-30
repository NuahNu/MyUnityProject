using System.Linq;
using UnityEngine;


#region CInterior
/*

*/
#endregion

public class CInterior : MonoBehaviour
{
    #region 인스펙터
    [Header("메인 이미지")]
    [SerializeField] private SpriteRenderer _mainSpriteRenderer;
    [SerializeField] private Sprite _mainSprite;

    [Header("글로우 이미지")]
    [SerializeField] private SpriteRenderer _glowSpriteRenderer;
    [SerializeField] private Sprite[] _glowSprites;
    #endregion

    #region 내부 변수

    #endregion

    void Awake()
    {
        if (_mainSpriteRenderer == null)
            Debug.LogWarning($"At {gameObject.name} : _mainSpriteRenderer == null");
        if (_mainSprite == null)
            Debug.LogWarning($"At {gameObject.name} : _mainSprite == null");

        if (_glowSpriteRenderer == null)
            Debug.LogWarning($"At {gameObject.name} : _glowSpriteRenderer == null");
        if (_glowSprites == null || _glowSprites.Length < 3)
            Debug.LogWarning($"At {gameObject.name} : _glowSprites == null || _glowSprites.Length < 3");
    }

    void Start()
    {
        _mainSpriteRenderer.sprite = _mainSprite;
        _glowSpriteRenderer.sprite = _glowSprites[1];
    }

    void Update()
    {
        
    }
}
