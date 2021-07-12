using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    internal int enemiesKilled;
    internal int shellsFired;
    internal float playerHealth;
    internal int waveNo;

    internal float[] position;

    public PlayerData(Player _player)
    {
        enemiesKilled = _player.enemiesKilled;
        shellsFired = _player.bulletsFired;
        playerHealth = _player.currentHealth;
        waveNo = _player.waveNo;

        position = new float[3];
        position[0] = _player.transform.position.x;
        position[1] = _player.transform.position.y;
        position[2] = _player.transform.position.z;

    }

}
