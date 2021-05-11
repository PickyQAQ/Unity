using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGrounds : MonoBehaviour
{
    public Transform[] backGrouneds;
    public float fparallax = 0.4f;
    public float layerFraction = 5f;
    public float fSmooth = 5f;

    Transform cam;
    Vector3 previousCamPos;


    private void Awake()
    {
        cam = Camera.main.transform;
    }


    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        float fParallaxX = (previousCamPos.x - cam.position.x) * fparallax;
        for(int i = 0; i < backGrouneds.Length; i++)
        {
            float fNewX = backGrouneds[i].position.x + fParallaxX * (1 + i * layerFraction);
            Vector3 newPos = new Vector3(fNewX, backGrouneds[i].position.y, backGrouneds[i].position.z);
            backGrouneds[i].position = Vector3.Lerp(backGrouneds[i].position, newPos, fSmooth * Time.deltaTime);
        }

        previousCamPos = cam.position;  //将变化前的摄像机位置作为上一帧摄像机的位置
    }
}
