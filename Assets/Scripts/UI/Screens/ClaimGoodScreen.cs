using UnityEngine;
using UnityEngine.Events;

public class ClaimGoodScreen : Screen
{
    [SerializeField] private GoodView _viewTamplate;
    [SerializeField] private GameObject _container;

    private GoodView _view;

    public event UnityAction<Good> GoodClamed;

    private void Awake()
    {
        Close();
    }

    public override void Close()
    {
        base.Close();

        if (_view != null)
        {
            _view.Choosed -= OnGoodChoosed;
            Destroy(_view.gameObject);
            _view = null;
        }
    }

    public void ShowGood(Good good)
    {
        _view = Instantiate(_viewTamplate, _container.transform);
        _view.Init(good);
        _view.Render();
        _view.Choosed += OnGoodChoosed;
        Open();
    }

    private void OnGoodChoosed(Good good)
    {
        GoodClamed?.Invoke(good);
        Close();
    }
}
