using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField]
    private GameObject Arrow_L;
    [SerializeField]
    private GameObject Arrow_R;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private SpriteRenderer mapRenderer;
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;
    private Vector3 dragOrigin;

    private void Awake()
    {
        //mapRenderer = GetComponent<SpriteRenderer>();  // 追加：mapRendererを取得
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;
        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
    }

    private void Update()
    {
        PanCamera();
    }

    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            difference.y = 0;  // 上下にしかドラッグできないように変更

            cam.transform.position = ClampCamera(cam.transform.position + difference);

        }
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;
        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);


        if (targetPosition.x == minX || targetPosition.x == maxX || targetPosition.y == minY || targetPosition.y == maxY)
        {
            // 範囲に丁度にきた時の処理（関数Aの実行など）
            FunctionA();
        }

        return new Vector3(newX, newY, targetPosition.z);
    }
}
private void PanCamera()
{
    if (Input.GetMouseButtonDown(0))
        dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

    if (Input.GetMouseButton(0))
    {
        Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
        difference.y = 0;  // 上下にしかドラッグできないように変更

        cam.transform.position = ClampCamera(cam.transform.position + difference);

    }
}

private Vector3 ClampCamera(Vector3 targetPosition)
{
    float camHeight = cam.orthographicSize;
    float camWidth = cam.orthographicSize * cam.aspect;

    float minX = mapMinX + camWidth;
    float maxX = mapMaxX - camWidth;
    float minY = mapMinY + camHeight;
    float maxY = mapMaxY - camHeight;
    float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
    float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

    return new Vector3(newX, newY, targetPosition.z);
}
}


