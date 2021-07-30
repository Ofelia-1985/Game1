using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public float TopY;
    public float DownY;
    public float speed;
    
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        float PosAuxY = gameObject.transform.position.y + speed;
        gameObject.transform.position = new Vector3 (gameObject.transform.position.x,
                                                     PosAuxY,
                                                     gameObject.transform.position.z);

        if(PosAuxY > TopY || PosAuxY < DownY)
        {
            speed*=-1;
        }     
    }
}
