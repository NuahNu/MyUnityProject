using UnityEngine;


#region CCursorChanger
/*

*/
#endregion

public class CCursorChanger : MonoBehaviour
{
    public enum CursorType
    {
        Default,

    }
    #region 인스펙터
    [Header("커서 이미지")]
    [SerializeField] private Texture2D _default;
    #endregion

    #region 내부 변수
    
    #endregion

    void Awake()
    {
        // 1. 그냥 내부 함수 사용.
        Cursor.SetCursor(_default, Vector2.zero, CursorMode.Auto);
        // 2. 비활성화하고 직접 그린다.
        //Cursor.visible = false;
        // SpriteRenderer를 갖는 오브젝트를 생성한다.

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
