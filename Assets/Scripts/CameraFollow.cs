using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Ссылка на объект игрока (его Transform)
    public Vector3 offset;    // Смещение камеры относительно игрока

    private void Start()
    {
        // Если смещение не указано в инспекторе, можем задать его по умолчанию
        if (offset == Vector3.zero)
        {
            offset = new Vector3(0, 0, -10);  // Обычное смещение для 2D игр (камера позади игрока на оси Z)
        }
    }

    private void LateUpdate()  // LateUpdate, чтобы камера двигалась после всех изменений игрока
    {
        if (player != null)
        {
            // Камера следит за игроком, добавляя смещение
            transform.position = player.position + offset;
        }
    }
}

