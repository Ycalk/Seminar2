using System.Collections;
using System.Collections.ObjectModel;

namespace Seminar2.Generator
{
    internal class Layer(Layer.Type type) : IEnumerable<LayerElement>
    {
        public enum Type
        {
            Numbers,
            Operators
        }

        public Type LayerType { get; } = type;

        public LayerElement this[int index] => Elements[index];

        private readonly ReadOnlyCollection<LayerElement> _numbers = new(
        [
            new LayerElement("0", LayerElement.Type.Number),
            new LayerElement("1", LayerElement.Type.Number),
            new LayerElement("2", LayerElement.Type.Number),
            new LayerElement("x", LayerElement.Type.Variable)
        ]);

        private readonly ReadOnlyCollection<LayerElement> _operators = new(
        [
            new LayerElement("+", LayerElement.Type.Operator),
            new LayerElement("-", LayerElement.Type.Operator),
            new LayerElement("*", LayerElement.Type.Operator)
        ]);

        private ReadOnlyCollection<LayerElement> Elements => LayerType == Type.Numbers ? _numbers : _operators;
        public IEnumerator<LayerElement> GetEnumerator() => Elements.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
