//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[RequireComponent(typeof(LineRenderer))]
//public class Laser : MonoBehaviour
//{
//    public LayerMask layerMask;          // ����� ���� ��� ��������, � �������� ����� ����������������� �����
//    public float defaultLength = 50f;    // ������������ ����� ������, ���� �� ��������� � ��������
//    public int NumOfReflections = 3;     // ���������� ��������� ��������� ������
//    public float minLength = 0.1f;        // ����������� ����� �����

//    private LineRenderer _lineRenderer;  // ������ �� ��������� LineRenderer ��� ������������ ������
//    private Camera _camera;              // ������ �� �������� ������
//    private RaycastHit2D hit;            // ���������� � ������������ ���� � ������������

//    private void Start()
//    {
//        // �������� ��������� LineRenderer, ������������ � �������
//        _lineRenderer = GetComponent<LineRenderer>();
//        // �������� �������� ������ �����
//        _camera = Camera.main;
//    }

//    private void Update()
//    {
//        // �������� ����� ��� ���������� ������ � ����������� ������ ����
//        ReflectLaser();
//        Debug.DrawRay(hit.point, hit.normal, Color.red, 1f);
//    }

//    // ����� ��� ������� � ��������� ������ � �����������
//    void ReflectLaser()
//    {
//        // �������� ������� ���� � ������� ������������
//        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
//        mouseWorldPosition.z = 0;  // �������� Z ���������� ��� 2D ������������

//        // ��������� ����������� �� ������� (�����) � ������� ����
//        Vector2 direction = (mouseWorldPosition - transform.position).normalized;

//        // ������ ������ ��� �� ������� ������� ������� � ����������� ����
//        Vector2 rayOrigin = transform.position;
//        _lineRenderer.positionCount = 1;
//        _lineRenderer.SetPosition(0, transform.position); // ������ ����� � ������� ������� �������

//        float remainLength = defaultLength;  // ���������� ����� ������

//        // ���� ��� ��������� ��������� ������
//        for (int i = 0; i < NumOfReflections; i++)
//        {
//            // ��������� Raycast ��� 2D (Physics2D.Raycast), �������� ������������ ���� � ��������
//            hit = Physics2D.Raycast(rayOrigin, direction, remainLength, layerMask);

//            if (hit.collider != null)  // ���� ��� ���������� � ��������
//            {
//                // ��������� ����� ����� � LineRenderer �� ����� ������������ ����
//                _lineRenderer.positionCount += 1;
//                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);

//                // �������� ���������� ���������� �� ���������� ����� ������
//                remainLength -= Vector2.Distance(rayOrigin, hit.point);

//                // ���������, �� ����� �� ���������� ���������� ������ ����������� �����
//                remainLength = Mathf.Max(remainLength, minLength); // ������������� ����������� �����

//                // ������ ����� ���, ������� ����� ���������� �� ����������� �� ������ ������� ������������
//                rayOrigin = hit.point;
//                direction = Vector2.Reflect(direction, hit.normal);
//            }
//            else  // ���� ������������ �� ���������
//            {
//                // ��������� ��������� ����� �� ����� ���������� ����� ����
//                _lineRenderer.positionCount += 1;
//                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, rayOrigin + direction * remainLength);
//                break;  // ��������� ����, ��� ��� ��� ������ ������������ �����
//            }
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    public LayerMask layerMask;
    public float defaultLength = 50f;
    public int NumOfReflections = 3;

    private LineRenderer _lineRenderer;
    private Camera _camera;
    private RaycastHit hit;

    //Ray ray;
    //private Vector3 direction;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _camera = Camera.main;
    }

    private void Update()
    {
        ReflectLaser();
        //Debug.DrawRay(hit.point, hit.normal, Color.red, 1f);
    }

    void ReflectLaser()
    {
        //ray = new Ray(transform.position, transform.forward);
        // �������� ������� ���� � ������� ������������
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;  // �������� Z ���������� ��� 2D ������������

        // ��������� ����������� �� ������� (�����) � ������� ����
        Vector3 direction = (mouseWorldPosition - transform.position).normalized;

        // ������ ������ ��� �� ������� ������� ������� � ����������� ����
        Ray ray = new(transform.position, direction);

        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, transform.position);

        float remainLength = defaultLength;

        for (int i = 0; i < NumOfReflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, defaultLength, layerMask))
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);

                remainLength -= Vector3.Distance(ray.origin, hit.point);

                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
            }
            else
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, ray.origin + ray.direction * remainLength);
            }
        }
    }

    //void NormalLaser() 
    //{
    //    _lineRenderer.SetPosition(0, transform.position);

    //    if (Physics.Raycast(transform.position, transform.forward, out hit, defaultLength, layerMask))
    //    {
    //        _lineRenderer.SetPosition(1, hit.point);
    //    }
    //    else 
    //    {
    //        _lineRenderer.SetPosition(1, transform.position + (transform.forward * defaultLength));
    //    }
    //}
}
