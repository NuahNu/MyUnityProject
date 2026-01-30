using UnityEngine;
using UnityEngine.EventSystems;


#region CPlayerInput
/*
일단 싱글톤

뭔가 한다.

지금은 게임 씬이니까


*/
#endregion



public class CPlayerInput : MonoBehaviour
{
    #region 인스펙터

    #endregion

    #region 내부 변수
    public static CPlayerInput Instance { get; private set; }

    private CPeopleController _selectedPeople;
    #endregion

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("중복 감지 → 기존 인스턴스가 있으므로 현재 오브젝트 제거");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // 다른 씬에서 다른걸 사용할까??
        //DontDestroyOnLoad(this.gameObject);

    }

    void Start()
    {

    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Debug.LogWarning("CPlayerInput.Instance 삭제.");
            Instance = null;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 캐릭터면 선택 상태로 만든다.
        // 드래그가 가능한데, 여러개를 선택하도록 한다.

        // 클릭 혹은 상호작용이 가능한 객체들은 하이라이트 효과가 있다.
        // 유닛은 기본 유닛이 노란색 하이라이트인데, 선택하거나 커서가 올라가면 초록색으로
        // 문은 파란색 하이라이트
        // 방은 유닛이 선택된 상태일때만
        //  노란색 하이라이트 로 표시된다.

        PointerEventData.InputButton button = eventData.button;
        GameObject eventGameObject = eventData.pointerCurrentRaycast.gameObject;

        // 유닛 선택
        if (//eventGameObject.CompareTag("Ally") &&
            button == PointerEventData.InputButton.Left &&
            eventGameObject.TryGetComponent(out CPeopleController peopleController))
        {
            // 선택된 오브젝트가 된다.
            _selectedPeople = peopleController;
            Debug.Log($"{_selectedPeople.name} 선택");
            // 하이라이트를 해준다.
        }
        // 유닛 방 이동
        else if (_selectedPeople != null && button == PointerEventData.InputButton.Right &&
            eventGameObject.TryGetComponent(out CRoom room)) // 선택된 유닛이 있고, 방을 우클릭 했으면
        {
            // 이동 명령을 한다.
            _selectedPeople.ChangeTargetRoom(room);

            Debug.Log($"{_selectedPeople.name}는 {room.transform.position}로 이동한다.");
        }
        else
        {
            // 유효하지 않은 클릭이면 해제한다.
            if (_selectedPeople != null)
                Debug.Log($"{_selectedPeople.name} 선택 해제");
            _selectedPeople = null;
        }
    }

    private void Update()
    {
        if (Instance == null || Instance != this)
        {
            Debug.LogWarning("이럴수가 있나?");
            if (Instance != null)
                Destroy(gameObject);
        }
        Instance = this; ;

    }
}
