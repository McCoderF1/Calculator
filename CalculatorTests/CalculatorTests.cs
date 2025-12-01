namespace CalculatorTests
{
    public class CalculatorTests
    {
        [Test]
        public void Add_SingleValue_ValueReturned()
        {
            Calculator.Engine.Calculator calculator = new();

            int? result = calculator.Add(5);

            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void Add_TwoValues_ValuesAdded()
        {
            Calculator.Engine.Calculator calculator = new();

            int? result = calculator.Add(5, 5);

            Assert.That(result, Is.EqualTo(10));
        }
    }
}
