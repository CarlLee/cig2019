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
            if (puzzlePiece != null)
            {
                if(!puzzlePiece.canMove)
                {
                    var go = Instantiate(puzzlePiece.gameObject);
                    go.transform.position = puzzlePiece.transform.position;
                    puzzlePiece = go.GetComponent<PuzzlePiece>();
                    puzzlePiece.canMove = true;
                }
                offset = puzzlePiece.transform.position - mousePos;
                originalPos = puzzlePiece.transform.position;
                ItemSlots.Instance.DescreaseCount(puzzlePiece.id);
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
            if (puzzlePiece != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, LayerMask.GetMask("PuzzleBoard"));
                var hitTarget = hit.collider?.gameObject;
                var grid = hitTarget?.GetComponent<TriGrid>();
                var puzzleBoard = hitTarget?.GetComponent<PuzzleBoard>();
                if (grid != null && puzzleBoard != null)
                {
                    Vector2 relativePos = new Vector2(puzzlePiece.transform.position.x, puzzlePiece.transform.position.y) - new Vector2(grid.transform.position.x, grid.transform.position.y);
                    Vector2 orthoPos = CoordsUtils.SlopeToOrtho(relativePos) / grid.cellSize;
                    Vector2Int gridOffset = new Vector2Int(Mathf.RoundToInt(orthoPos.x), Mathf.RoundToInt(orthoPos.y));
                    bool canFit = grid.CanFit(gridOffset.x, gridOffset.y, puzzlePiece.shape);
                    bool isOutOfBound = grid.IsOutOfBound(gridOffset.x, gridOffset.y);
                    Debug.Log("canFit: " + canFit + ", isOutOfBound: " + isOutOfBound);
                    if (canFit && !isOutOfBound)
                    {
                        puzzleBoard.Attach(gridOffset.x, gridOffset.y, puzzlePiece.shape);
                    }
                }
                Destroy(puzzlePiece.gameObject);
            }
        }
    }
}
