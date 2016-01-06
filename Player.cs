using UnityEngine;
using System.Collections;

public class Player : Entity {


    protected override void Start()
    {
        base.Start();

    }

    void Update () {
	
        if(health <= 0)
        {
            base.Die();
        }
	}
}
