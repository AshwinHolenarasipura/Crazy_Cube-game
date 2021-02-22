using UnityEngine;

public class Blade_Controller : MonoBehaviour
{
    [SerializeField] private GameObject Blade;
    [SerializeField] private Transform pos1, pos2;
    private float Blade_Rotate_Speed = -10f;
    private float Blade_Move_Speed = 2f;
    bool Turnback;

    // Update is called once per frame
    void Update()
    {
        Blade_Rotate();
    }
    void Blade_Check()
    {
        if (Blade.transform.position.x <= pos1.position.x)
        {
            Turnback = true;
        }
        else if (Blade.transform.position.x >= pos2.position.x)
        {
            Turnback = false;
        }
    }
    void Blade_Rotate()
    {
        Blade.transform.Rotate(0, 0, Blade_Rotate_Speed);
        Blade_Check();
        if (Turnback)
        {
            Blade.transform.position = Vector2.MoveTowards(Blade.transform.position, pos2.position,
                Blade_Move_Speed * Time.deltaTime);
        }
        else
        {
            Blade.transform.position = Vector2.MoveTowards(Blade.transform.position, pos1.position,
               Blade_Move_Speed * Time.deltaTime);
        }
    }
}
