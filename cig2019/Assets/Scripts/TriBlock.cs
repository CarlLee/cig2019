using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 12 13 14 15 
 * 8  9  10 11
 * 4  5  6  7
 * 0  1  2  3
 */

public struct TriBlock
{
    private int mask;

    public int Mask
    {
        get
        {
            return mask;
        }

        set
        {
            mask = value;
        }
    }

    public TriBlock AddBlock(int offsetX, int offsetY, TriBlock block)
    {
        TriBlock result = new TriBlock();
        result.Mask = this.mask;

        TriBlock shifted = block.Shift(offsetX, offsetY);
        result.Mask |= shifted.Mask;
        return result;
    }

    public TriBlock Shift(int offsetX, int offsetY)
    {
        TriBlock result = new TriBlock();
        result.Mask = this.mask;
        // Y offset
        result.Mask = result.Mask >> (offsetY * 8);

        // X offset
        int row0 = result.Mask & 255;
        row0 = row0 >> offsetX;
        int row1 = result.Mask >> 8 & 255;
        row1 = row1 >> offsetX;
        int row2 = result.Mask >> 16 & 255;
        row2 = row2 >> offsetX;
        int row3 = result.Mask >> 24 & 255;
        row3 = row3 >> offsetX;

        result.Mask = row0 | (row1 << 8) | (row2 << 16) | (row3 << 24);
        return result;
    }
}
