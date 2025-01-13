using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu]
public class ShurikenSpread : ProjectileGunBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override IEnumerator StartShootProjectile(Character2D character2D)
    {

        character2D.model.animator.Play("Spin");
        base.StartShootProjectile(character2D);
        yield return new WaitForSeconds(2f);
        character2D.model.animator.Play("Base");
    }
}
