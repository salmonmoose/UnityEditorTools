using System;
using NUnit.Framework;

[TestFixture]
[Category("Test Kickstart")]
public class Descartes {
	[Test]
	public void CogitoErgoSum()
	{
		bool I_THINK = true;
		bool I_AM = true;

		Assert.AreEqual(I_THINK, I_AM);
	}
}