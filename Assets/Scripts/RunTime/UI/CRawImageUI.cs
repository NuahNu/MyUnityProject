using UnityEngine;
using UnityEngine.UI;


#region CBarUI
/*

*/
#endregion

public class CRawImageUI : MonoBehaviour
{
    #region 인스펙터
    [Header("Main")]
    [SerializeField] private RawImage _mainRawImage;
    [Header("Glow")]
    [SerializeField] private bool _glowFlag = false;
    [SerializeField] private RawImage _glowRawImage;
    #endregion

    #region 내부 변수

    #endregion

    // 이미지를 사용하고 나서 크기 재조정이 필요하다.

    public RawImage MainRawImage { get { return _mainRawImage; } }
    public RawImage GlowRawImage { get { return _glowRawImage; } }

    public bool GlowFlag { get { return _glowFlag; } set { _glowFlag = value; } }

    public Color AllColor
    {
        set
        {
            _mainRawImage.color = value;
            _glowRawImage.color = value;
        }
    }

    void Awake()
    {
        if (_mainRawImage == null)
        {
            Debug.LogWarning($"{gameObject.name} MainRawImage ==  null");
        }
        if (_glowFlag)
        {
            if (_glowRawImage == null)
            {
                Debug.LogWarning($"{gameObject.name} GlowRawImage ==  null");
            }
        }
        else
        {
            _glowRawImage.gameObject.SetActive(false);
        }
    }
}
