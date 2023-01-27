using ClassLibrary;

namespace ClassLibrary.Tests
{
	public class TestExamples
	{
		[Fact]
		public void DummyString_returns_knownvalue()
		{
			var actual = ExampleClass.DummyString();
			Assert.Equal("TextExample", actual);
		}
	}
}