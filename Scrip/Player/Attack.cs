using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
   public float delayDestroy;
   public float speed;

   private void OnEnable()
   {
       Destroy(gameObject, delayDestroy);
   }

   private void Update()//ande der e izq
   {
       transform.Translate(Vector2.right * speed * Time.deltaTime);
   }

   void OnTriggerEnter2D()  //para q el attack se destruya al tocar algo
   {
       Destroy(gameObject);
   }
}
