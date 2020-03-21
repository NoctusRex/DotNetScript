using System;
internal class Test2
{
	
	public void Print(string text)
	{
		Console.WriteLine(text);
		Console.ReadKey();
	}
	
}

public class Test
{
	public void test(string text)
	{
		new Test2().Print(Test2());
	}
	
	private string Test2()
	{
		return "test";
	}
	
	public void Test3()
	{
		test("nix");
	}
}