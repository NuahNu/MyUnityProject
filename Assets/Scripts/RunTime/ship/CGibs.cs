using UnityEngine;


#region CGibs
/*

*/
#endregion

public class CGibs : MonoBehaviour
{
    #region 인스펙터
    [Header("Gibs")]
    [SerializeField] private Transform[] _gibs; // 트랜스폼을 가져와서 .gameObject 를 사용

    [Header("속성")]
    [SerializeField] private float _time = 3;

    [SerializeField] private float _maxSpeed = 3;
    [SerializeField] private float _minSpeed = 0;

    [SerializeField] private float _maxRotateSpeed = 30;

    #endregion

    #region 내부 변수
    //private readonly Dictionary<string, GameObject> Gibs = new Dictionary<string, GameObject>();
    private float _startTime;

    private Vector3[] _dirs;
    private float[] _rotates;
    #endregion

    private void Reset()
    {
        if (_gibs == null || _gibs.Length == 0)
            _gibs = GetComponentsInChildren<Transform>();
    }

    void Awake()
    {
        if (_gibs == null || _gibs.Length == 0)
            _gibs = GetComponentsInChildren<Transform>();
    }

    void Start()
    {
        _startTime = Time.time;

        _dirs = new Vector3[_gibs.Length];
        _rotates = new float[_gibs.Length];

        for (int i = 0; i < _gibs.Length; i++)
        {
            Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            dir = dir.normalized;

            float speed = Random.Range(_minSpeed, _maxSpeed);

            _dirs[i] = dir * speed * Time.deltaTime;

            _rotates[i] = Random.Range(-_maxRotateSpeed, _maxRotateSpeed) * Time.deltaTime;
        }
    }

    void Update()
    {
        // 일정 시간이 지나면 이 오브젝트 삭제
        if (Time.time - _startTime > _time)
        {
            Destroy(gameObject);
        }


        for (int i = 0; i < _gibs.Length; i++)
        {
            _gibs[i].position += _dirs[i];
            _gibs[i].Rotate(Vector3.forward, _rotates[i]);
        }

    }
}
