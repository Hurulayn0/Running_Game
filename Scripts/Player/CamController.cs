using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset; //kamera ve oyuncu aras�ndaki alana offset denir
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()  //daha d�z bir hareket i�in lateupdate
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }


    }
}

