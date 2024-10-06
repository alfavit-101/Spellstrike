//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[RequireComponent(typeof(LineRenderer))]
//public class Laser : MonoBehaviour
//{
//    public LayerMask layerMask;          // Маска слоёв для объектов, с которыми будет взаимодействовать лазер
//    public float defaultLength = 50f;    // Максимальная длина лазера, если не столкнётся с объектом
//    public int NumOfReflections = 3;     // Количество возможных отражений лазера
//    public float minLength = 0.1f;        // Минимальная длина лазер

//    private LineRenderer _lineRenderer;  // Ссылка на компонент LineRenderer для визуализации лазера
//    private Camera _camera;              // Ссылка на основную камеру
//    private RaycastHit2D hit;            // Информация о столкновении луча с поверхностью

//    private void Start()
//    {
//        // Получаем компонент LineRenderer, прикреплённый к объекту
//        _lineRenderer = GetComponent<LineRenderer>();
//        // Получаем основную камеру сцены
//        _camera = Camera.main;
//    }

//    private void Update()
//    {
//        // Вызываем метод для обновления лазера с отражениями каждый кадр
//        ReflectLaser();
//        Debug.DrawRay(hit.point, hit.normal, Color.red, 1f);
//    }

//    // Метод для расчёта и отрисовки лазера с отражениями
//    void ReflectLaser()
//    {
//        // Получаем позицию мыши в мировом пространстве
//        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
//        mouseWorldPosition.z = 0;  // Обнуляем Z координату для 2D пространства

//        // Вычисляем направление от объекта (пушки) к курсору мыши
//        Vector2 direction = (mouseWorldPosition - transform.position).normalized;

//        // Создаём первый луч из текущей позиции объекта в направлении мыши
//        Vector2 rayOrigin = transform.position;
//        _lineRenderer.positionCount = 1;
//        _lineRenderer.SetPosition(0, transform.position); // Первая точка — текущая позиция объекта

//        float remainLength = defaultLength;  // Оставшаяся длина лазера

//        // Цикл для обработки отражений лазера
//        for (int i = 0; i < NumOfReflections; i++)
//        {
//            // Выполняем Raycast для 2D (Physics2D.Raycast), проверяя столкновение луча с объектом
//            hit = Physics2D.Raycast(rayOrigin, direction, remainLength, layerMask);

//            if (hit.collider != null)  // Если луч столкнулся с объектом
//            {
//                // Добавляем новую точку в LineRenderer на месте столкновения луча
//                _lineRenderer.positionCount += 1;
//                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);

//                // Вычитаем пройденное расстояние из оставшейся длины лазера
//                remainLength -= Vector2.Distance(rayOrigin, hit.point);

//                // Проверяем, не стало ли оставшееся расстояние меньше минимальной длины
//                remainLength = Mathf.Max(remainLength, minLength); // Устанавливаем минимальную длину

//                // Создаём новый луч, который будет отражаться от поверхности на основе нормали столкновения
//                rayOrigin = hit.point;
//                direction = Vector2.Reflect(direction, hit.normal);
//            }
//            else  // Если столкновения не произошло
//            {
//                // Добавляем последнюю точку на конце оставшейся длины луча
//                _lineRenderer.positionCount += 1;
//                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, rayOrigin + direction * remainLength);
//                break;  // Прерываем цикл, так как луч достиг максимальной длины
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
        // Получаем позицию мыши в мировом пространстве
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;  // Обнуляем Z координату для 2D пространства

        // Вычисляем направление от объекта (пушки) к курсору мыши
        Vector3 direction = (mouseWorldPosition - transform.position).normalized;

        // Создаём первый луч из текущей позиции объекта в направлении мыши
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
