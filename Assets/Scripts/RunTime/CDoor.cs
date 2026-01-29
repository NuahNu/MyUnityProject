using System;
using UnityEngine;
using UnityEngine.EventSystems;


#region CDoor
/*
문을 클릭하면 고정 상태를 토글한다.
유닛들이 지나다녀도 자동으로 열린다.
시스템이 마비되면
    자동으로 열리지 않는다.
    유저가 토글할 수 없다.
*/
#endregion

public class CDoor : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    #region 인스펙터
    [Header("애니메이터")]
    [SerializeField] private Animator _animator;

    [Header("애니메이터 파라미터")]
    [SerializeField] private string _paramIsDoorOpen = "IsDoorOpen";
    [SerializeField] private string _paramDoorLevel = "fDoorLevel"; // 


    [Header("문 초기 설정값")]
    [SerializeField] private bool _isDoorOpen = false;
    [SerializeField] private int _doorLevel = 0;
    #endregion

    #region 내부 변수
    private int _hashIsDoorOpen;
    private int _hashDoorLevel;
    #endregion

    private void Reset()
    {
        _animator = GetComponentInChildren<Animator>();

    }

    void Awake()
    {
        if (_animator == null)
        {
            _animator = GetComponentInChildren<Animator>();
        }

        _hashIsDoorOpen = Animator.StringToHash(_paramIsDoorOpen);
        _hashDoorLevel = Animator.StringToHash(_paramDoorLevel);
    }

    private void Start()
    {
        _animator.SetBool(_hashIsDoorOpen, _isDoorOpen);
        _animator.SetFloat(_hashDoorLevel, (float)_doorLevel);
    }

    public void TogleDoor()
    {
        _isDoorOpen = !_isDoorOpen;
        _animator.SetBool(_hashIsDoorOpen, _isDoorOpen);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            CPlayerInput.Instance.OnPointerDown(this.gameObject);
            TogleDoor();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 커서 바꾸기...
        // 커서 
        //eventData.pointerCurrentRaycast
        //RaycastResult
    }
}
