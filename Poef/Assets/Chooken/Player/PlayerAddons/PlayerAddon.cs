using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public abstract class PlayerAddon : MonoBehaviour
{
    protected Player m_player;

    public Type Init(Player player)
    {
        if (m_player == null) m_player = player;
        return this.GetType();
    }
}
