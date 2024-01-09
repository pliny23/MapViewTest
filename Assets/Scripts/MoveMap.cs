using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private float spriteWidth;

    void Start()
    {
        // スプライトの幅を取得
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void OnMouseDown()
    {
        if (!isDragging)
        {
            // ドラッグ開始時にマウスの座標とスプライトの座標の差分を保存
            //offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

            isDragging = true;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            // ドラッグ中はマウスの座標に合わせてスプライトを移動
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;

            // 右端の制限
            float maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - spriteWidth / 2;
            newPosition.x = Mathf.Clamp(newPosition.x, -maxX, maxX);

            // 左端の制限
            float minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + spriteWidth / 2;
            newPosition.x = Mathf.Clamp(newPosition.x, minX, -minX);

            transform.position = newPosition;
        }
    }
}
