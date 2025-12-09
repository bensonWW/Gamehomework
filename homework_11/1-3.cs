using UnityEngine;

public class createTowl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i <= 20; i++)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.SetParent(this.plane.transform);
            obj.transform.localPosition = new Vector3(0, hight, 0);
            obj.transform.localScale = new Vector3(1, 0.25f, 1);
            obj.transform.Rotate(0, rotate, 0);
            obj.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
            hight += 0.25f;
            rotate += 3.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    [SerializeField] private GameObject plane;
    private float rotate = 0;
    private float hight = 0;
}
