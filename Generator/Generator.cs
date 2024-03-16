namespace Seminar2.Generator
{
    internal class Generator
    {
        private readonly Layer[] _layers;
        private readonly HashSet<Expression.Expression> _expressions = new();

        public Generator(int expressionSize)
        {
            _layers = new Layer[2 * expressionSize + 1];
            InitializeLayers();
        }

        private void InitializeLayers()
        {
            for (var i = 0; i < _layers.Length; i++)
                _layers[i] = new Layer((Layer.Type)(i%2));
        }

        public IEnumerable<Expression.Expression> GenerateExpression()
        {
            var exp = new LayerElement[_layers.Length];
            var enumerators = _layers.Select(l => l.GetEnumerator()).ToArray();
            var currentLevel = 0;

            while (currentLevel >= 0)
            {
                if (enumerators[currentLevel].MoveNext())
                {
                    exp[currentLevel] = enumerators[currentLevel].Current;
                    if (currentLevel == _layers.Length - 1)
                    {
                        var expression = new Expression.Expression(exp);
                        if (_expressions.Add(expression))
                            yield return expression;
                    }
                }
                else
                {
                    enumerators[currentLevel] = _layers[currentLevel].GetEnumerator();
                    currentLevel--;
                    continue;
                }

                if (currentLevel < _layers.Length - 1)
                    currentLevel++;
            }
        }
    }
}
