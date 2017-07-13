using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableTile : MonoBehaviour
{
    private MovableTile topTile;
    private MovableTile rightTile;
    private MovableTile leftTile;
    private MovableTile bottomTile;

    public MovableTile TopTile
    {
        get
        {
            return topTile;
        }

        set
        {
            topTile = value;
        }
    }

    public MovableTile RightTile
    {
        get
        {
            return rightTile;
        }

        set
        {
            rightTile = value;
        }
    }

    public MovableTile LeftTile
    {
        get
        {
            return leftTile;
        }

        set
        {
            leftTile = value;
        }
    }

    public MovableTile BottomTile
    {
        get
        {
            return bottomTile;
        }

        set
        {
            bottomTile = value;
        }
    }

}
