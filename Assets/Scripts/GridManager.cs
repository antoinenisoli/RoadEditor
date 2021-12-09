using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public Mesh CurrentTileMesh { get; set; }

    [SerializeField] GameObject roadPrefab;
    [SerializeField] Vector2Int gridSize;
    [SerializeField] Vector3Int gridPos;
    [SerializeField] Transform roadPreview;
    [SerializeField] Material freeMat, stuckMat;
    Camera mainCam;
    public int[,] map;
    Dictionary<Vector2Int, RoadTile> roadTiles = new Dictionary<Vector2Int, RoadTile>();

    private void Awake()
    {
        if (!Instance)
            Instance = this;

        CurrentTileMesh = roadPrefab.GetComponentInChildren<MeshFilter>().sharedMesh;
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
        if (!InRange(coordinates.x, coordinates.y))
            return;
        if (map[coordinates.x, coordinates.y] == 1)
        {
            //print("already a road here ");
            return;
        }

        map[coordinates.x, coordinates.y] = 1;
        GameObject newRoad = Instantiate(roadPrefab, pos, Quaternion.identity);
        RoadTile tile = newRoad.GetComponent<RoadTile>();
        tile.Create(coordinates);
        tile.SetMesh(CurrentTileMesh);
        roadTiles.Add(coordinates, tile);
        EventManager.Instance.onNewRoad.Invoke();
    }

    public void RemoveRoad(Vector3Int pos)
    {
        Vector2Int coordinates = new Vector2Int(pos.x, pos.z);
        if (!InRange(coordinates.x, coordinates.y))
            return;
        if (map[coordinates.x, coordinates.y] == 0)
        {
            //print("no road here ");
            return;
        }

        if (roadTiles.TryGetValue(coordinates, out RoadTile tile))
        {
            tile.Death();
            map[coordinates.x, coordinates.y] = 0;
            roadTiles.Remove(coordinates);
        }
    }

    public bool InRange(int x, int y)
    {
        if (x > gridSize.x || x < 0 || y > gridSize.y || y < 0)
            return false;

        return true;
    }

    void ShowDebug(Vector2Int coordinates)
    {
        if (InRange(coordinates.x, coordinates.y))
        {
            if (map[coordinates.x, coordinates.y] == 0)
                roadPreview.GetComponentInChildren<MeshRenderer>().material = freeMat;
            else if (map[coordinates.x, coordinates.y] == 1)
                roadPreview.GetComponentInChildren<MeshRenderer>().material = stuckMat;
        }
    }

    public void LoadMap(int[,] map)
    {
        this.map = map;
        roadTiles.Clear();
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                Vector3 worldPos = new Vector3(coordinates.x, 0, coordinates.y);
                GameObject newRoad = Instantiate(roadPrefab, worldPos, Quaternion.identity);
                RoadTile tile = newRoad.GetComponent<RoadTile>();
                tile.Create(coordinates);
                tile.SetMesh(CurrentTileMesh);
                roadTiles.Add(coordinates, tile);
                EventManager.Instance.onNewRoad.Invoke();
            }
        }
    }

    public void SetTile(Mesh newMesh)
    {
        CurrentTileMesh = newMesh;
        roadPreview.GetComponentInChildren<MeshFilter>().sharedMesh = newMesh;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Ray screenRay = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(screenRay, out RaycastHit hit))
        {
            Vector3 mousePos = hit.point;
            gridPos = new Vector3Int(Mathf.RoundToInt(mousePos.x), 0, Mathf.RoundToInt(mousePos.z));
            roadPreview.position = Vector3.Lerp(roadPreview.position, gridPos, 50f * Time.deltaTime);
            Vector2Int coordinates = new Vector2Int(gridPos.x, gridPos.z);
            if (Input.GetMouseButton(0))
                PlaceRoad(gridPos);
            else if (Input.GetMouseButton(1))
                RemoveRoad(gridPos);

            ShowDebug(coordinates);
        }
    }
}
