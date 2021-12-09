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
    [SerializeField] Material freeMat, stuckMat;
    Camera mainCam;
    public int[,] map;
    public Dictionary<Vector2Int, RoadTile> roadTiles = new Dictionary<Vector2Int, RoadTile>();

    private void Awake()
    {
        if (!Instance)
            Instance = this;

        map = new int[gridSize.x, gridSize.y];
        mainCam = Camera.main;
    }

    public RoadTile GetRoadTile(Vector2Int coordinates)
    {
        if (roadTiles.TryGetValue(coordinates, out RoadTile tile))
            return tile;

        return null;
    }

    public void PlaceRoad(Vector3Int pos)
    {
        Vector2Int coordinates = new Vector2Int(pos.x, pos.z);
        //print(coordinates);
        if (map[coordinates.x, coordinates.y] == 1)
        {
            //print("already a road here ");
            return;
        }

        map[coordinates.x, coordinates.y] = 1;
        GameObject newRoad = Instantiate(roadPrefab, pos, Quaternion.identity);
        RoadTile tile = newRoad.GetComponent<RoadTile>();
        tile.Create(coordinates);
        roadTiles.Add(coordinates, tile);
        EventManager.Instance.onNewRoad.Invoke();

        GameManager.instance.CheckNeighboors(coordinates);
    }

    public void RemoveRoad(Vector3Int pos)
    {
        Vector2Int coordinates = new Vector2Int(pos.x, pos.z);
        print(coordinates);
        if (map[coordinates.x, coordinates.y] == 0)
        {
            print("no road here ");
            return;
        }

        if (roadTiles.TryGetValue(coordinates, out RoadTile tile))
        {
            tile.Death();
            map[coordinates.x, coordinates.y] = 0;
            roadTiles.Remove(coordinates);
        }
    }

    private void Update()
    {
        Ray screenRay = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(screenRay, out RaycastHit hit))
        {
            Vector3 mousePos = hit.point;
            gridPos = new Vector3Int(Mathf.RoundToInt(mousePos.x), 0, Mathf.RoundToInt(mousePos.z));
            debug.position = Vector3.Lerp(debug.position, gridPos, 50f * Time.deltaTime);
            Vector2Int coordinates = new Vector2Int(gridPos.x, gridPos.z);
            if (Input.GetMouseButton(0))
                PlaceRoad(gridPos);
            else if (Input.GetMouseButton(1))
                RemoveRoad(gridPos);

            /*if (map[coordinates.x, coordinates.y] == 0)
                debug.GetComponentInChildren<MeshRenderer>().material = freeMat;
            else if (map[coordinates.x, coordinates.y] == 1)
                debug.GetComponentInChildren<MeshRenderer>().material = stuckMat;*/
        }
    }
}
