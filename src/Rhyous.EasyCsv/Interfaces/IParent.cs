namespace Rhyous.EasyCsv
{
    public interface IParent<TParent>
        where TParent : class
    {
        TParent Parent { get; set; }
    }
}
