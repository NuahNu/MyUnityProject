using System.Collections.Generic;
using UnityEngine;


#region CMainUI
/*

*/
#endregion

public class CMainUI : MonoBehaviour
{
    #region 인스펙터
    [Header("플레이어 컨트롤러 (일단 함선을 직접 연결. 나중에 수정) ")]
    [SerializeField] private CShip _allyShip;

    [SerializeField] private List<CSystemUI> _systemUIList = new();

    [Header("적 컨트롤러")]
    [SerializeField] private CShip _enemyShip;

    [Header("시스템 UI 시작 위치 및 간격")]
    [SerializeField] private Transform _systemUIOffse;
    [SerializeField] private Vector2 _systemUIInterval = Vector2.zero;

    [Header("캔버스")]
    [SerializeField] private Canvas _canvas;

    [Header("프리팹")]
    [SerializeField] private GameObject _systemUIPrefab;
    #endregion

    #region 내부 변수
    private Dictionary<CSystem.ESystemType, CSystem> _allySystemDic;

    private Dictionary<CSystem.ESystemType, CSystemUI> _allySystemUIDic = new();


    private Dictionary<CSystem.ESystemType, CSystem> _enemySystemDic;

    private bool _allyChangeFlag = false;
    #endregion

    void Awake()
    {

    }

    void Start()
    {
        // 아군 함선의 정보를 긁어와 UI를 만들고, 그린다.
        if (_allyShip == null)
        {
            Debug.LogWarning($"{gameObject.name} _allyShip == null");
            return;
        }
        _allyShip.MainUI = this;
        _allySystemDic = _allyShip.InstalledSystem;

        //적군 함선은 좀 다르다... 일단 긁어와
        if (_enemyShip == null)
        {
            Debug.LogWarning($"{gameObject.name} _enemyShip == null");
            return;
        }
        _enemyShip.MainUI = this;
        _enemySystemDic = _enemyShip.InstalledSystem;
    }

    void Update()
    {
        if (_allyChangeFlag)
        {
            // 추가 코드
            for (int i = 0; i < (int)CSystem.ESystemType.Count; i++)
            {
                CSystem.ESystemType type = (CSystem.ESystemType)i;

                if (_allySystemDic.ContainsKey(type) && !_allySystemUIDic.ContainsKey(type))
                {
                    //type에 맞는 시스템 UI를 만든다.
                    GameObject tmp = Instantiate(_systemUIPrefab, _systemUIOffse);

                    tmp.name = type.ToString();

                    tmp.transform.localPosition = Vector2.zero;

                    if (tmp.TryGetComponent(out CSystemUI systemUI))
                    {
                        systemUI.System = _allySystemDic[type];

                        _systemUIList.Add(systemUI);
                        _allySystemUIDic.Add(type, systemUI);
                    }
                }
            }
            // 위치 갱신
            int count = 0;
            for (int i = 0; i < (int)CSystem.ESystemType.Count; i++)
            {
                CSystem.ESystemType type = (CSystem.ESystemType)i;

                if (_allySystemUIDic.ContainsKey(type))
                {
                    _allySystemUIDic[type].transform.localPosition = new Vector2( _systemUIInterval.x * count++,0/*_systemUIInterval.y*/);
                }
            }
            _allyChangeFlag = false;
        }

    }

    public void ChangeFlag(string tag)
    {
        switch (tag)
        {
            case "Ally":
                _allyChangeFlag = true;
                break;
            case "Enemy":
                break;
        }
    }
}
