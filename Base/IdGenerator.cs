using System;

namespace Base
{
	public static class IdGenerator
	{
		private static int currentValue;
		private static bool isPrepared;

		public static string GetNewId()
		{
			if (!isPrepared)
				return Guid.NewGuid().ToString();

			var guid = ToGuid(currentValue).ToString();
			currentValue++;
			return guid;
		}

		public static void PrepareGeneratorForTest(int startValue)
		{
			currentValue = startValue;
			isPrepared = true;
		}

		public static void ResetPreparation()
		{
			isPrepared = false;
		}

		private static Guid ToGuid(int value)
		{
			byte[] bytes = new byte[16];
			BitConverter.GetBytes(value).CopyTo(bytes, 0);
			return new Guid(bytes);
		}
	}
}
