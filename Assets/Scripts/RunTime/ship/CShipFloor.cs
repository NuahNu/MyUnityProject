using System;
using UnityEngine;


#region CShipFloor
/*
Floor 이미지를 갖는 친구
없는 녀석들은 직접 그려줘야하다.
Floor가 없어도 방들은 직접 그려야한다,.

각 floor 는 여러개의 room으로 구한성한다.
이걸 각각의 스크립트로 구현하던가
좌표의 배열의 배열로 만든다던가.

모든 유닛은 room을 기준으로 움직일 수 있다
같은 진영의 유닛은 같은 칸에 들어갈 수 없다.
각 room엥는 system이 설치될 수 있다.
    system...


// 방과 문을 먼저 구현하고, 타일을 구분해보자..
*/
#endregion

[Serializable]
public class CTile
{
    // 타일이 있는가
    public bool IsTile = false;
    // 상하좌우 이동 가능 여부
    // 방 ID
    public int TileIndex;
    // 
}

[Serializable]
public class CRoomData
{
    public int[] TileIndex;
    // 이동 가능한 방 - 연결된 방
    // 연결된 문의 위치.?
}



public class CShipFloor : MonoBehaviour
{
    

    #region 인스펙터
    [Header("타일 맵")]
    [SerializeField] private CTile[,] _tiles;    // 타일 정보
    [SerializeField] private GameObject[] _roomDatas;
    [SerializeField] private GameObject[] _doors;
    #endregion

    #region 내부 변수

    #endregion

    void Awake()
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
