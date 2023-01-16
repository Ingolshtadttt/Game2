using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)//проверка на касание тригера
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit is Hero)
        {
            unit.ReceiveDamage();
        }
    }
}
