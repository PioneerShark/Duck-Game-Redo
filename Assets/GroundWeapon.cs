using UnityEngine;

public class GroundWeapon : MonoBehaviour
{
    [SerializeField]
    private string first;
    [SerializeField]

    private string second;
    [SerializeField]
    private Sprite model;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void WeaponSwap(string _first, string _second)
    {

    }
    public void OnTriggerEnter2D(Collider2D col) 
    {
        Character2D chara = col.gameObject.GetComponent<Character2D>();
        if (chara != null) 
        {
            var f = first;
            var s = second;
            if (!chara.isPlayer) return;
            if (chara.weaponInUse == "Pistol")
            {

                //WeaponSwap(chara.weaponAlt, chara.weaponAltSecondary);
                chara.weaponAlt = f;
                chara.weaponAltSecondary = s;
                chara.SwapWeapon();
            }
            else
            {
                chara.weaponInUse = f;
                chara.weaponInUseSecondary = s;
                chara.SwapWeapon();
                chara.SwapWeapon();

            }
        }
    }
}
