using Unity.Mathematics;
using UnityEngine;


#region CPeopleAnimController
/*
캐릭터(People)의 상태에 따라 다른 행동( 애니메이션 )을 한다.

모든 캐릭터는 이동 명령만 가능. 명령의 처리는 여기서 하지 않는다.
*/
#endregion

public class CPeopleAnimController : MonoBehaviour, CPeopleController.IStateListener, CPeopleController.IDirListener, CPeopleController.IBattleListenr
{
    #region 인스펙터
    [Header("애니메이터")]
    [SerializeField] private Animator _animator;

    [Header("애니메이터 파라미터")]
    [SerializeField] private string _paramState = "aState";
    [SerializeField] private string _paramMoveDir = "fMoveDir"; // 
    [SerializeField] private string _paramAttDir = "fAttDir";   // -1 ~ 1 / target - this
    #endregion

    #region 내부 변수
    // 파라미터 해시
    private int _hashState;
    private int _hashMoveDir;
    private int _hashAttDir;

    //private bool _hasShotParam;     // mantis
    //private bool _hasRepairParam;   //
    #endregion

    void Reset()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void Awake()
    {
        if (_animator == null)
        {
            _animator = GetComponentInChildren<Animator>();
        }

        _hashState = Animator.StringToHash(_paramState);
        _hashMoveDir = Animator.StringToHash(_paramMoveDir);
        _hashAttDir = Animator.StringToHash(_paramAttDir);

    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnStateChange(CPeopleController.EPeopleState changedState)
    {
        //Debug.Log($"changedState : {changedState}");

        _animator.SetInteger(_hashState, (int)changedState);
    }

    public void BeforeBattle(Vector2 relativeCoordinates)
    {
        //Debug.Log($"relativeCoordinates : {relativeCoordinates}");

        if (relativeCoordinates.x != 0)
            _animator.SetFloat(_hashAttDir, relativeCoordinates.x);
        else if (relativeCoordinates.y != 0)
            _animator.SetFloat(_hashAttDir, relativeCoordinates.y);
        else
        {
            _animator.SetFloat(_hashAttDir, 0);
        }
    }

    public void OnDirChange(Vector2 dir)
    {
        int _currentDir;        // 상 우 하 좌 0 ~ 4
        if(math.abs(dir.x) > math.abs(dir.y))
        {
            if (dir.x > 0) _currentDir = 1;
            else _currentDir = 3;
        }
        else if (math.abs(dir.x) < math.abs(dir.y))
        {
            if (dir.y > 0) _currentDir = 0;
            else _currentDir = 2;

        }
        else
        {
            // 둘의 변화량이 같을수가 없어야 하는데?
            Debug.LogWarning("이 문구가 나오면 CPeopleAnimController → OnDirChange 를 확인할것");
            _currentDir = 2;    // 오류방지용
        }

        //Debug.LogWarning($" Dir = {Dir}, float.{_currentDir}");
        _animator.SetFloat(_hashMoveDir, (float)_currentDir);  // 2D지만 블랜딩 중 중간 애니메이션을 사용하고 싶다면 뎀프와 델타를 사용한다.
    }
}
