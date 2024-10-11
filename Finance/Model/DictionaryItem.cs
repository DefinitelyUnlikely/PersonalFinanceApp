namespace Finance.Model;

public class DictionaryItem
{
    public string Key { get; set; }
    public List<Model.Transaction> Value { get; set; }

    public DictionaryItem(string key, List<Model.Transaction> value)
    {
        Key = key;
        Value = value;
    }
}
