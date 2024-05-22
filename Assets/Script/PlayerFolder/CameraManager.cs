using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SingleTon<CameraManager>
{

    public Transform target;
    public float speed;
    public Vector2 center;
    public Vector2 size;
    float height;
    float width;

    public Dictionary<int, float[]> centerXY = new Dictionary<int, float[]>();
    public Dictionary<int, float[]> sizeXY = new Dictionary<int, float[]>();

    void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
        GenerateData();
        center.x = 0;
        center.y = 0;
        size.x = 33.4f;
        size.y = 10.8f;
    }

    private void GenerateData()
    {
        centerXY.Add(1, new float[] { 0, 0 });
        centerXY.Add(2, new float[] { 52.95f, -0.29f });
        centerXY.Add(3, new float[] { 109.5f, -0.45f });
        sizeXY.Add(1, new float[] { 33.4f, 10.8f });
        sizeXY.Add(2, new float[] { 66.3f, 11.2f });
        sizeXY.Add(3, new float[] { 40, 11 });
    }

    public void cameraXYSetting(int key, int index)
    {
        center.x = centerXY[key][index - 1];
        center.y = centerXY[key][index];
        size.x = sizeXY[key][index - 1];
        size.y = sizeXY[key][index];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);

        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}

