using UnityEngine;
using UnityEngine.EventSystems;


#region CRoom
/*

*/
#endregion

public class CRoom : MonoBehaviour, IPointerDownHandler
{
    #region 인스펙터

    #endregion

    #region 내부 변수

    #endregion

    void Awake()
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            CPlayerInput.Instance.OnPointerDown(this.gameObject);
        }
    }
}
