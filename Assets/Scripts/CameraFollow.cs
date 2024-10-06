using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // ������ �� ������ ������ (��� Transform)
    public Vector3 offset;    // �������� ������ ������������ ������

    private void Start()
    {
        // ���� �������� �� ������� � ����������, ����� ������ ��� �� ���������
        if (offset == Vector3.zero)
        {
            offset = new Vector3(0, 0, -10);  // ������� �������� ��� 2D ��� (������ ������ ������ �� ��� Z)
        }
    }

    private void LateUpdate()  // LateUpdate, ����� ������ ��������� ����� ���� ��������� ������
    {
        if (player != null)
        {
            // ������ ������ �� �������, �������� ��������
            transform.position = player.position + offset;
        }
    }
}

