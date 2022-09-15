using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelManager : MonoBehaviour
{
    public GameObject cubePrefab;
    public int lines = 4;
    public int columns = 3;
    public float padding = 0.3f;
    public Material[] materials;
    private List<Material> potentialMaterials = new List<Material>();
    private List<CubeBehaviour> cubes = new List<CubeBehaviour>();
    private List<CubeBehaviour> cubesRevealed = new List<CubeBehaviour>();
    private List<CubeBehaviour> alreadyMatched = new List<CubeBehaviour>();
    public TextMeshProUGUI scoreText;
    private int score = 0;
    

    // Start is called before the first frame update
    void Start()
    {   
        if((lines * columns) % 2 != 0) {
            Debug.LogError("The level need to have an even number of cubes", gameObject);
            return;
        }

        for(int i=0; i < materials.Length; i++){
            potentialMaterials.Add(materials[i]);
            potentialMaterials.Add(materials[i]);
        }
        float decal = 1 + padding;

        for(float y=0; y < decal * lines; y += decal){
            for(float x=0; x < decal * columns; x += decal){
                Vector3 position = new Vector3(x, y, 0f);
                CreateCube(position);
            }
        }
    }
    IEnumerator UnRevealCubes(){
        yield return new WaitForSeconds(2f);
        for(int i=0; i<cubesRevealed.Count; i++){
            cubesRevealed[i].UnrevealColor();
        }
        cubesRevealed.Clear();
    }
    

    public void CubeRevealed(CubeBehaviour cube){
        if(cubesRevealed.Contains(cube))return;
        if(alreadyMatched.Contains(cube))return;
        if(cubesRevealed.Count >= 2) return;

        cube.RevealColor();
        cubesRevealed.Add(cube);

        if(cubesRevealed.Count >= 2){
            score++;
            scoreText.text = score.ToString();
            if(cubesRevealed[0].hiddenMaterial == cubesRevealed[1].hiddenMaterial){
                Debug.Log("It's a match!!!");
                alreadyMatched.Add(cubesRevealed[0]);
                alreadyMatched.Add(cubesRevealed[1]);
                cubesRevealed.Clear();
            }
            else {
                StartCoroutine(UnRevealCubes());
            }
        }
    
    }

    private void CreateCube(Vector3 position){
        GameObject cubeGO = Instantiate(cubePrefab, position, Quaternion.identity);
        CubeBehaviour cube = cubeGO.GetComponent<CubeBehaviour>();
        cubes.Add(cube);
        cube.manager = this;

        int index = Random.Range(0, potentialMaterials.Count);
        cube.hiddenMaterial = potentialMaterials[index];
        potentialMaterials.RemoveAt(index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}