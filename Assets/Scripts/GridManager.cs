using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] GameObject roadPrefab;
    [SerializeField] Vector2Int gridSize;
    [SerializeField] Vector3Int gridPos;
    [SerializeField] Transform debug;
    Camera mainCam;
    public int[,] map;

    private void Awake()
    {
        if (!Instance)
            Instance = this;

        map = new int[gridSize.x, gridSize.y];
        mainCam = Camera.main;
    }

    public void PlaceRoad(Vector3Int pos)
    {
        Vector2Int coordinates = new Vector2Int(System.Math.Abs(pos.x), System.Math.Abs(pos.z));
        print(coordinates);
        if (map[coordinates.x, coordinates.y] == 1)
        {
            print("already a road here ");
            return;
        }

        GameObject newRoad = Instantiate(roadPrefab, pos, Quaternion.identity);
        RoadTile tile = newRoad.GetComponent<RoadTile>();
        tile.Create();
        map[coordinates.x, coordinates.y] = 1;
        EventManager.Instance.onNewRoad.Invoke();
    }

    private void Update()
    {
        Ray screenRay = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(screenRay, out RaycastHit hit))
        {
            Vector3 mousePos = hit.point;
            gridPos = new Vector3Int(Mathf.RoundToInt(mousePos.x), 0, Mathf.RoundToInt(mousePos.z));
            debug.position = Vector3.Lerp(debug.position, gridPos, 50f * Time.deltaTime);
            if (Input.GetMouseButtonDown(0))
                PlaceRoad(gridPos);
        }
    }
}
