using UnityEngine;
using UnityEngine.UI;


#region CBarUI
/*

*/
#endregion

public class CBarUI : MonoBehaviour
{
    #region 인스펙터
    [Header("Bar")]
    public RawImage BarRawImage;
    [Header("Glow")]
    public RawImage GlowRawImage;
    #endregion

    #region 내부 변수

    #endregion

    void Awake()
    {
        if(BarRawImage ==  null)
        {
            Debug.LogWarning($"{gameObject.name} BarRawImage ==  null");
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
