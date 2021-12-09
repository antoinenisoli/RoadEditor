<<<<<<< Updated upstream
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public UnityEvent onNewRoad = new UnityEvent();

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    UnityEvent my_event = new UnityEvent();
    private int _neightborhoodSize = 3;
    private Vector2Int[,] _neighboors;

    private bool[,] _isNeighboors;
    // Start is called before the first frame update
    void Start()
    {
        my_event.AddListener(CheckNeighboors);
        _isNeighboors = new bool[_neightborhoodSize, _neightborhoodSize];
        _neighboors = new Vector2Int[_neightborhoodSize, _neightborhoodSize];
        InitiateNeighborhood();
    }

    private void CheckNeighboors()
    {
        ResetTab();
        int j;
        for (int i = 0; i < _neightborhoodSize; i++)
        {
            for (j = 0; j < _neightborhoodSize; j++)
            {
                if(GameManager.instance.GetCell(_neighboors[i, j]) == 1)
                    _isNeighboors[i,j] = true;
            }
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
>>>>>>> Stashed changes
