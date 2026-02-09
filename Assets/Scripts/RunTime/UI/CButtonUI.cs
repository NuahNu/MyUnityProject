using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


#region CButtonUI
/*

*/
#endregion

public class CButtonUI : MonoBehaviour
{
    #region 인스펙터
    [Header("연결된 시스템")]
    //[ReadOnly]
    [SerializeField] private CSystem _system;

    [Header("asdf")]
    [SerializeField] private int _asdf;

    [Header("빠스 (15 X 6)")]
    [SerializeField] private GameObject _barUIProfab;
    [SerializeField] private List<CRawImageUI> _barUIList;
    [SerializeField] private Vector2 _posOffset = Vector2.zero;

    [Header("캔버스")]
    [SerializeField] private Transform _canvas;

    [Header("바 이미지")]
    [SerializeField] private Texture _line;
    [SerializeField] private Texture _bar;
    [SerializeField] private Texture _barGlow;

    [Header("색(100, 255, 100)")]
    [SerializeField] private Color _green;
    #endregion

    #region 내부 변수
    // 그릴 아이콘
    // 대충 시스템의 정보들 긁어와서 그려야 한다.how?
    private int _barCount = 0;
    #endregion

    void Awake()
    {

    }

    public void SetSystem(CSystem system)
    {
        _system = system;
    }

    void Start()
    {
        if (_system == null)
        {
            Debug.LogWarning($"{gameObject.name} _system == null");
        }
    }

    void Update()
    {
        if (_system == null)
            return;

        UpdateBar();
    }

    private void UpdateBar()
    {
        if (_system.CurrentPower != _barCount)
        {
            _barCount = _system.CurrentPower;
        }


        while (_system.MaxPower != _barUIList.Count)
        {
            AddBar();
        }

        for (int i = 0; i < _barUIList.Count; i++)
        {
            if (i < _barCount)
            {
                _barUIList[i].MainRawImage.texture = _bar;
                _barUIList[i].MainRawImage.color = _green;

                _barUIList[i].GlowRawImage.color = _green;
            }
            else
            {
                _barUIList[i].MainRawImage.texture = _line;
                _barUIList[i].MainRawImage.color = Color.white;

                _barUIList[i].GlowRawImage.color = Color.white;
            }
        }
    }

    private void AddBar()
    {
        // 새로운 프리팹을 만들고 배열에 추가한다.
        GameObject tmp = Instantiate(_barUIProfab, this.transform);

        // 위치 지정.(상대위치)
        tmp.transform.localPosition = new Vector2(_posOffset.x, _posOffset.y * _barUIList.Count);

        if (tmp.TryGetComponent(out CRawImageUI barUI))
        {
            barUI.GlowRawImage.texture = _barGlow;
            _barUIList.Add(barUI);
        }
    }
}
