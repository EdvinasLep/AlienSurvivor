using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehavior : ProjectileWeaponBehavior
{
    KnifeController kController;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        kController = FindObjectOfType<KnifeController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * kController.speed * Time.deltaTime;
    }
}
