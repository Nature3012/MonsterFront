using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    enum PlayerState 
    {
        AliveMove,
        AliveAttack,
        Die
    }
    PlayerState playerState = PlayerState.AliveAttack;

}
