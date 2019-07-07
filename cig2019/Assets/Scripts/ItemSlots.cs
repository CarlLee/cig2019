using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlots : MonoBehaviour
{
    public static ItemSlots Instance;
    public int[] itemIds;
    public GameObject ItemTemplate;

    int[] items = new int[100];
    public void InscreaseCount(int id)
    {
        items[id] = items[id] + 1;
    }

    public void DescreaseCount(int id)
    {
        items[id] = System.Math.Max(0, items[id] - 1);
    }

    public int GetCount(int id)
    {
        return items[id];
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for(int i = 0; i < itemIds.Length; i++)
        {
            var id = itemIds[i];
            var data = Medicine.Instance.Data[id];
            var item = Instantiate(ItemTemplate);
            var puzzlePiece = item.GetComponent<PuzzlePiece>();
            puzzlePiece.id = int.Parse(data[0]);
            puzzlePiece.name = data[1];
            puzzlePiece.shape.mask = System.Convert.ToUInt32(data[2], 2);
            item.transform.SetParent(transform, false);
            item.transform.localPosition = new Vector2((puzzlePiece.cellSize * 5) * i, 0);
        }
    }

    void Update()
    {
        
    }
}
