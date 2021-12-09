using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private int _tabWidth = 2000;
    [SerializeField]
    private int _tabHeight = 2000;

    private int[,] _tileGrid;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _tileGrid = new int[_tabWidth, _tabHeight];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetCell(int x, int y)
    {
        return _tileGrid[x, y];
    }

    public int GetCell(Vector2Int pos)
    {
        return _tileGrid[pos.x, pos.y];
    }
}
