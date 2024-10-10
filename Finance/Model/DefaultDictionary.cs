namespace Finance.Model;

public class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue>
{
    private readonly Func<TValue> _defaultValueFactory;

    public DefaultDictionary(Func<TValue> defaultValueFactory)
    {
        _defaultValueFactory = defaultValueFactory;
    }

    public new TValue this[TKey key]
    {
        get
        {
            if (!TryGetValue(key, out TValue value))
            {
                value = _defaultValueFactory();
                Add(key, value);
            }
            return value;
        }
        set => base[key] = value;
    }
}

// Aliases, tack Claude.
// using Level4Dict = Finance.Model.DefaultDictionary<int, System.Collections.Generic.List<Finance.Model.Transaction>>;
// using Level3Dict = Finance.Model.DefaultDictionary<int, Finance.Model.DefaultDictionary<int, System.Collections.Generic.List<Finance.Model.Transaction>>>;
// using Level2Dict = Finance.Model.DefaultDictionary<int, Finance.Model.DefaultDictionary<int, Finance.Model.DefaultDictionary<int, System.Collections.Generic.List<Finance.Model.Transaction>>>>;
// using Level1Dict = Finance.Model.DefaultDictionary<int, Finance.Model.DefaultDictionary<int, Finance.Model.DefaultDictionary<int, Finance.Model.DefaultDictionary<int, System.Collections.Generic.List<Finance.Model.Transaction>>>>>;
// using System.Globalization;