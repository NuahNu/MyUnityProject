using UnityEngine;


#region CWeapon
/*
이건 루트나 부모에 추가하고
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
        Right,
        Front,
        Left
    }


    #region 인스펙터
    [Header("재장전 방식")]
    [SerializeField] private EReloadType _reloadType;

    [Header("무기의 방향 (Left 면 좌우반전, Front면 정면 무기.)")]
    [SerializeField] private EWeaponDir _weaponDir;

    [Header("활성화시 이동 거리")]
    [SerializeField] private float _activeOffset;

    [Header("무기 관련")]
    [SerializeField] private SpriteRenderer _weaponRenderer;
    #endregion

    #region 내부 변수

    #endregion

    void Awake()
    {
        if (_weaponRenderer == null)
        {
            Transform weaponT = transform.Find("weapon");
            GameObject weaponGO;    // 사용하지 않는 무기도 있다.

            if (weaponT != null)
            {
                weaponGO = weaponT.gameObject;
            }
            else
            {
                weaponGO = new GameObject("weapon");
                weaponGO.transform.parent = this.transform;
            }
            weaponGO.transform.localPosition = new Vector3(0, 0, Common.z_offset);

            if (!weaponGO.TryGetComponent(out _weaponRenderer))
            {
                weaponGO.AddComponent<SpriteRenderer>();
            }
            if (!weaponGO.TryGetComponent(out _weaponRenderer))
            {
                Debug.LogWarning($"At {gameObject.name} : _weaponRenderer == null");
                return;
            }
        }
    }

    void Start()
    {
        switch (_reloadType)
        {
            case EReloadType.Animation:
                break;

            case EReloadType.GlowImage:
                break;
            default:
                Debug.LogWarning($"{gameObject.name}의 _reloadType 이 설정되지 않았다. 확인해볼것.");
                break;
        }
        switch (_weaponDir)
        {
            case EWeaponDir.Left:
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

    }
}
