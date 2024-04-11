namespace ReflectionDocumenter
{
	public static class AccessModifierExtensions
	{
		public static string ToFriendlyString(this AccessModifiers accessModifier)
		{
			return accessModifier switch
			{
				AccessModifiers.ProtectedInternal => "protected internal",
				AccessModifiers.PrivateProtected => "private protected",
				_ => accessModifier.ToString().ToLower(),
			};
		}
	}
}