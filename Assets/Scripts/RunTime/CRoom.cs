using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


#region CRoom
/*
방에 어떤 시스템이 있는지 저장.
방에 몇명까지 들어갈 수 있는지 저장.

누가 들어가있는지 저장.
*/
#endregion

public class CRoom : MonoBehaviour, IPointerDownHandler
{
    #region 인스펙터
    [Header("몇칸")]
    [SerializeField] private int _sizeOfRoom;

    [Header("들어가있을 예정인 객체들")]
    [SerializeField] private List<GameObject> _allys;
    [SerializeField] private List<GameObject> _enemys;
    // 지금 당장 들어가 있지 않아도 들어갈 예정이라고 예약한다.
    // 따라서 미리 들어가도록 명령이 되어있다면 다른 녀석들이 못 들어가는게 맞다.
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

    public bool EnterRoom(GameObject people)
    {
        return true;
        return false;
    }

    public void ExitRoom(GameObject people)
    {

    }


    public void OnPointerDown(PointerEventData eventData)
    {
        CPlayerInput.Instance.OnPointerDown(eventData);
    }
}
