using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Material hiddenMaterial;
    private Material originalMaterial;
    public LevelManager manager;
    private Animator animator;

    void Start()
    {
     originalMaterial = GetComponent<Renderer>().material;   
     animator = GetComponent<Animator>();   
    }

    void OnMouseEnter(){
        animator.SetBool("mouseOver", true);
    }

    void OnMouseExit(){
        animator.SetBool("mouseOver", false);
    }

    void OnMouseUp(){
        manager.CubeRevealed(this);
    }

    public void RevealColor(){
        GetComponent<Renderer>().material = hiddenMaterial;
        animator.SetBool("cubeSelected", true);
    }

    public void UnrevealColor(){
        GetComponent<Renderer>().material = originalMaterial;
    }
            
    // Update is called once per frame
    void Update()
    {
        
    }
}