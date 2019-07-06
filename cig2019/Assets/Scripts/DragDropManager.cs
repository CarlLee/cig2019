using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropManager : MonoBehaviour
{
    public new Camera camera;
    
    Vector3 offset;
    Vector3 originalPos;
    PuzzlePiece puzzlePiece;
    void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, LayerMask.GetMask("PuzzlePiece"));
            var hitTarget = hit.collider?.gameObject;
            puzzlePiece = hitTarget?.GetComponent<PuzzlePiece>();
            if(puzzlePiece != null)
            {
                offset = puzzlePiece.transform.position - mousePos;
                originalPos = puzzlePiece.transform.position;
            }
        }

        if (Input.GetButton("Fire1"))
        {
            if (puzzlePiece != null)
            {
                puzzlePiece.transform.position = mousePos + offset;
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, LayerMask.GetMask("PuzzleBoard"));
            var hitTarget = hit.collider?.gameObject;
            var grid = hitTarget?.GetComponent<TriGrid>();
            var puzzleBoard = hitTarget?.GetComponent<PuzzleBoard>();
            if(grid != null && puzzleBoard != null && puzzlePiece != null)
            {
                Vector2 relativePos = new Vector2(puzzlePiece.transform.position.x, puzzlePiece.transform.position.y) - new Vector2(grid.transform.position.x, grid.transform.position.y);
                Vector2 orthoPos = CoordsUtils.SlopeToOrtho(relativePos) / grid.cellSize;
                Vector2Int gridOffset = new Vector2Int(Mathf.RoundToInt(orthoPos.x), Mathf.RoundToInt(orthoPos.y));
                bool canFit = grid.CanFit(gridOffset.x, gridOffset.y, puzzlePiece.shape);
                bool isOutOfBound = grid.IsOutOfBound(gridOffset.x, gridOffset.y);
                if (canFit && !isOutOfBound)
                {
                    Vector2 snappedPos = CoordsUtils.OrthoToSlope(new Vector2(gridOffset.x * grid.cellSize, gridOffset.y * grid.cellSize));
                    puzzlePiece.transform.position = snappedPos;
                }
                return;
            }
            puzzlePiece.transform.position = originalPos;
        }
    }
}
