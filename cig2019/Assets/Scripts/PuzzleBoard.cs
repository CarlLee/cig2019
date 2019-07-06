using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBoard : MonoBehaviour
{
    void Start()
    {
        var triGrid = GetComponent<TriGrid>();
        var blockSize = triGrid.cellSize * 4;
        var gridSize = triGrid.gridSize;
        this.gameObject.layer = LayerMask.NameToLayer("PuzzleBoard");
        var collider = this.gameObject.AddComponent<BoxCollider2D>();
        var colliderSize = CoordsUtils.OrthoToSlope(new Vector2(blockSize * gridSize.x, blockSize * gridSize.y));
        collider.size = colliderSize;
        collider.offset = colliderSize / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
