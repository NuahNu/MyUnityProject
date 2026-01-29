using UnityEngine;


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


    public static CPlayerInput Instance {  get; private set; }
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

    void Update()
    {

    }
    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void OnPointerDown(GameObject gameObject)
    {
        // 캐릭터면 선택 상태로 만든다.
        // 드래그가 가능한데, 여러개를 선택하도록 한다.
    }
}
