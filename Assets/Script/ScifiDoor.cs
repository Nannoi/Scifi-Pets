using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScifiDoor : MonoBehaviour
{
    [SerializeField] private int currentPiece;
    public static ScifiDoor Instance;
    private Animator animator;

    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    public void IncreasePiece(int value)
    {
        currentPiece += value;
        animator.SetInteger("Puzzle", currentPiece);
    }
}

