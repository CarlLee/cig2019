using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriGrid : MonoBehaviour
{
    public GameObject cellPrefab;
    public float cellSize = 2.56f;
    public Vector2Int gridSize;

    [SerializeField]
    public TriBlock[] blocks;

    TriCell[,] triCells;
    void Start()
    {
        triCells = new TriCell[blocks.Length, 16];
        float blockSize = cellSize * 4;
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                Vector2 origin = new Vector2(x, y) * blockSize;
                origin = CoordsUtils.OrthoToSlope(origin);
                GameObject blockObj = new GameObject();
                blockObj.name = "block" + x + "," + y;
                blockObj.transform.SetParent(transform, false);
                blockObj.transform.localPosition = origin;
                blockObj.hideFlags = HideFlags.DontSave;
                int index = y * gridSize.x + x;
                TriBlock block = blocks[index];
                for (int i = 0; i < 16; i++)
                {
                    int ix = i % 4;
                    int iy = i / 4;
                    uint cellMask = block.mask >> (i * 2) & 3;
                    GameObject cell = Instantiate<GameObject>(cellPrefab);
                    cell.name = "cell" + i;
                    cell.transform.SetParent(blockObj.transform, false);
                    var cellPos = CoordsUtils.OrthoToSlope(new Vector2(ix, iy) * cellSize);
                    cell.transform.localPosition = cellPos;
                    var triCell = cell.GetComponent<TriCell>();
                    triCell.UpdatePresentation(cellMask);
                    triCells[index,i] = triCell;
                }
            }
        }
        //transform.localPosition = new Vector2(-gridSize.x / 2f * blockSize, -gridSize.y / 2f * blockSize);
    }

    void Update()
    {
        float blockSize = cellSize * 4;
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                Vector2 origin = new Vector2(x, y) * blockSize;
                int index = y * gridSize.x + x;
                TriBlock block = blocks[index];
                for (int i = 0; i < 16; i++)
                {
                    int ix = i % 4;
                    int iy = i / 4;
                    uint cellMask = block.mask >> (i * 2) & 3;
                    var triCell = triCells[index, i];
                    triCell.UpdatePresentation(cellMask);
                    triCells[index, i] = triCell;
                }
            }
        }
    }

    public bool CanFit(int offsetX, int offsetY, TriBlock toFit)
    {
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                int index = y * gridSize.x + x;
                TriBlock block = blocks[index];
                int blockOffsetX = offsetX - x * 4;
                int blockOffsetY = offsetY - y * 4;
                if(!block.CanFit(toFit.Shift(blockOffsetX, blockOffsetY)))
                {
                    Debug.Log(toFit);
                    Debug.Log(toFit.Shift(blockOffsetX, blockOffsetY));
                    Debug.Log("index: " + index + " offset: " + new Vector2Int(blockOffsetX, blockOffsetY));
                    return false;
                }
            }
        }
        return true;
    }
}
