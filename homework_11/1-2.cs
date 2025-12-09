using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class createRandom : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject createRandomobj()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                return GameObject.CreatePrimitive(PrimitiveType.Cube);
            case 1:
                return GameObject.CreatePrimitive(PrimitiveType.Sphere);
            case 2:
                return GameObject.CreatePrimitive(PrimitiveType.Capsule);
            case 3:
                return GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            default:
                return GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
    }
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject Object = createRandomobj();

                Object.transform.SetParent(this.plane.transform);
                Object.transform.localPosition = new Vector3(i - 4.5f, 0.5f, j - 4.5f);
                Object.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
                Object.GetComponent<Renderer>().material.color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    [SerializeField] private GameObject plane;
}
