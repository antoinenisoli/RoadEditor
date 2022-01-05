using UnityEngine;

public enum BlockColors { blank, red, blue, green, yellow, cyan, white, purple };

[System.Serializable]
public class Level
{
#if UNITY_EDITOR
    [HideInInspector] public bool showBoard;
#endif

    public int rows = 9;
    public int columns = 9;
    public BlockColors[,] board = new BlockColors[2, 2];
}


public class Levels : MonoBehaviour
{
    public Level[] allLevels = new Level[2];
}
