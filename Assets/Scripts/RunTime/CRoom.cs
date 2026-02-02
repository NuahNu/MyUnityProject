using System;
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
    [Header("크기")]
    [SerializeField] private int _sizeOfRoom;

    [Header("문의 위치")]
    [SerializeField] private EDoorPosition _doorPosition;

    [Header("들어가있을 예정인 객체들")]
    [SerializeField] private List<GameObject> _allys;
    [SerializeField] private List<GameObject> _enemys;
    // 지금 당장 들어가 있지 않아도 들어갈 예정이라고 예약한다.
    // 따라서 미리 들어가도록 명령이 되어있다면 다른 녀석들이 못 들어가는게 맞다.

    [Header("설치된 시스템")]
    [ReadOnly]
    [SerializeField] private CSystem _system;
    #endregion

    #region 내부 변수

    #endregion

    public bool IsExistSystem {  get { return _system != null; } }
    public int SizeOfRoom {  get { return _sizeOfRoom; } }

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
        if (people.tag == "Ally")
        {
            if (!_allys.Contains(people))
            {
                if(_allys.Count <  _sizeOfRoom)
                {
                    _allys.Add(people);
                    return true;
                }
                else
                {
                    Debug.LogWarning("인원 초과.");
                }
            }
            else
            {
                Debug.LogWarning("이미 추가된 유닛이다.");// 이게 어떻게 가능한거지?
            }
        }
        else if (people.tag == "Enemy")
        {
            if (!_enemys.Contains(people))
            {
                if (_enemys.Count < _sizeOfRoom)
                {
                    _enemys.Add(people);
                    return true;
                }
                else
                {
                    Debug.LogWarning("인원 초과.");
                }
            }
            else
            {
                Debug.LogWarning("이미 추가된 유닛이다.");// 이게 어떻게 가능한거지?
            }
        }
        else
        {
            Debug.LogWarning("태그가 지정되지 않은 유닛이다.");
        }
        return false;
    }

    public void ExitRoom(GameObject people)
    {
        if (people.CompareTag("Ally"))
        {
            _allys.Remove(people);
        }
        else if (people.CompareTag("Enemy"))
        {
            _enemys.Remove(people);
        }
        else
        {
            Debug.LogWarning("태그가 지정되지 않은 유닛이다.");
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        CPlayerInput.Instance.OnPointerDown(eventData);
    }

    public bool CheckEnemy(GameObject gameObject, out GameObject taget)
    {
        taget = null;
        //if (gameObject.TryGetComponent(out CPeopleController pc))
        string tag = gameObject.tag;
        switch (tag)
        {
            case "Ally":
                if (_enemys != null && _enemys.Count > 0)
                {
                    taget = _enemys[0];
                    return true;
                }
                break;
            case "Enemy":
                if (_allys != null && _allys.Count > 0)
                {
                    taget = _allys[0];
                    return true;
                }
                break;
            default: return false;
        }

        return false;
    }

    public bool NeedExtinguish()
    {
        return false;
    }

    public bool NeedRepair()
    {
        return false;
    }


    internal bool Install(CSystem.ESystemType type)
    {
        CSystem newSystem = this.gameObject.AddComponent<CSystem>();

        newSystem.Init(this, type);

        _system = newSystem;

        return _system.IsExistInterior;
    }
}
