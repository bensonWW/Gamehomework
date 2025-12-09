using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class nameSopt : MonoBehaviour
{
    [SerializeField] private GameObject plane;

    void Start()
    {
        int width = 36;                         // 每列 36 個點
        int height = nameDots.Count / width;

        float spacing = 0.2f;                    // 點與點間距
        float dotSize = 0.2f;                    // dot 大小

        Vector3 planePos = plane.transform.position;

        // ★ 水平置中在 plane 上
        float originX = planePos.x - (width - 1) * spacing / 2f;

        // ★ 最下面一排貼在 plane 上方一點
        float bottomY = planePos.y + dotSize * 0.5f;

        for (int i = 0; i < nameDots.Count; i++)
        {
            int row = i / width;   // 0 = 最上面那排
            int col = i % width;   // 0 = 最左邊那排

            GameObject dot;

            if (nameDots[i] == 1)
            {
                dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                dot.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                dot = GameObject.CreatePrimitive(PrimitiveType.Cube);
                dot.GetComponent<Renderer>().material.color =
                    new Color(Random.value, Random.value, Random.value);
            }

            dot.transform.SetParent(plane.transform);

            // dot 變小一點
            dot.transform.localScale = Vector3.one * dotSize;

            // 物理
            Rigidbody rb = dot.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.mass = 1f;
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            // ★ 把 row 轉成「從下往上」的 index
            int rowFromBottom = height - 1 - row;

            // ★ 計算座標：X 左到右，Y 從下往上
            float x = originX + col * spacing;
            float y = bottomY + rowFromBottom * spacing;
            float z = planePos.z;

            Vector3 pos = new Vector3(x, y, z);

            // 紅色字稍微浮出牆面
            if (nameDots[i] == 1)
                pos.z -= 0.1f;

            dot.transform.position = pos;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 爆炸中心（你可以調整位置）
            Vector3 center = plane.transform.position + Vector3.up * 1f;

            float explosionForce = 20f;   // 爆炸力道
            float radius = 10f;           // 影響半徑
            float upwards = 0.1f;           // 往上推的力量

            // 找出 plane 底下所有 Rigidbody
            Rigidbody[] rbs = plane.GetComponentsInChildren<Rigidbody>();

            foreach (var rb in rbs)
            {
                if (rb == null) continue;

                // 確保是可動的
                rb.isKinematic = false;
                rb.useGravity = true;

                // 加爆炸力
                rb.AddExplosionForce(explosionForce, center, radius, upwards, ForceMode.Impulse);
            }
        }
    }

    private List<int> nameDots = new List<int>{
        0,0,0,1,0,0,0,1,1,0,0,0, 1,0,0,0,1,1,0,1,1,0,0,0, 0,0,0,1,1,0,0,1,0,0,0,0,
        0,0,1,0,0,0,0,1,0,1,0,0, 1,1,0,0,1,1,0,1,1,0,0,0, 0,0,1,1,0,0,1,1,1,1,1,0,
        1,1,1,1,1,1,0,1,0,0,1,0, 0,1,1,0,1,1,0,1,1,0,0,1, 0,1,1,1,0,0,0,1,0,0,1,0,
        0,1,0,0,1,0,0,1,0,1,0,0, 0,0,1,1,1,1,0,1,1,0,1,1, 0,1,1,1,0,0,0,1,0,0,1,0,
        0,1,0,0,1,0,0,1,1,0,0,0, 0,0,0,1,1,1,0,1,1,1,1,0, 0,0,1,1,0,1,1,1,1,1,1,1,
        0,1,0,0,1,0,0,1,0,1,0,0, 0,0,0,0,1,1,0,1,1,0,0,0, 0,0,1,1,0,0,0,0,1,0,0,0,
        0,1,0,0,1,0,0,1,0,0,1,0, 0,0,0,1,1,1,0,1,1,1,0,0, 0,0,1,1,0,0,1,1,1,1,1,1,
        0,1,0,0,1,0,0,1,0,1,0,0, 0,0,1,1,1,1,0,1,1,1,1,0, 0,0,1,1,0,0,1,0,0,0,0,1,
        0,1,0,0,1,0,0,1,1,0,0,0, 0,1,1,0,1,1,0,1,1,0,1,1, 0,0,1,1,0,0,1,1,1,1,1,1,
        0,1,0,0,1,0,0,1,0,0,0,0, 1,1,0,1,1,0,0,0,1,1,0,1, 0,0,1,1,0,0,0,0,1,0,0,0,
        0,1,0,0,1,0,0,1,0,0,0,0, 0,0,1,1,0,0,0,0,0,1,1,0, 0,0,1,1,0,1,1,1,1,1,1,1,
        1,1,1,1,1,1,0,1,0,0,0,0, 0,1,1,0,0,0,0,0,0,0,1,1, 0,0,1,1,0,0,0,0,1,0,0,0
    };
}
