using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropManager : MonoBehaviour
{
    public new Camera camera;
    public TriGrid grid;

    GameObject target;
    Vector3 offset;
    Vector3 originalPos;
    Medicine medicine;
    void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, LayerMask.GetMask("Medicine"));
            target = hit.collider?.gameObject;
            if(target != null)
            {
                offset = target.transform.position - mousePos;
                originalPos = target.transform.position;
                medicine = target.GetComponent<Medicine>();
            }
        }

        if(Input.GetButton("Fire1"))
        {
            if(target != null)
            {
                target.transform.position = mousePos + offset;
            }
        }

        if(Input.GetButtonUp("Fire1"))
        {
            Vector2 relativePos = new Vector2(target.transform.position.x, target.transform.position.y) - new Vector2(grid.transform.position.x, grid.transform.position.y);
            Vector2 orthoPos = CoordsUtils.SlopeToOrtho(relativePos) / grid.cellSize;
            Vector2Int gridOffset = new Vector2Int(Mathf.RoundToInt(orthoPos.x), Mathf.RoundToInt(orthoPos.y));
            bool canFit = grid.CanFit(gridOffset.x, gridOffset.y, medicine.shape);
            Debug.Log(canFit);
            //Vector2Int blockOffset = new Vector2Int(gridOffset.x / 4, gridOffset.y / 4);
            //Vector2Int blockCoords = new Vector2Int(gridOffset.x % 4, gridOffset.y % 4);
            //int blockIndex = blockOffset.y * grid.gridSize.x + blockOffset.x;
            //Debug.Log(string.Format("gridOffset: {2} blockOffset: {0}, blockCoords: {1}, blockIndex: {3}", blockOffset, blockCoords, gridOffset, blockIndex));
            //if (blockIndex >= 0 && blockIndex < grid.blocks.Length)
            //Debug.Log(grid.blocks[blockIndex].CanFit(medicine.shape.Shift(blockCoords.x, blockCoords.y)));
            if (target != null)
            {
                target.transform.position = originalPos;
            }
        }
    }
}
