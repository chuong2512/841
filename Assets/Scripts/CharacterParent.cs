using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParent : MonoBehaviour
{
    public int moveSpeed;

    public void Move()
    {
        this.transform.Translate(new Vector3(0, 0, 1) * moveSpeed * Time.deltaTime);
    }

    public void Stop()
    {
        this.transform.Translate(new Vector3(0, 0, 0));
    }
}
