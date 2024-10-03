using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public static ActiveWeapon Instance { get; private set; }

    [SerializeField] private Wand wand;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        FollowMousePosition(); //каждый кадр проверяет следует ли оружие за курсором
    }

    public Wand GetActiveWeapon()
    {
        return wand;
    }

    //поворачивает оружие в сторону курсора
    private void FollowMousePosition()
    {
        Vector3 mousePos = GameInput.Instance.GetMousePosition(); //смотрит координаты курсора
        Vector3 playerPosition = Player.Instance.GetPlayerScreenPosition(); //смотрит координаты героя

        if (mousePos.x < playerPosition.x) //если координата курсора по оси х меньше оси х героя, то разворачивает оружие
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
