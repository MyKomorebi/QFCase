using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public void Hurt(float value, bool force = false);
    void SetHPScale(float hPScale);
    void SetSpeedScale(float speedScale);
}
