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
    [SerializeField] private Transform _subSystemUIOffse;
    [SerializeField] private Vector2 _systemUIInterval = Vector2.zero;

    [Header("사일로 UI")]
    [SerializeField] private Transform _siloUIOffse;
    [SerializeField] private List<CRawImageUI> _siloUI = new();

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
            // 시스템 UI
            // 추가 코드
            for (int i = 0; i < (int)CSystem.ESystemType.Count; i++)
            {
                CSystem.ESystemType type = (CSystem.ESystemType)i;

                if (_allySystemDic.ContainsKey(type) && !_allySystemUIDic.ContainsKey(type))
                {
                    //type에 맞는 시스템 UI를 만든다.
                    GameObject tmp = Instantiate(_systemUIPrefab, (CSystem.IsSubSystem(type) ? _subSystemUIOffse : _systemUIOffse));

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
            // 위치 갱신 - 배와 직접 연결이 아니라 컨트롤러와의 연결로 구현하면 
            // 거기의 키 조작 순서에 따라 그리면 된다?
            int count = 0;
            int subCount = 0;   // 얘는 우에서 좌로 역순으로 그려야 패팅이 될 것 같은데>?
            for (int i = 0; i < (int)CSystem.ESystemType.Count; i++)
            {
                CSystem.ESystemType type = (CSystem.ESystemType)i;

                if (_allySystemUIDic.ContainsKey(type))
                {
                    _allySystemUIDic[type].transform.localPosition = new Vector2(
                        (CSystem.IsSubSystem(type) ? _systemUIInterval.x * subCount++ : _systemUIInterval.x * count++), 0/*_systemUIInterval.y*/);
                }
            }
            _allyChangeFlag = false;

            // 사일로
            // 추가 코드
            if(_siloUI.Count < _allyShip.CurrentMaxPower)
            {

            }
            //_allyShip.RemainingPower; // 만큼 바를 활성화한다.
            // 새로 그리기?
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
