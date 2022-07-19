using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyDome : MonoBehaviour
{
    public GameObject Player;

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, 0, Player.transform.position.z);
        transform.Rotate(0, Time.deltaTime, 0);
    }
}
