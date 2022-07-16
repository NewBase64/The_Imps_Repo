using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubism : MonoBehaviour
{
    [Header("----Cubez----")]
    [SerializeField] AnimationCurve curve;
    [SerializeField] GameObject Up;
    [SerializeField] GameObject Down;
    [SerializeField] GameObject Left;
    [SerializeField] GameObject Right;
    [SerializeField] GameObject Forward;
    [SerializeField] GameObject Back;
    [Header("----Nums----")]
    [Range(0.1f, 1)][SerializeField] float Speed = 1;
    [Range(1, 6)][SerializeField] int CurrentNum;
    [Header("----Rotation----")]
    [Range(0f, 100)][SerializeField] float rotx;
    [Range(0f, 100)][SerializeField] float roty;
    [Range(0f, 100)][SerializeField] float rotz;
    [Range(0f, 100)][SerializeField] float rotRange;

    float _current = 0, _target = 1;
    bool transitioning = false;
    bool Building = true;

    Vector3 randomRot;
    Vector3 upPos = new Vector3(0, 1, 0);
    Vector3 downPos = new Vector3(0, -1, 0);
    Vector3 leftPos = new Vector3(-1, 0, 0);
    Vector3 rightPos = new Vector3(1, 0, 0);
    Vector3 forwardPos = new Vector3(0, 0, 1);
    Vector3 backPos = new Vector3(0, 0, -1);

    // Start is called before the first frame update
    void Start()
    {
        if (CurrentNum == 0)
            CurrentNum = Random.Range(1, 6);
        if (CurrentNum == 1)
            Building = true;
        else if (CurrentNum == 2)
            Right.transform.localPosition = rightPos;
        else if (CurrentNum == 3)
        {
            Right.transform.localPosition = rightPos;
            Left.transform.localPosition = leftPos;
        }
        else if (CurrentNum == 4)
        {
            Right.transform.localPosition = rightPos;
            Left.transform.localPosition = leftPos;
            Up.transform.localPosition = upPos;
        }
        else if (CurrentNum == 5)
        {
            Right.transform.localPosition = rightPos;
            Left.transform.localPosition = leftPos;
            Up.transform.localPosition = upPos;
            Down.transform.localPosition = downPos;
        }
        else if (CurrentNum == 6)
        {
            Right.transform.localPosition = rightPos;
            Left.transform.localPosition = leftPos;
            Up.transform.localPosition = upPos;
            Down.transform.localPosition = downPos;
            Forward.transform.localPosition = forwardPos;
        }

        randomRot.x = rotx + Random.Range(-rotRange, rotRange);
        randomRot.y = rotx + Random.Range(-rotRange, rotRange);
        randomRot.z = rotx + Random.Range(-rotRange, rotRange);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(randomRot * Time.deltaTime);

        _current = Mathf.MoveTowards(_current, _target, Speed * Time.deltaTime);

        switch (CurrentNum)
        {
            case 1:
                Mover(Right, rightPos, Building);
                StartCoroutine(ChangeNumber());
                break;
            case 2:
                Mover(Left, leftPos, Building);
                StartCoroutine(ChangeNumber());
                break;
            case 3:
                Mover(Up, upPos, Building);
                StartCoroutine(ChangeNumber());
                break;
            case 4:
                Mover(Down, downPos, Building);
                StartCoroutine(ChangeNumber());
                break;
            case 5:
                Mover(Forward, forwardPos, Building);
                StartCoroutine(ChangeNumber());
                break;
            case 6:
                Mover(Back, backPos, Building);
                StartCoroutine(ChangeNumber());
                break;
        }
    }

    private void Mover(GameObject cube, Vector3 target, bool build)
    {
        if (build)
        {
            cube.transform.localPosition = Vector3.Lerp(Vector3.zero, target, curve.Evaluate(_current));
        }
        else
        {
            cube.transform.localPosition = Vector3.Lerp(target, Vector3.zero, curve.Evaluate(_current));
        }
    }

    private IEnumerator ChangeNumber()
    {
        if (!transitioning)
        {
            transitioning = true;
            yield return new WaitForSeconds(Speed);
            if (CurrentNum == 6)
            {
                if (Building)
                {
                    Building = false;
                }
                else
                {
                    Building = true;
                }
                CurrentNum = 1;
            }
            else
                CurrentNum++;
            _current = 0;
            transitioning = false;
        }
    }
}
