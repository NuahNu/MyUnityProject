using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D.Animation;


#region CPeopleController
/*
 모든 캐릭터(People)는 이동명령만 가능하며, 해당 명령에 따라 특정한 '방'에 도착하고, '방'의 상태에 따라 다른 행동을 한다. 
*/
#endregion

public class CPeopleController : MonoBehaviour, IPointerDownHandler
{
    public interface IStateListener
    {
        void OnStateChange(EPeopleState changedState);
    }
    public interface IDirListener
    {
        void OnDirChange(Vector2 Dir);
    }

    public interface IBattleListenr
    {
        void BeforeBattle(Vector2 relativeCoordinates);
    }


    // 우선순위를 위해 switch가 아닌 elsf if로 구현?
    public enum EPeopleState
    {
        Die,
        Walk,
        Attack,
        Shot,
        Extinguish,
        Repair,
        Work,
        Idle,
        Count
    }

    public enum ETag
    {
        None = 0,
        Ally,
        Enemy
    }

    // ftl의 이동 명령에 대해
    /*
    자신은 특정 방에 있다 라고만 알 수 있다.
    다른 방으로 이동하라 라는 명령을 받아 움직인다.

    각 방은 2,4 개의 칸으로 이루어져 있다.

    방 기준 상대 좌표를 갖고 있다. 이건 방이 갖는가 캐릭터가 갖는가
    */

    #region 인스펙터
    [Header("스프라이트 라이브러리")]
    [SerializeField] private SpriteLibrary _spriteLibrary;

    [Header("스프라이트 라이브러리 에셋")]
    [SerializeField] private SpriteLibraryAsset _idle;
    [SerializeField] private SpriteLibraryAsset _selected;

    [Header("진영 태그")]
    [SerializeField] private ETag _tag;
    #endregion

    #region 내부 변수
    private readonly List<IStateListener> _stateListeners = new List<IStateListener>();
    private readonly List<IBattleListenr> _battleListeners = new List<IBattleListenr>();
    private readonly List<IDirListener> _dirListeners = new List<IDirListener>();

    private EPeopleState _currentState = EPeopleState.Shot;
    private Vector2 _currentDir = Vector2.down;
    private Vector2 _relativeCoordinates = Vector2.zero;

    #endregion

    void Reset()
    {
        // 여기서 스라들 설정
        // 초기화니까 기존애 있던 녀석 날려버리기.
        if (_spriteLibrary != null) _spriteLibrary = null;

        _spriteLibrary = GetComponent<SpriteLibrary>();
    }

    private void OnValidate()
    {
        if (_tag == 0)
        {
            Debug.LogWarning($"{this.name}의 태그 설정을 하지 않았다. 인스펙터 확인");
        }
    }

    void Awake()
    {
        CacheListeners();

        // 여기서 실제로 존재하는지 한번 더 확인

    }

    private void CacheListeners()
    {
        _stateListeners.Clear();
        _battleListeners.Clear();
        _dirListeners.Clear();

        MonoBehaviour[] monos = GetComponentsInChildren<MonoBehaviour>(true);

        for (int i = 0; i < monos.Length; i++)
        {
            MonoBehaviour m = monos[i];

            if (m == null) continue;

            IStateListener state = m as IStateListener;
            if (state != null)
            {
                _stateListeners.Add(state);
            }

            IBattleListenr battle = m as IBattleListenr;
            if (state != null)
            {
                _battleListeners.Add(battle);
            }
            IDirListener dir = m as IDirListener;
            if (dir != null)
            {
                _dirListeners.Add(dir);
            }
        }
    }

    private void NotifyDir()
    {
        for (int i = 0; i < _dirListeners.Count; i++)
        {
            if (_dirListeners[i] == null) continue;

            _dirListeners[i].OnDirChange(_currentDir);
        }
    }

    private void NotifyState()
    {
        for (int i = 0; i < _stateListeners.Count; i++)
        {
            if (_stateListeners[i] == null) continue;

            _stateListeners[i].OnStateChange(_currentState);
        }
    }

    private void NotifyBattle()
    {
        for (int i = 0; i < _battleListeners.Count; i++)
        {
            if (_battleListeners[i] == null) continue;

            _battleListeners[i].BeforeBattle(_relativeCoordinates);
        }
    }

    void Start()
    {
        NotifyDir();
        NotifyState();
        NotifyBattle();
    }

    void Update()
    {
        // =======================================================
        // 테스트
        // 움직임 방향
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 newDir = new Vector2(x, y);

        if (newDir != Vector2.zero && _currentDir != newDir)
        {
            _currentDir = newDir;
            NotifyDir();
            //Debug.Log($"_currentDir = x : {newDir.x}  |  y : {newDir.y}");
        }

        transform.Translate(newDir * Time.deltaTime * 0.35f);

        // 공격 방향
        x = 0;
        y = 0;
        if (Input.GetKey(KeyCode.J)) x += -1;
        if (Input.GetKey(KeyCode.K)) y += -1;
        if (Input.GetKey(KeyCode.L)) x += 1;
        if (Input.GetKey(KeyCode.I)) y += 1;

        newDir = new Vector2(x, y);
        if (_relativeCoordinates != newDir)
        {
            _relativeCoordinates = newDir;
            NotifyBattle();
            //Debug.Log($"_relativeCoordinates = x : {newDir.x}  |  y : {newDir.y}");
        }

        // 상태 변경
        for (int i = 0; i < (int)EPeopleState.Count; i++)
        {
            if (Input.GetKey(KeyCode.Alpha1 + i))
            {
                EPeopleState newState = (EPeopleState)i;
                if (_currentState != newState)
                {
                    _currentState = newState;
                    NotifyState();
                    //Debug.Log($"_currentState : {newState}");
                }
            }

        }

        // =======================================================

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            CPlayerInput.Instance.OnPointerDown(this.gameObject);
            if(CPlayerInput.Instance == null)
            {
                Debug.LogWarning("CPlayerInput.Instance == null");
                return;
            }
            if(this == null)
            {
                Debug.LogWarning("this == null");
                return;
            }
            if(this.gameObject == null)
            {
                Debug.LogWarning("this.gameObject == null");
                return;
            }

        }
    }
}
