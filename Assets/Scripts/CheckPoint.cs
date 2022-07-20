using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    private GameManager checkManager;
    public bool checkReach;
    public Sprite checkConfirm;
    public Sprite checkFalse;
    public SpriteRenderer checkPointSpriteRenderer;

    // Use this for initialization
    void Start()
    {
        checkManager = FindObjectOfType<GameManager>();
        checkPointSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.name);
        if (collision.name == "Player")
        {
            Debug.Log("set!");
            GameManager.currentCheckPoint = gameObject;
            checkPointSpriteRenderer.sprite = checkConfirm;
            checkReach = true;
        }
    }

}
