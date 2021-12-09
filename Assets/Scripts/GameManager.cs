using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private int _tabWidth = 2000;
    [SerializeField]
    private int _tabHeight = 2000;

    UnityEvent my_event = new UnityEvent();
    private int _neightborhoodSize = 3;
    private Vector2Int[,] _neighboors;
    private bool[,] _isNeighboors;
    private int[,] _tileGrid;

    [SerializeField]
    private Mesh _neightboorTop;
    [SerializeField]
    private Mesh _neightboorTopBottomLeftRight;
    [SerializeField]
    private Mesh _neightboorBottomTopRight;
    [SerializeField]    
    private Mesh _neightboorTopDown;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _isNeighboors = new bool[_neightborhoodSize, _neightborhoodSize];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetCell(int x, int y)
    {
        return GridManager.Instance.map[x, y];
    }

    public int GetCell(Vector2Int pos)
    {
        return GridManager.Instance.map[pos.x, pos.y];
    }

    public void CheckNeighboors(Vector2Int coordinate)
    {
        ResetTab();
        int j;
        for (int i = 0; i <= _neightborhoodSize; i++)
        {
            for (j = 0; j <= _neightborhoodSize; j++)
            {
                if(i + coordinate.x - 1 > 0 && i + coordinate.x - 1 < GridManager.Instance.gridSize.x && j + coordinate.y - 1 > 0 && j + coordinate.y - 1 < GridManager.Instance.gridSize.y)
                    if (GridManager.Instance.map[i + coordinate.x - 1, j + coordinate.y - 1] == 1)
                        _isNeighboors[i, j] = true;
            }
        }
        bool top = _isNeighboors[0,1];
        bool down = _isNeighboors[2, 1];
        bool right = _isNeighboors[1, 2];
        bool left = _isNeighboors[1, 0];
        RoadTile roadTile = GridManager.Instance.roadTiles[coordinate];
        MeshFilter meshFilter = roadTile.GetComponentInChildren<MeshFilter>();
        if (top && !down && !right && !left)
        {
            meshFilter.mesh = _neightboorTop;
        }
        if (!top && down && !right && !left)
        {

            meshFilter.mesh = _neightboorTop;
            roadTile.visual.DORotate(new Vector3(0, 180, 0), 1f);
        }
        if (!top && !down && right && !left)
        {

            meshFilter.mesh = _neightboorTop;
            roadTile.visual.DORotate(new Vector3(0, 90, 0), 1f);
        }
        if (!top && !down && !right && left)
        {

            meshFilter.mesh = _neightboorTop;
            roadTile.visual.DORotate(new Vector3(0, -90, 0), 1f);
        }

    }
    private void InitiateNeighborhood()
    {
        _neighboors[0, 0] = new Vector2Int(-1, -1);
        _neighboors[0, 1] = new Vector2Int(-1, 0);
        _neighboors[0, 2] = new Vector2Int(-1, 1);
        _neighboors[1, 0] = new Vector2Int(0, -1);
        _neighboors[1, 1] = new Vector2Int(0, 0);
        _neighboors[1, 2] = new Vector2Int(0, 1);
        _neighboors[2, 0] = new Vector2Int(1, -1);
        _neighboors[2, 1] = new Vector2Int(1, 0);
        _neighboors[2, 2] = new Vector2Int(1, 1);
    }
    private void ResetTab()
    {
        int j;
        for (int i = 0; i < _neightborhoodSize; i++)
        {
            for (j = 0; j < _neightborhoodSize; j++)
            {
                _isNeighboors[i, j] = false;
            }
        }
    }
}
