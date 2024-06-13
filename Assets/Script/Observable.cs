using System.Collections.Generic;
using System.ComponentModel;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine;

public class Observable<T> : INotifyPropertyChanged, INotifyPropertyChanging // C#은 다중 상속이 안되는 대신, 인터페이스는 가능함
{   //관측 가능한 인자를 소환 할 수 있다.
    private T value;

    private IEqualityComparer<T> Comparer;

    public T Value
    {
        get
        {
            return value;
        }
        set
        {
            if (!Comparer.Equals(this.value, value))
            {
                OnValueChanging();
                this.value = value;
                OnValueChanged();
            }
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;
    public event PropertyChangingEventHandler PropertyChanging;

    public Observable(T defaultValue = default(T), IEqualityComparer<T> comparer = null)
    {   //생성자.
        value = defaultValue;
        this.Comparer = comparer ?? EqualityComparer<T>.Default;
    }

    protected virtual void OnValueChanging()
    {
        this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs("value"));
    }

    protected virtual private void OnValueChanged()
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("value"));
    }
}


//t 예제용 class.
class temptemp : MonoBehaviour
{
    public Observable<int> _hp;
    public Text text_hp;



    public delegate void aaa();
    public delegate void aaaa(int a);
    public delegate void aaaaa(int a, int b);
    //=>이와같은 일을 방지 하고자, Action을 쓴다.

    int c = 0;
    public void testc(Action callback = null)
    {
        int a = 0;
        int b = 0;

        c = a + b;

        if (callback != null)
        {
            callback();
        }
    }

    private void Start()
    {
        testc(() => { });

        _hp = new Observable<int>();


        _hp.PropertyChanging += (o, e) =>
        {
            text_hp.text = _hp.Value.ToString();
            // _hp.Value 의 get.
        };
        

    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 200, 100), "teset"))
        {
            _hp.Value += 1;
            //_hp.Value의 set.
        }
    }
}