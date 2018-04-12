using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;


[CustomEditor(typeof(Pickup))]
public class PickupEditor : Editor {

    public override void OnInspectorGUI()
    {
        Pickup pickup = (Pickup)target;

        pickup.type = (PickupType)EditorGUILayout.EnumPopup("Pickup Type", pickup.type);

        pickup.name = EditorGUILayout.TextField("Name", pickup.name);
        pickup.rotateSpeed = EditorGUILayout.FloatField("Rotation Speed", pickup.rotateSpeed);

        switch (pickup.type)
        {
            case PickupType.MovementSpeed:
                pickup.speedBonus = EditorGUILayout.IntField("Speed Bonus", pickup.speedBonus);
                break;
            case PickupType.SmallHealth:
                pickup.healthBonus = EditorGUILayout.IntField("Health Bonus", pickup.healthBonus);
                break;
            case PickupType.FireRate:
                pickup.fireRateBonus = EditorGUILayout.FloatField("Fire Rate Bonus", pickup.fireRateBonus);
                break;
            case PickupType.WeaponDamage:
                pickup.damageBonus = EditorGUILayout.IntField("Damage Bonus", pickup.damageBonus);
                break;
            default:
                Debug.LogError("Invalid pickup type!");
                break;
        }
    }

}
#endif

public enum PickupType
{
    SmallHealth,
    MovementSpeed,
    FireRate,
    WeaponDamage
};
