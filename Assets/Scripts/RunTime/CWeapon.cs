//using UnityEditor.Animations;
using UnityEngine;


#region CWeapon
/*
특정 객체에 추가하면 (아마도 Cship)
거기서 무기 설치 위치 배열을 가져와 거기에 무기를 만든다?

배가 알아서 그 위치에 오브젝트를 만들고 거기에 얘를 추가한다?

자식으로 하나 만든 후 로컬 좌표계를 수정한다.
*/
#endregion

public class CWeapon : MonoBehaviour
{
    public enum EReloadType
    {
        None,
        Animation,
        GlowImage
    }

    public enum EWeaponDir
    {
        None,
        Right,  // 이미지상 오른쪽
        Front,  // 이미지상 위
        Left    // 이미지상 왼쪽
    }


    #region 인스펙터
    [Header("재장전 방식")]
    [SerializeField] private EReloadType _reloadType;

    [Header("무기의 방향 (Left 면 좌우반전, Front면 정면 무기.)")]
    [SerializeField] private EWeaponDir _weaponDir;

    [Header("활성화시 이동 거리")]
    [SerializeField] private float _activeOffset;

    [Header("무기 관련")]
    [SerializeField] private GameObject _weaponGameObject;
    [SerializeField] private SpriteRenderer _weaponRenderer;
    //[SerializeField] private Sprite _weaponSprite;

    [Header("애니메이터")]
    [SerializeField] private Animator _animator;

    [Header("EReloadType.Animation")]
    //[SerializeField] private AnimatorController _animatorController;

    [Header("EReloadType.GlowImage")]
    [SerializeField] private SpriteRenderer _glowRenderer;
    [SerializeField] private Sprite _glowSprite;
    #endregion

    #region 내부 변수

    #endregion

    void Awake()
    {
        if (_weaponRenderer == null)
        {
            Transform weaponT = transform.Find("weapon");
            //_weaponGameObject;    // 사용하지 않는 무기도 있다.????

            if (weaponT != null)
            {
                _weaponGameObject = weaponT.gameObject;
            }
            else
            {
                _weaponGameObject = new GameObject("weapon");
                _weaponGameObject.transform.parent = this.transform;
            }
            _weaponGameObject.transform.localPosition = new Vector3(0, 0, Common.z_offset);

            if (!_weaponGameObject.TryGetComponent(out _weaponRenderer))
            {
                _weaponGameObject.AddComponent<SpriteRenderer>();
            }
            if (!_weaponGameObject.TryGetComponent(out _weaponRenderer))
            {
                Debug.LogWarning($"At {gameObject.name} : _weaponRenderer == null");
                return;
            }
        }
    }

    void Start()
    {
        if (_animator == null)
        {
            Debug.LogWarning($"{gameObject.name}이 _animator == null이다. _reloadType에 상관 없이 애니메이터는 필요하다.");
        }
        switch (_reloadType)
        {
            case EReloadType.Animation:
                break;
            case EReloadType.GlowImage:
                Debug.LogWarning($"{gameObject.name}의 EReloadType이 GlowImage이다. GlowImage는 미구현.");

                if (_glowRenderer == null)
                {
                    Debug.LogWarning($"{gameObject.name}의 EReloadType이 GlowImage _glowRenderer == null이다.");
                }
                if (_glowSprite == null)
                {
                    Debug.LogWarning($"{gameObject.name}의 EReloadType이 GlowImage _glowSprite == null이다.");
                }
                break;
            default:
                Debug.LogWarning($"{gameObject.name}의 _reloadType 이 설정되지 않았다. 확인해볼것.");
                break;
        }

        switch (_weaponDir)
        {
            case EWeaponDir.Left:
                _weaponRenderer.flipX = true;
                break;
            case EWeaponDir.Front:
                break;
            case EWeaponDir.Right:
                break;
            default:
                Debug.LogWarning($"{gameObject.name}의 _weaponDir 이 설정되지 않았다. 확인해볼것.");
                break;
        }
    }

    void Update()
    {
        switch (_reloadType)
        {
            case EReloadType.Animation:
                break;
            case EReloadType.GlowImage:
                break;
        }
    }
}
