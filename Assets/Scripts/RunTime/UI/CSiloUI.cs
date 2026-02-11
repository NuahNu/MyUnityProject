using System.Collections.Generic;
using UnityEngine;


#region CSiloUI
/*

*/
#endregion

public class CSiloUI : MonoBehaviour
{
    #region 인스펙터
    [Header("연결된 Ship")]
    [SerializeField] private CShip _ship;

    [Header("RawImagePregab")]
    [SerializeField] private GameObject _RawImagePrefab;

    [Header("바 Texture")]
    [SerializeField] private Texture _lineTexture;
    [SerializeField] private Texture _barTexture;
    [SerializeField] private Texture _barGlowTexture;

    [Header("바 정보")]
    [SerializeField] private List<CRawImageUI> _barUIList;
    [SerializeField] private Vector2 _barOffset = new Vector2(0, 0);
    [SerializeField] private Vector2 _barInterval = new Vector2(0, 10);

    [Header("색(100, 255, 100)")]
    [SerializeField] private Color _green = new Color(100f / 255, 1, 100f / 255, 1);
    #endregion

    #region 내부 변수

    #endregion

    public CShip Ship { get { return _ship; } set { _ship = value; } }

    void Awake()
    {

    }

    void Start()
    {
        if (_ship == null)
        {
            Debug.LogWarning($"{gameObject.name} _ship == null");
            return;
        }
    }

    void Update()
    {
        if (_ship == null) return;


        // add
        while (_barUIList.Count < _ship.CurrentMaxPower)
        {
            GameObject go = Instantiate(_RawImagePrefab, this.transform);
            go.transform.localPosition = new Vector2(_barOffset.x + _barInterval.x, _barOffset.y + _barInterval.y * _barUIList.Count);

            if (go.TryGetComponent(out CRawImageUI rawImageUI))
            {
                //rawImageUI.GlowRawImage.texture = _barGlowTexture; 이건 아마 기본값과 같다.
                _barUIList.Add(rawImageUI);
            }
        }
        //_allyShip.RemainingPower; // 만큼 바를 활성화한다.

        for (int i = 0; i < _barUIList.Count; i++)
        {
            if (i < _ship.RemainingPower)
            {
                _barUIList[i].MainRawImage.texture = _barTexture;

                _barUIList[i].AllColor = _green;
            }
            else
            {
                _barUIList[i].MainRawImage.texture = _lineTexture;

                _barUIList[i].AllColor = Color.white;
            }
        }

    }
}
