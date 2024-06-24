using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int bulletsPerTap;
    public int bulletsShoot;
    public int bullesLefts;
    protected float speed = 5f;
    protected float jumpForce = 7f;
    public bool onGround;
    public float bulletForce = 20f;
    public float upwardForce = 20f;
    public float spread = 20;
    public float timebetweenShooting = 1.5f;
    public float reloadTime = 2f;
    public bool allowInvoke = true;
    public bool reloading;
}
