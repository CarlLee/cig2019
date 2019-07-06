using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public GameObject cellPrefab;
    public float cellSize = 2.56f;
    public TriBlock shape;
    public PuzzleBoard puzzleBoard;
    // Start is called before the first frame update
    void Start()
    {
        GameObject grid = new GameObject();
        grid.name = "grid";
        var triGrid = grid.AddComponent<TriGrid>();
        triGrid.gridSize = new Vector2Int(1, 1);
        triGrid.blocks = new TriBlock[]{ shape };
        triGrid.cellPrefab = cellPrefab;
        triGrid.cellSize = cellSize;
        grid.transform.SetParent(transform, false);
        grid.transform.localPosition = Vector3.zero;
        this.gameObject.layer = LayerMask.NameToLayer("PuzzlePiece");
        
        var collider = gameObject.AddComponent<BoxCollider2D>();
        float blockSize = cellSize * 4f;
        var colliderSize = CoordsUtils.OrthoToSlope(new Vector2(blockSize, blockSize));
        collider.size = colliderSize;
        collider.offset = colliderSize / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
