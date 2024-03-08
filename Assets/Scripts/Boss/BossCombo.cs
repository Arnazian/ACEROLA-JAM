using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combo", menuName = "Combo")]
public class BossCombo : ScriptableObject
{
    public BaseBossAttack[] attacksInCombo;
    public float[] delayAfterAttack;
}
