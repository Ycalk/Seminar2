namespace Seminar2.Generator
{
    internal class LayerElement (string value, LayerElement.Type type)
    {
        public enum Type
        {
            Number,
            Variable,
            Operator
        }

        public Type ElementType { get; } = type;
        public string Value { get; } = value;
    }
}
