using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfWeapon
{
    Gun,
    ShotGun,
    Magnum,
    Rifle,
    Knife,
    Graneade
}


[CreateAssetMenu(fileName = "newWeapon",menuName = "Weapons")]
public class ScriptableWeapons : ScriptableObject
{
    public new string name;
    public TypeOfWeapon typeOfWeapon;
    public float damage;
    public float rateShoot;
    public int capacity;
    public string prefabName;
    public int lvlWeapon;
    public Sprite imageObject;
    public int priceWeapon;
    public int amountDays;

       


    // Start is called before the first frame update
}
