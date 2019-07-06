using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/*
 * 12 13 14 15 
 * 8  9  10 11
 * 4  5  6  7
 * 0  1  2  3
 */

[System.Serializable]
public struct TriBlock
{
    public uint mask;

    public TriBlock AddBlock(int offsetX, int offsetY, TriBlock block)
    {
        TriBlock result = new TriBlock();
        result.mask = this.mask;

        TriBlock shifted = block.Shift(offsetX, offsetY);
        result.mask |= shifted.mask;
        return result;
    }

    public TriBlock Shift(int offsetX, int offsetY)
    {
        TriBlock result = new TriBlock();
        bool left = offsetX < 0;
        bool down = offsetY < 0;
        offsetX = System.Math.Abs(offsetX) * 2;
        offsetY = System.Math.Abs(offsetY);
        result.mask = this.mask;

        if(offsetY >= 4)
        {
            result.mask = 0;
            return result;
        }

        // Y offset
        if (down)
        {
            result.mask = result.mask << (offsetY * 8);
        }
        else
        {
            result.mask = result.mask >> (offsetY * 8);
        }

        // X offset
        uint row0 = result.mask & 255;
        uint row1 = (result.mask >> 8) & 255;
        uint row2 = (result.mask >> 16) & 255;
        uint row3 = (result.mask >> 24) & 255;
        if (left)
        {
            row0 = (row0 << offsetX) & 255;
            row1 = (row1 << offsetX) & 255;
            row2 = (row2 << offsetX) & 255;
            row3 = (row3 << offsetX) & 255;
        }
        else
        {
            row0 = row0 >> offsetX;
            row1 = row1 >> offsetX;
            row2 = row2 >> offsetX;
            row3 = row3 >> offsetX;
        }

        result.mask = row0 | (row1 << 8) | (row2 << 16) | (row3 << 24);
        return result;
    }

    public bool CanFit(TriBlock block)
    {
        var inverted = ~this.mask;
        return (inverted | block.mask) == inverted;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(System.Convert.ToString(this.mask >> 24 & 255, 2).PadLeft(8, '0'));
        sb.Append("\n");
        sb.Append(System.Convert.ToString(this.mask >> 16 & 255, 2).PadLeft(8, '0'));
        sb.Append("\n");
        sb.Append(System.Convert.ToString(this.mask >> 8 & 255, 2).PadLeft(8, '0'));
        sb.Append("\n");
        sb.Append(System.Convert.ToString(this.mask & 255, 2).PadLeft(8, '0'));
        sb.Append("\n");
        return sb.ToString();
    }
}
