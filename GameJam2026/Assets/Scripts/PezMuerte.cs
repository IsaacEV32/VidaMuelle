//using UnityEngine;

//public class PezMuerte : MonoBehaviour
//{
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    //public int VidaPez = 100;

//    //public int DaniodeAcido = 200;
//    bool isMoving = false;

//    public static bool Pezmuerto;
//    void Start()
//    {
//        Pezmuerto = false;

//    }

//    // Update is called once per frame
    
//    public void muerteinstantanea()
//    {
//        Vector4 posDeAci=transform.position;
//        posDeAci.y = Mathf.Clamp(posDeAci.y, -2.5f, 6f);
//        transform.position = posDeAci;
//        if (isMoving)
//        {
//            //Se movera dependiendo del input dado
//            posDeAci += movement * speed * Time.deltaTime;
//            nuevaPos += Vector3.right * speed * Time.deltaTime;
//        }
//        else
//        {
//            nuevaPos += Vector3.right * speed * Time.deltaTime;
//        }
//    }
//}
