namespace Seminar2.Generator
{
    internal class LayerElement (string element, LayerElement.Type type)
    {
        public enum Type
        {
            Number,
            Variable,
            Operator
        }

        public Type ElementType { get; } = type;
        public string Element { get; } = element;
    }
}
