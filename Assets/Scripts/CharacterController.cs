using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private float health;
    protected float Speed { get; set; }
    protected float Health
    {
        get { return health; } //veriyi okurken çalışan metot
        set //veriyi çekreken çalışan metot
        {
            if (value < 0)
                health = 0;
            else
                health = value;
        }
    }



}
