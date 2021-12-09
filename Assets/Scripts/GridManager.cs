using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    int[,] map;
    [SerializeField] GameObject roadPrefab;
    [SerializeField] Vector2Int gridSize;
    Camera mainCam;
    [SerializeField] Vector3Int gridPos;
    [SerializeField] Transform debug;

    private void Awake()
    {
        map = new int[gridSize.x, gridSize.y];
        mainCam = Camera.main;
    }

    public void PlaceRoad(Vector3Int pos)
    {
        Instantiate(roadPrefab, pos, Quaternion.identity);
        Vector2Int coordinates = new Vector2Int(System.Math.Abs(pos.x), System.Math.Abs(pos.z));
        print(coordinates);
        map[coordinates.x, coordinates.y] = 1;
    }

    private void Update()
    {
        Ray screenRay = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(screenRay, out RaycastHit hit))
        {
            Vector3 mousePos = hit.point;
            gridPos = new Vector3Int(Mathf.RoundToInt(mousePos.x), 0, Mathf.RoundToInt(mousePos.z));
            debug.position = Vector3.Lerp(debug.position, gridPos, 10f * Time.deltaTime);
            if (Input.GetMouseButtonDown(0))
                PlaceRoad(gridPos);
        }
    }
}
