using System.Collections;
using System.Collections.Generic;
using UnityEngine;
                              
public class GameManager : MonoBehaviour
{
    public GameObject player;
    public static GameObject currentCheckPoint;

    private CheckPoint checkpoint;
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player");
        this.checkpoint = FindObjectOfType<CheckPoint>();

    }

    // Update is called once per frame
    void Update()
    {
        if (checkpoint.checkReach == true)
            {
                this.player.transform.position = currentCheckPoint.transform.position;
            }
            else
            {
                this.player.transform.position = new Vector3(0, 1, 0);
            }
    }
}
