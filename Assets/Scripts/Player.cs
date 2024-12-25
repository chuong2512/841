using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Material characterColor;
    Material trailEffect;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        characterColor = this.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
        trailEffect = this.gameObject.transform.GetChild(2).GetComponent<ParticleSystemRenderer>().trailMaterial;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blue"))
        {
            ChangeCharacterColor(0);
        }

        if (other.gameObject.CompareTag("Green"))
        {
            ChangeCharacterColor(1);
        }

        if (other.gameObject.CompareTag("Red"))
        {
            ChangeCharacterColor(2);
        }


        if (other.gameObject.CompareTag("Finish"))
        {
            GameManager.instance.TouchedFinishline();
        }
    }

    void ChangeCharacterColor(int num)
    {
        characterColor = GameManager.instance.characterColors[num];
        trailEffect = GameManager.instance.trailEffects[num];
        this.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = characterColor;
        this.gameObject.transform.GetChild(2).GetComponent<ParticleSystemRenderer>().trailMaterial = trailEffect;
    }
    public void Run()
    {
        animator.SetInteger("Player", 1);
    }

    public void FastRun()
    {
        animator.SetInteger("Player", 2);
    }

    void Die()
    {
        GameManager.instance.playersList.Remove(this.gameObject);
        GameManager.instance.PlayerCounter();
        GameManager.instance.CharacterParentCheck();
        GameManager.instance.DiePlayer();
        int zrot = Random.Range(0, 359);
        GameObject dieFx = Instantiate(GameManager.instance.dieEffext, new Vector3(this.transform.position.x, 0, this.transform.position.z), Quaternion.Euler(90, 0, zrot));
        dieFx.GetComponent<SpriteRenderer>().color = this.gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color;
        Destroy(this.gameObject);
    }
}
