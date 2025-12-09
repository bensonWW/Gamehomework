using UnityEngine;

public class controller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject shere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                shere.transform.SetParent(this.plane.transform);
                shere.transform.localPosition = new Vector3(i - 4.5f, 0.5f, j - 4.5f);
                shere.transform.localScale = new Vector3(shere.transform.localScale.x * 10, shere.transform.localScale.y * 10, shere.transform.localScale.z * 10);
                shere.GetComponent<Renderer>().material.color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    [SerializeField] private GameObject plane;
}
