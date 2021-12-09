using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject selectionButton;
    [SerializeField] Transform content;
    [SerializeField] GameObject[] tilePrefabs;

    private void Awake()
    {
        tilePrefabs.Reverse();
        for (int i = 0; i < tilePrefabs.Length; i++)
        {
            GameObject g = Instantiate(selectionButton, content);
            RoadTileButton button = g.GetComponent<RoadTileButton>();
            button.roadPrefab = tilePrefabs[i];
            button.Init();
        }
    }
}
