using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Miner : MonoBehaviour
{
    [SerializeField] private float _timeBetweenHits;
    [SerializeField] private Ore _ore;
    [SerializeField] private Color[] _colors;
    [SerializeField] private Hammer _hammer;
    [SerializeField] private Platform _platform;
    [SerializeField] private float _coolingTime;
    [SerializeField] private Renderer _renderer;

    private Animator _animator;
    private float _runningTime = 0;
    private Material _currentMaterial;
    private Color _lastColor;
    private int _currentKey = 0;
    private Coroutine coolDownJob;

    public Dictionary<int, Color> TemperatureColors = new Dictionary<int, Color>();
    public Color CurrentColor => _currentMaterial.color;

    public event UnityAction<Color> ColorChanged;

    private void OnEnable()
    {
        _hammer.Hit += OnHit;
    }

    private void OnDisable()
    {
        _hammer.Hit -= OnHit;
    }

    private void Start()
    {
        for (int i = 0; i < _colors.Length; i++)
        {
            TemperatureColors.Add(i, _colors[i]);
        }

        _currentMaterial = _renderer.material;
        _currentMaterial.color = TemperatureColors[_currentKey];
        _lastColor = CurrentColor;
        _animator = GetComponent<Animator>();

    }

    /*private void Update()
     {
         if (_platform.IsWork)
         {
             Debug.Log("Work");
             _animator.Play("HitWithHammer");

             if (_runningTime >= _timeBetweenHits)
             {
                 _animator.Play("HitWithHammer");
                 _runningTime = 0;
             }
         }

         if (_currentKey != 0 && coolDownJob == null)
         {
             Debug.Log("Start");
             coolDownJob = StartCoroutine(CoolDown());
         }

         _runningTime += Time.deltaTime;
     }*/

    private void OnHit(Color newColor)
    {
        _currentMaterial.color = newColor;
        ColorChanged?.Invoke(_lastColor);
        _lastColor = CurrentColor;
        /* if (isHitRedPiece)
         {
             if (TemperatureColors.ContainsKey(_currentKey + 1))
             {
                 _currentMaterial.color = TemperatureColors[_currentKey + 1];
                 _currentKey += 1;
             }
         }*/
    }

    public void HitWithHammer()
    {
        Debug.Log(_animator);
        _animator.Play("HitWithHammer");
    }

    private IEnumerator CoolDown()
    {
        while (true)
        {
            if (TemperatureColors.ContainsKey(_currentKey - 1))
            {
                _currentMaterial.color = TemperatureColors[_currentKey - 1];
                _currentKey -= 1;
            }
            else
            {
                StopCoroutine(coolDownJob);
                coolDownJob = null;
            }

            yield return new WaitForSeconds(_coolingTime);
        }

    }
}
