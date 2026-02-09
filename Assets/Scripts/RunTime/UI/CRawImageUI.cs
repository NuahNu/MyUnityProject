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
    public RawImage MainRawImage;
    [Header("Glow")]
    public RawImage GlowRawImage;
    #endregion

    #region 내부 변수

    #endregion

    void Awake()
    {
        if (MainRawImage == null)
        {
            Debug.LogWarning($"{gameObject.name} MainRawImage ==  null");
            Debug.LogWarning($"{gameObject.name} GlowRawImage ==  null");
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
