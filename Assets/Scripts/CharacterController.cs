using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] protected float Speed { get {return speed;} set{ speed = value;} }
    protected float Health
    {
        get {  return health; } //veriyi okurken çalışan metot
        set //veriyi çekreken çalışan metot
        {
            
            if (value < 0)
                health = 0;
            else
                health = value;
        }
    }
}
