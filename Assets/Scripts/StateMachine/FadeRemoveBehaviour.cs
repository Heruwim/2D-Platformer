using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehaviour : StateMachineBehaviour
{
    [SerializeField] private float _fadeTime = 0.5f;
    [SerializeField] private float _fadeDelay = 0.0f;

    private float _timeElapsed = 0f;
    private float _fadeDelayElapsed = 0f;

    private SpriteRenderer _spriteRenderer;
    private GameObject _objectToRemove;
    private Color _startColor;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timeElapsed = 0f;
        _spriteRenderer = animator.GetComponent<SpriteRenderer>();
        _startColor = _spriteRenderer.color;
        _objectToRemove = animator.gameObject;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        {

        }
        if (_fadeDelay > _fadeDelayElapsed)
        {
            _fadeDelayElapsed += Time.deltaTime;
        }
        else
        {
            _timeElapsed += Time.deltaTime;
            float newAlpha = _startColor.a * (1 - (_timeElapsed / _fadeTime));

            _spriteRenderer.color = new Color(_startColor.r, _startColor.g, _startColor.b, newAlpha);

            if (_timeElapsed > _fadeTime)
            {
                Destroy(_objectToRemove);
            }
        }

    }
}
