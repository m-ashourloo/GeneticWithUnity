using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area
{
    SpriteRenderer sp;
    [HideInInspector] public float sx, ex;
    public GameObject wheel;

    public  Area(SpriteRenderer sp)
    {
        this.sp = sp;
        SetParameters();
    }
    private void SetParameters()
    {
        sx = sp.transform.position.x - sp.bounds.size.x / 2;
        ex = sp.transform.position.x + sp.bounds.size.x / 2;
    }
    public Vector3 GetWheelPosition()
    {
        var gh = UnityEngine.Random.Range(sx,ex);
        Vector3 pos = new Vector3(gh, sp.transform.position.y, 0f);
        return pos;

    }
}
