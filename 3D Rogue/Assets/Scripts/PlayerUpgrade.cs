using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : Item {

  [HideInInspector] public int healthBonus = 2;
  [HideInInspector] public int maxHealthBonus = 1;
  [HideInInspector] public float playerSpeedBonus = 1;
  [HideInInspector] public float fireRateBonus = 2;
  [HideInInspector] public float damageBonus = 1;

    void Start()
    {
        name = this.gameObject.name;
        Debug.Log("name");
    }


}
