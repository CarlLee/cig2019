using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct AttachHistory
{
    public TriBlock block;
    public int offsetX;
    public int offsetY;
}

public class PuzzleBoard : MonoBehaviour
{
    public int puzzleId = 0;

    int prevPuzzleId;
    TriGrid triGrid;
    List<AttachHistory> attachHistories = new List<AttachHistory>();
    void Start()
    {
        triGrid = GetComponent<TriGrid>();
        var blockSize = triGrid.cellSize * 4;
        var gridSize = triGrid.gridSize;
        this.gameObject.layer = LayerMask.NameToLayer("PuzzleBoard");
        var collider = this.gameObject.AddComponent<BoxCollider2D>();
        var colliderSize = CoordsUtils.OrthoToSlope(new Vector2(blockSize * gridSize.x, blockSize * gridSize.y));
        collider.size = colliderSize * 2;
        collider.offset = colliderSize / 2f;
        LoadPuzzle(puzzleId);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadPuzzle(int puzzleId)
    {
        var data = Medicine.Instance.Shape[puzzleId];
        triGrid.gridSize = new Vector2Int(2, 2);
        triGrid.blocks = new TriBlock[]{
            new TriBlock { mask =  ~System.Convert.ToUInt32(data[2], 2)},
            new TriBlock { mask =  ~System.Convert.ToUInt32(data[3], 2)},
            new TriBlock { mask =  ~System.Convert.ToUInt32(data[4], 2)},
            new TriBlock { mask =  ~System.Convert.ToUInt32(data[5], 2)},
        };
        prevPuzzleId = puzzleId;
    }

    public void Attach(int x, int y, TriBlock shape)
    {
        attachHistories.Add(new AttachHistory()
        {
            offsetX = x,
            offsetY = y,
            block = shape
        });
        triGrid.Attach(x, y, shape);
    }

    public void ChangeBoard(int puzzleId)
    {
        LoadPuzzle(puzzleId);
        for(int i = 0; i < attachHistories.Count; i++)
        {
            var history = attachHistories[i];
            triGrid.Attach(history.offsetX, history.offsetY, history.block);
        }
    }
}
