using System.Reflection;

namespace ReflectionDocumenter
{
	public static class ClassInfoExtensions
	{
		public static AccessModifiers GetAccessModifier(this Type type)
		{
			var accessModifier = AccessModifiers.Internal;

			if (type.IsPublic)
			{
				accessModifier = AccessModifiers.Public;
			}

			return accessModifier;
		}

		public static string GetTypeName(this Type type)
		{
			return type.IsGenericType ? $"{type.Name.Split('`')[0]}<{string.Join(", ", type.GetGenericArguments().Select(t => t.Name))}>" : type.Name;
		}

		public static AccessModifiers GetAccessModifier(this FieldInfo field)
		{
			var accessModifier = AccessModifiers.Private;

			if (field.IsPublic)
			{
				accessModifier = AccessModifiers.Public;
			}
			else if (field.IsFamily)
			{
				accessModifier = AccessModifiers.Protected;
			}
			else if (field.IsAssembly)
			{
				accessModifier = AccessModifiers.Internal;
			}
			else if (field.IsFamilyOrAssembly)
			{
				accessModifier = AccessModifiers.ProtectedInternal;
			}
			else if (field.IsFamilyAndAssembly)
			{
				accessModifier = AccessModifiers.PrivateProtected;
			}

			return accessModifier;
		}

		public static AccessModifiers GetAccessModifier(this PropertyInfo property)
		{
			if (property.GetMethod != null && property.SetMethod != null)
			{
				var getAccessModifier = GetAccessModifier(property.GetMethod);
				var setAccessModifier = GetAccessModifier(property.SetMethod);

				return GetMostPermissiveAccessModifier(getAccessModifier, setAccessModifier);
			}

			return property.SetMethod != null ? GetAccessModifier(property.SetMethod) : GetAccessModifier(property.GetMethod);
		}

		public static AccessModifiers GetAccessModifier(this ConstructorInfo constructorInfo)
		{
			var accessModifier = AccessModifiers.Private;

			if (constructorInfo.IsPublic)
			{
				accessModifier = AccessModifiers.Public;
			}
			else if (constructorInfo.IsFamily)
			{
				accessModifier = AccessModifiers.Protected;
			}
			else if (constructorInfo.IsAssembly)
			{
				accessModifier = AccessModifiers.Internal;
			}
			else if (constructorInfo.IsFamilyOrAssembly)
			{
				accessModifier = AccessModifiers.ProtectedInternal;
			}
			else if (constructorInfo.IsFamilyAndAssembly)
			{
				accessModifier = AccessModifiers.PrivateProtected;
			}

			return accessModifier;
		}

		public static AccessModifiers GetAccessModifier(this MethodInfo method)
		{
			var accessModifier = AccessModifiers.Private;

			if (method.IsPublic)
			{
				accessModifier = AccessModifiers.Public;
			}
			else if (method.IsFamily)
			{
				accessModifier = AccessModifiers.Protected;
			}
			else if (method.IsAssembly)
			{
				accessModifier = AccessModifiers.Internal;
			}
			else if (method.IsFamilyOrAssembly)
			{
				accessModifier = AccessModifiers.ProtectedInternal;
			}
			else if (method.IsFamilyAndAssembly)
			{
				accessModifier = AccessModifiers.PrivateProtected;
			}

			return accessModifier;
		}

		public static string GetTypeName(this MethodInfo method)
		{
			return method.IsGenericMethod ? $"{method.Name.Split('`')[0]}<{string.Join(", ", method.GetGenericArguments().Select(t => t.Name))}>" : method.Name;
		}

		public static AccessModifiers GetAccessModifier(this EventInfo eventInfo)
		{
			var accessModifier = AccessModifiers.Private;

			if (eventInfo.AddMethod.IsPublic)
			{
				accessModifier = AccessModifiers.Public;
			}
			else if (eventInfo.AddMethod.IsFamily)
			{
				accessModifier = AccessModifiers.Protected;
			}
			else if (eventInfo.AddMethod.IsAssembly)
			{
				accessModifier = AccessModifiers.Internal;
			}
			else if (eventInfo.AddMethod.IsFamilyOrAssembly)
			{
				accessModifier = AccessModifiers.ProtectedInternal;
			}
			else if (eventInfo.AddMethod.IsFamilyAndAssembly)
			{
				accessModifier = AccessModifiers.PrivateProtected;
			}

			return accessModifier;
		}

		private static AccessModifiers GetMostPermissiveAccessModifier(AccessModifiers modifier1, AccessModifiers modifier2)
		{
			AccessModifiers[] hierarchy = {
				AccessModifiers.Public,
				AccessModifiers.ProtectedInternal,
				AccessModifiers.Protected,
				AccessModifiers.PrivateProtected,
				AccessModifiers.Internal,
				AccessModifiers.Private
			};

			var index1 = Array.IndexOf(hierarchy, modifier1);
			var index2 = Array.IndexOf(hierarchy, modifier2);

			return index1 < index2 ? modifier1 : modifier2;
		}
	}
}