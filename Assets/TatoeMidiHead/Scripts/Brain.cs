using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MidiJack;
using UnityEngine;

public class Brain : MonoBehaviour
{
    GameObject camFrontObj;
    GameObject cam45Obj;
    Camera camFront;
    Camera cam45;
    Cam45Script cam45Script;
    CamFrontScript camFrontScript;
    
    public Transform
            eri,
            eriFace,
            eriHat,
            take,
            takeFace,
            takeGlasses;

    public GameObject
            eriLasers,
            takeLasers;

    List<Color>
        colorList =
            new List<Color>()
            {
                new Color32(183, 188, 155, 255),
                new Color32(51, 191, 79, 255),
                new Color32(45, 148, 206, 255),
                new Color32(220, 72, 41, 255),
                new Color32(255, 208, 0, 255)
            };

    int colorIndex = 0;

    // Map keys & notes that triggers animations
    string[]
        keys =
        {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "space",
            "escape",
            "z",
            "w"
        };

    int[][]
        notes =
        {
            new int[] { 36 },
            new int[] { 37, 50, 63, 76, 89, 102, 49 },
            new int[] { 38, 51, 64, 77, 90, 103 },
            new int[] { 39, 52, 65, 78, 91, 104, 62 },
            new int[] { 40, 53, 66, 79, 92, 105 },
            new int[] { 41, 54, 67, 80, 93, 106 },
            new int[] { 42, 55, 68, 81, 94, 107 },
            new int[] { 43, 56, 69, 82, 95, 108, 75 },
            new int[] { 44, 57, 70, 83, 96, 109, 88 },
            new int[] { 45, 58, 71, 84, 97, 110 },
            new int[] { 46, 59, 72, 85, 98, 111, 101 },
            new int[] { 47, 60, 73, 86, 99, 112 },
            new int[] { 48, 61, 74, 87, 100, 113 },
            new int[] { 38, 62, 86 }
        };

    void Start()
    {
        camFrontObj = GameObject.Find("Front Camera");
        cam45Obj = GameObject.Find("45 Camera");
        camFront = camFrontObj.GetComponent<Camera>();
        cam45 = cam45Obj.GetComponent<Camera>();
        cam45Script = cam45Obj.GetComponent<Cam45Script>();
        camFrontScript = camFrontObj.GetComponent<CamFrontScript>();
        camFront.DOColor(colorList[colorIndex], 0.2f);
        cam45.DOColor(colorList[colorIndex], 0.2f);
        cam45Script.isWired = false;
        eriLasers.SetActive(false);
        takeLasers.SetActive(false);        
    }

    void Update()
    {
        // 0. Reset Position
        if (TEGetKeyDown(0))
        {
            camFront.transform.DOLocalMove(new Vector3(0, 0, 30), 0.5f);
            cam45.transform.DOLocalMove(new Vector3(-10, 5, 8), 0.5f);
            camFrontObj.SetActive(true);
            cam45Obj.SetActive(false);
            take.DOScale(1, 0.3f);
            take.DOLocalRotate(new Vector3(0, 0, 0), 0.5f);
            eri.DOScale(1, 0.3f);
            eri.DOLocalRotate(new Vector3(0, 0, 0), 0.5f);
            cam45Script.isWired = false;
            camFrontScript.isWired = false;
        }

        // 1. Camera - Zoom
        if (TEGetKeyDown(1))
        {
            camFront.orthographicSize = 3f;
            cam45.orthographicSize = 3f;
        }
        else if (TEGetKeyUp(1))
        {
            camFront.orthographicSize = 5f;
            cam45.orthographicSize = 5f;
        }

        // 2. Take - Enlarge Glasses
        if (TEGetKeyDown(2))
        {
            takeGlasses.DOScaleZ(15, 0.5f);
        }
        else if (TEGetKeyUp(2))
        {
            takeGlasses.DOScaleZ(1, 0.5f);
        }

        // 3. Eri - Enlarge Hat
        if (TEGetKeyDown(3))
        {
            eriHat.DOLocalRotate(new Vector3(0, 170, 0), 0.5f);
            eriHat.DOScale(1.2f, 0.5f);
            eriHat.DOLocalMove(new Vector3(0, 2f, 0.3f), 0.5f);
        }
        else if (TEGetKeyUp(3))
        {
            eriHat.DOLocalRotate(new Vector3(0, 0, 0), 0.5f);
            eriHat.DOScale(1.0f, 0.5f);
            eriHat.DOLocalMove(new Vector3(0, 1.40f, -0.2f), 0.5f);
        }

        // 4. Camera - Change Background Color
        if (TEGetKeyDown(4))
        {
            camFront.DOColor(colorList[colorIndex], 0.2f);
            cam45.DOColor(colorList[colorIndex], 0.2f);
            colorIndex += 1;
            if (colorIndex >= colorList.Count)
            {
                colorIndex = 0;
            }
        }
        else if (TEGetKeyUp(4))
        {
            camFront.DOColor(colorList[colorIndex], 0.2f);
            cam45.DOColor(colorList[colorIndex], 0.2f);
            colorIndex += 1;
            if (colorIndex >= colorList.Count)
            {
                colorIndex = 0;
            }
        }

        // 5. Eri+Take - Shrink Faces
        if (TEGetKeyDown(5))
        {
            eriFace.DOScale(0.0f, 0.5f);
            takeFace.DOScale(0.0f, 0.5f);
        }
        else if (TEGetKeyUp(5))
        {
            eriFace.DOScale(1f, 0.5f);
            takeFace.DOScale(1f, 0.5f);
        }

        // 6. Camera - Shake
        if (TEGetKeyDown(6))
        {
            camFront.DOShakePosition(1.0f, 1.0f, 5, 3.0f, true);
            cam45.DOShakePosition(1.0f, 1.0f, 5, 3.0f, true);
        }

        // 7. Eri - Random Rotate, Scale
        if (TEGetKeyDown(7))
        {
            eri
                .DOLocalRotate(new Vector3(Random.Range(-90, 90),
                    Random.Range(-30, 30),
                    Random.Range(-15, 15)),
                0.3f);
            eri.DOScale(Random.Range(0.9f, 1.1f), 0.3f);
        }

        // 8. Take - Random Rotate, Scale
        if (TEGetKeyDown(8))
        {
            take
                .DOLocalRotate(new Vector3(Random.Range(-90, 90),
                    Random.Range(-30, 30),
                    Random.Range(-15, 15)),
                0.3f);
            take.DOScale(Random.Range(0.9f, 1.1f), 0.3f);
        }

        // 9. Take+Eri - Laser Beams
        if (TEGetKeyDown(9))
        {
            eriLasers.SetActive(true);
            takeLasers.SetActive(true);
        }
        else if (TEGetKeyUp(9))
        {
            eriLasers.SetActive(false);
            takeLasers.SetActive(false);
        }

        // 10. Camera - Switch
        if (TEGetKeyDown(10))
        {
            if( camFrontObj.activeSelf ){
                camFrontObj.SetActive(false);
                cam45Obj.SetActive(true);
            }else{
                camFrontObj.SetActive(true);
                cam45Obj.SetActive(false);
            }
        }

        // 11. Take+Eri - Rotate
        if (TEGetKey(11))
        {
            eri.Rotate(new Vector3(0, 360, 0) * Time.deltaTime);
            take.Rotate(new Vector3(0, 270, 0) * Time.deltaTime);
        }

        // 12. Take+Eri - Enlarge
        if (TEGetKeyDown(12))
        {
            take.DOScale(Random.Range(2f, 2f), 0.3f);
            eri.DOScale(Random.Range(2f, 2f), 0.3f);
        }
        else if (TEGetKeyUp(12))
        {
            take.DOScale(Random.Range(1f, 1f), 0.3f);
            eri.DOScale(Random.Range(1f, 1f), 0.3f);
        }

        // 13. Wireframe
        if (TEGetKeyDown(13))
        {  
            if( !cam45Script.isWired )
            {
                cam45Script.isWired = true;
                camFrontScript.isWired = true;
            }
            else
            {
                cam45Script.isWired = false;
                camFrontScript.isWired = false;
            }
        }

    }

    // Check input states
    private bool TEGetKeyDown(int index)
    {
        // Debug.Log(Input.GetKeyDown(keys[index]));
        if (Input.GetKeyDown(keys[index])) return true;
        for (int i = 0; i < notes[index].Length; i++)
        {
            if (MidiMaster.GetKeyDown(notes[index][i])) return true;
        }
        return false;
    }

    private bool TEGetKeyUp(int index)
    {
        if (Input.GetKeyUp(keys[index])) return true;
        for (int i = 0; i < notes[index].Length; i++)
        {
            if (MidiMaster.GetKeyUp(notes[index][i])) return true;
        }
        return false;
    }

    private bool TEGetKey(int index)
    {
        if (Input.GetKey(keys[index])) return true;
        for (int i = 0; i < notes[index].Length; i++)
        {
            if (MidiMaster.GetKey(notes[index][i]) > 0) return true;
        }
        return false;
    }
}
