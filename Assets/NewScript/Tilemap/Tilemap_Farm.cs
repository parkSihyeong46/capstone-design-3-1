using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tilemap_Farm : MonoBehaviour
{
    [Header("Get Script")]
    [SerializeField] Tilemap_Reader tilemap_Reader;
    [SerializeField] Tilemap_Marker tilemap_Marker;
    
    [Header("Plowing")]
    [SerializeField] Tilemap farmingTilemap;    //경작지가 그려질 타일맵
    [SerializeField] TileBase plowedTile;       //경작된 땅 타일이미지(룰타일)

    [Header("Watering")]
    [SerializeField] Tilemap wateringTilemap;   //물뿌려진 땅이 그려질 타일맵
    [SerializeField] TileBase wateredTile;      //물뿌려진 타일이미지(룰타일)

    [Header("Crop")]
    [SerializeField] GameObject tempCrop;       //심어질 작물. 다른거 고르면 바뀌게 해야함

    Vector3Int cellPos;
    bool isPlowed;  //필요할까?

    //한번 경작한 땅을 어떻게 다시 원상복구 시킬까?
    //경작한 순서대로 원래대로 돌아갔으면 좋겠는데
    //아무것도 심어져 있지 않을 때만 복구
    //당장 생각나는 방법은 리스트나 배열 사용해서 좌표값 저장한다음 일정 시간 지나면 타일 없앰. 만약 뭔가 심어져있으면 다음 순번으로 넘어감.

    public void PlowGround()
    {
        cellPos = tilemap_Marker.markedCellPosition;

        bool isPlowalbe = tilemap_Reader.GetMousePosTileData(cellPos);

        //마커 표시됐을때, 땅 위에 아무 오브젝트도 없을 때 경작하도록 함
        if(tilemap_Marker.isShow == true)
        {
            if (isPlowalbe == true && tilemap_Reader.isEmpty == true)
            {
                farmingTilemap.SetTile(cellPos, plowedTile);
            }
        }
    }

    public void SeedGround()
    {
        cellPos = tilemap_Marker.markedCellPosition;

        //타일이 있을 때만 모종(타일이 있다는 건 경작지라는 뜻)
        if (farmingTilemap.GetTile(cellPos) != null && tilemap_Reader.isEmpty == true)
        {
            //씨앗 심기
            isPlowed = true;
            Vector3 cropPos = new Vector3(cellPos.x + 0.5f, cellPos.y + 0.5f, 0);
            Instantiate(tempCrop, cropPos, Quaternion.identity);
        }
        else
        {
            isPlowed = false;
            return;
        }
    }

    void CropSystem()
    {
        //코루틴으로 자라는 시간 설정해서 일정시간 지나면 다 자라게 함. 아니면 시간 시스템 있으니까 그거 이용해서 얼마 지나면 다 자라도록 함
    }

    IEnumerator CropGrowing()
    {
        //작물이 자라는 코루틴(알아서 잘 자라게 해야하나? 아니면 단계별로 물줘야 자라게 해야하나?)

        return null;
    }

    IEnumerator CropRusting()
    {
        //관리하지 않으면 썩는 코루틴
        return null;
    }




    //체크할 타일맵의 모든 타일을 검사?
    //1. 마우스 위치를 그리드포지션으로 반환
    //2. 그 위치(그리드포지션)의 타일을 가져옴
    //3. 그 타일은 체크타일맵의 타일임
    //4. 그 좌표상에는 체크타일맵 말고 다른 오브젝트나 타일이 없어야함
}
