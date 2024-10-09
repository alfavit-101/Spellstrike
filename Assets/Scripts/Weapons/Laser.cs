using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    public LayerMask layerMask;          // Маска слоёв для объектов, с которыми будет взаимодействовать лазер
    public float defaultLength = 50f;    // Максимальная длина лазера, если не столкнётся с объектом
    public int NumOfReflections = 3;     // Количество возможных отражений лазера
    public float minLength = 0.1f;       // Минимальная длина лазера
    public float collisionOffset = 0.01f; // Небольшое смещение для предотвращения зацикливания

    private LineRenderer _lineRenderer;  // Ссылка на компонент LineRenderer для визуализации лазера
    private Camera _camera;              // Ссылка на основную камеру
    private RaycastHit2D hit;            // Информация о столкновении луча с поверхностью

    private void Start()
    {
        // Получаем компонент LineRenderer, прикреплённый к объекту
        _lineRenderer = GetComponent<LineRenderer>();
        // Получаем основную камеру сцены
        _camera = Camera.main;
    }

    private void Update()
    {
        // Вызываем метод для обновления лазера с отражениями каждый кадр
        ReflectLaser();

        // Отображаем нормаль на месте столкновения луча
        if (hit.collider != null)
        {
            Debug.DrawRay(hit.point, hit.normal, Color.red, 1f); // Визуализация нормали
        }
    }

    // Метод для расчёта и отрисовки лазера с отражениями
    void ReflectLaser()
    {
        // Получаем позицию мыши в мировом пространстве
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;  // Обнуляем Z координату для 2D пространства

        // Вычисляем направление от объекта (пушки) к курсору мыши
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;

        // Создаём первый луч из текущей позиции объекта в направлении мыши
        Vector2 rayOrigin = transform.position;
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, transform.position); // Первая точка — текущая позиция объекта

        float remainLength = defaultLength;  // Оставшаяся длина лазера

        // Цикл для обработки отражений лазера
        for (int i = 0; i < NumOfReflections; i++)
        {
            // Выполняем Raycast для 2D (Physics2D.Raycast), проверяя столкновение луча с объектом
            hit = Physics2D.Raycast(rayOrigin, direction, remainLength, layerMask);

            if (hit.collider != null)  // Если луч столкнулся с объектом
            {
                // Добавляем новую точку в LineRenderer на месте столкновения луча
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);

                // Вычитаем пройденное расстояние из оставшейся длины лазера
                remainLength -= Vector2.Distance(rayOrigin, hit.point);

                // Устанавливаем минимальную длину, если осталось слишком мало
                remainLength = Mathf.Max(remainLength, minLength);

                // Добавляем смещение от точки столкновения для предотвращения зацикливания
                rayOrigin = hit.point + hit.normal * collisionOffset;

                // Отражаем луч от поверхности, используя нормаль
                direction = Vector2.Reflect(direction, hit.normal);
            }
            else  // Если столкновения не произошло
            {
                // Добавляем последнюю точку на конце оставшейся длины луча
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, rayOrigin + direction * remainLength);
                break;  // Прерываем цикл, так как луч достиг максимальной длины
            }
        }
    }
}




//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[RequireComponent(typeof(LineRenderer))]
//public class Laser : MonoBehaviour
//{
//    public LayerMask layerMask; // Маска слоя, на который будет происходить облучение
//    public float defaultLength = 5000f; // Дефолтная длина лазера
//    public int NumOfReflections = 3; // Количество отражений

//    private LineRenderer _lineRenderer; // Компонент LineRenderer для визуализации лазера
//    private Camera _camera; // Основная камера для получения координат мыши

//    private void Start()
//    {
//        // Инициализация компонентов
//        _lineRenderer = GetComponent<LineRenderer>();
//        _camera = Camera.main;
//    }

//    private void Update()
//    {
//        ReflectLaser(); // Вызываем метод отражения лазера каждый кадр
//    }

//    //void ReflectLaser()
//    // {
//    //     // Получаем позицию мыши в мировом пространстве
//    //     Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
//    //     mouseWorldPosition.z = 0;  // Обнуляем Z координату для 2D пространства

//    //     // Вычисляем направление от объекта (пушки) к курсору мыши
//    //     Vector2 direction = (mouseWorldPosition - transform.position).normalized;

//    //     // Создаём первый луч из текущей позиции объекта в направлении мыши
//    //     Ray2D ray = new Ray2D(transform.position, direction);

//    //     _lineRenderer.positionCount = 1; // Сбрасываем количество позиций
//    //     _lineRenderer.SetPosition(0, transform.position); // Устанавливаем первую позицию лазера

//    //     float remainLength = defaultLength; // Оставшаяся длина лазера

//    //     for (int i = 0; i < NumOfReflections; i++)
//    //     {
//    //         RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, remainLength, layerMask); // Выполняем 2D Raycast

//    //         if (hit)
//    //         {
//    //             // Если луч столкнулся с объектом
//    //             _lineRenderer.positionCount += 1; // Увеличиваем количество позиций
//    //             _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point); // Устанавливаем позицию удара

//    //             remainLength -= Vector2.Distance(ray.origin, hit.point); // Уменьшаем оставшуюся длину

//    //             // Обновляем луч, отражая его от поверхности
//    //             ray = new Ray2D(hit.point, Vector2.Reflect(ray.direction, hit.normal));
//    //         }
//    //         else
//    //         {
//    //             // Если луч не столкнулся с объектом, просто продолжаем в текущем направлении
//    //             _lineRenderer.positionCount += 1; // Увеличиваем количество позиций
//    //             _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, ray.origin + ray.direction * remainLength); // Устанавливаем последнюю позицию
//    //             break; // Завершаем цикл, так как дальше нет столкновений
//    //         }
//    //     }
//    // }

//    void ReflectLaser()
//    {
//        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
//        mouseWorldPosition.z = 0;

//        Vector2 direction = (mouseWorldPosition - transform.position).normalized;
//        Ray2D ray = new Ray2D(transform.position, direction);

//        _lineRenderer.positionCount = 1;
//        _lineRenderer.SetPosition(0, transform.position);

//        float remainLength = defaultLength;

//        for (int i = 0; i < NumOfReflections; i++)
//        {
//            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, remainLength, layerMask);
//            Debug.DrawRay(ray.origin, ray.direction * remainLength, Color.green); // Визуализация луча

//            if (hit)
//            {
//                _lineRenderer.positionCount += 1;
//                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);

//                remainLength -= Vector2.Distance(ray.origin, hit.point);
//                ray = new Ray2D(hit.point, Vector2.Reflect(ray.direction, hit.normal));

//                Debug.DrawRay(hit.point, hit.normal, Color.red); // Визуализация нормали
//            }
//            else
//            {
//                _lineRenderer.positionCount += 1;
//                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, ray.origin + ray.direction * remainLength);
//                break;
//            }
//        }
//    }
//}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;


//[RequireComponent(typeof(LineRenderer))]
//public class Laser : MonoBehaviour
//{
//    public LayerMask layerMask;
//    public float defaultLength = 50f;
//    public int NumOfReflections = 3;

//    private LineRenderer _lineRenderer;
//    private Camera _camera;
//    private RaycastHit hit;

//    //Ray ray;
//    //private Vector3 direction;

//    private void Start()
//    {
//        _lineRenderer = GetComponent<LineRenderer>();
//        _camera = Camera.main;
//    }

//    private void Update()
//    {
//        ReflectLaser();
//        //Debug.DrawRay(hit.point, hit.normal, Color.red, 1f);
//    }

//    void ReflectLaser()
//    {
//        //ray = new Ray(transform.position, transform.forward);
//        // Получаем позицию мыши в мировом пространстве
//        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
//        mouseWorldPosition.z = 0;  // Обнуляем Z координату для 2D пространства

//        // Вычисляем направление от объекта (пушки) к курсору мыши
//        Vector3 direction = (mouseWorldPosition - transform.position).normalized;

//        // Создаём первый луч из текущей позиции объекта в направлении мыши
//        Ray ray = new(transform.position, direction);

//        _lineRenderer.positionCount = 1;
//        _lineRenderer.SetPosition(0, transform.position);

//        float remainLength = defaultLength;

//        for (int i = 0; i < NumOfReflections; i++)
//        {
//            if (Physics.Raycast(ray.origin, ray.direction, out hit, defaultLength, layerMask))
//            {
//                _lineRenderer.positionCount += 1;
//                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);

//                remainLength -= Vector3.Distance(ray.origin, hit.point);

//                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
//            }
//            else
//            {
//                _lineRenderer.positionCount += 1;
//                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, ray.origin + ray.direction * remainLength);
//            }
//        }
//    }

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
//}
