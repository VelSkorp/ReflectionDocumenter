using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace ReflectionDocumenter
{
	public class ReflectedClassInfo
	{
		private readonly BindingFlags BindingFlags = 
			BindingFlags.Public |
			BindingFlags.NonPublic |
			BindingFlags.Instance |
			BindingFlags.Static |
			BindingFlags.DeclaredOnly;

		public string GetClassDefinition(Type type)
		{
			var classString = new StringBuilder();
			classString.Append($"{type.GetAccessModifier().ToFriendlyString()} class {type.GetTypeName()}\n{{\n");

			classString.Append(GetFieldsDefinition(type.GetFields(BindingFlags)));

			classString.Append(GetProperiesDefinition(type.GetProperties(BindingFlags)));

			classString.Append(GetConstructorsDefinition(type.GetConstructors(BindingFlags), type));

			classString.Append(GetMethodsDefinition(type.GetMethods(BindingFlags)));

			classString.Append(GetEventsDefinition(type.GetEvents(BindingFlags)));

			classString.Append('}');
			return classString.ToString();
		}

		public string GetFieldsDefinition(FieldInfo[] fields)
		{
			var fieldsString = new StringBuilder();
			foreach (var field in fields)
			{
				if (field.GetCustomAttribute<CompilerGeneratedAttribute>() is null)
				{
					var accessModifier = field.GetAccessModifier().ToFriendlyString();
					var isStatic = field.IsStatic ? " static" : string.Empty;
					var isReadonlyOrConst = field.IsInitOnly ? " readonly" : field.IsLiteral ? " const" : string.Empty;

					fieldsString.Append($"\t{accessModifier}{isStatic}{isReadonlyOrConst} {field.FieldType.GetTypeName()} {field.Name};\n");
				}
			}
			return fieldsString.ToString();
		}

		public string GetProperiesDefinition(PropertyInfo[] properties)
		{
			var propertiesString = new StringBuilder();
			foreach (var property in properties)
			{
				var accessModifier = property.GetAccessModifier().ToFriendlyString();
				var propertyType = property.PropertyType.GetTypeName();
				var getter = string.Empty;
				var setter = string.Empty;
				var isPropStatic = property.GetMethod?.IsStatic ?? property.SetMethod?.IsStatic ?? false;
				var staticString = isPropStatic ? " static" : string.Empty;

				if (property.GetMethod != null)
				{
					var getModifier = property.GetMethod.GetAccessModifier().ToFriendlyString();
					getter = string.Concat(!getModifier.Equals(accessModifier) ? $" {getModifier} " : " ", "get;");
				}

				if (property.SetMethod != null)
				{
					var setModifier = property.SetMethod.GetAccessModifier().ToFriendlyString();
					setter = string.Concat(!setModifier.Equals(accessModifier) ? $" {setModifier} " : " ", "set;");
				}

				propertiesString.Append($"\t{accessModifier}{staticString} {propertyType} {property.Name} {{{getter}{setter} }}\n");
			}
			return propertiesString.ToString();
		}

		public string GetConstructorsDefinition(ConstructorInfo[] constructors, Type type)
		{
			var constructorString = new StringBuilder();
			foreach (var constructor in constructors)
			{
				var accessModifier = constructor.GetAccessModifier().ToFriendlyString();
				var parameters = constructor.GetParameters();

				constructorString.Append($"\t{accessModifier} {type.GetTypeName()}(");
				constructorString.Append(string.Join(", ", Array.ConvertAll(parameters, p => $"{p.ParameterType.GetTypeName()} {p.Name}")));
				constructorString.Append(") { }\n");
			}
			return constructorString.ToString();
		}

		public string GetMethodsDefinition(MethodInfo[] methods)
		{
			var methodString = new StringBuilder();
			foreach (var method in methods)
			{
				if (!method.IsSpecialName)
				{
					var accessModifier = method.GetAccessModifier().ToFriendlyString();
					var isStatic = method.IsStatic ? " static" : string.Empty;
					var returnType = method.ReturnType == typeof(void) ? "void" : method.ReturnType.GetTypeName();
					var methodName = method.GetTypeName();
					var parameters = method.GetParameters();

					methodString.Append($"\t{accessModifier}{isStatic} {returnType} {methodName}(");
					methodString.Append(string.Join(", ", Array.ConvertAll(parameters, p =>
					{
						var paramModifier = p.IsIn ? "in " : p.IsOut ? "out " : p.ParameterType.IsByRef ? "ref " : string.Empty;
						return $"{paramModifier}{p.ParameterType.GetTypeName()} {p.Name}";
					})));
					methodString.Append(") { }\n");
				}
			}
			return methodString.ToString();
		}

		public string GetEventsDefinition(EventInfo[] events)
		{
			var eventsString = new StringBuilder();
			foreach (var eventInfo in events)
			{
				var accessModifier = eventInfo.GetAccessModifier().ToFriendlyString();
				eventsString.Append($"\t{accessModifier} event {eventInfo.EventHandlerType.GetTypeName()} {eventInfo.Name};\n");
			}
			return eventsString.ToString();
		}
	}
}