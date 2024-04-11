namespace ReflectionDocumenter
{
	public delegate T MyGenericEventHandler<T, F>(T data1, F data2);

	public class ExampleClass<T, F>
	{
		#region Fields

		public T PublicField;
		private T PrivateField;
		protected T ProtectedField;
		internal T InternalField;
		protected internal T ProtectedInternalField;
		private protected T PrivateProtectedField;
		public const int ConstField = 0;
		public static T StaticField;
		public readonly T ReadonlyField;
		public DateTime? NullableField;
		public List<T> GenericTypeField;
		public Dictionary<T, F> GenericType2Field;
		public T[] ArrayField;

		#endregion

		#region Properties

		public T PublicPropertyGetSet { get; set; }
		private T PrivatePropertyGetSet { get; set; }
		protected T ProtectedPropertyGetSet { get; set; }
		internal T InternalPropertyGetSet { get; set; }
		protected internal T ProtectedInternalPropertyGetSet { get; set; }
		private protected T PrivateProtectedPropertyGetSet { get; set; }
		public static T StaticPropertyGetSet { get; set; }
		public DateTime? NullablePropertyGetSet { get; set; }
		public List<T> GenericTypePropertyGetSet { get; set; }
		public Dictionary<T, F> GenericType2PropertyGetSet { get; set; }
		public T[] ArrayPropertyGetSet { get; set; }

		public T PublicPropertyGet => PublicField;
		public T PublicPropertySet
		{
			set => PublicField = value;
		}

		public T PublicPropertyGetProtectedSet { get; protected set; }
		public T PublicPropertyGetInternalSet { get; internal set; }
		public T PublicPropertyGetProtectedInternalSet { get; protected internal set; }
		public T PublicPropertyGetPrivateSet { get; private set; }
		public T PublicPropertyGetProtectedPrivateSet { get; private protected set; }

		public T PublicPropertySetProtectedGet { protected get; set; }
		public T PublicPropertySetInternalGet { internal get; set; }
		public T PublicPropertySetProtectedInternalGet { protected internal get; set; }
		public T PublicPropertySetPrivateGet { private get; set; }
		public T PublicPropertySetProtectedPrivateGet { private protected get; set; }

		#endregion

		#region Constructors

		public ExampleClass() { }
		private ExampleClass(T type) { }
		protected ExampleClass(F type) { }
		internal ExampleClass(T type1, F type2) { }
		protected internal ExampleClass(List<T> type) { }
		private protected ExampleClass(T[] type) { }

		#endregion

		#region Methods

		public void PublicMethod() { }
		private void PrivateMethod() { }
		protected void ProtectedMethod() { }
		internal void InternalMethod() { }
		protected internal void ProtectedInternalMethod() { }
		private protected void PrivateProtectedMethod() { }
		public void PublicMethodWithArgs(int intArg, DateTime? nullableArg, List<T> genericTypeArg, Dictionary<T, F> genericType2Arg, T[] arrayArg) { }
		public void PublicMethodWithInOutRefArgs(in List<T> genericInArg, ref int intRefArg, out DateTime? nullableOutArg) => nullableOutArg = default;
		public decimal PublicMethodWithReturnValue() => decimal.MaxValue;
		public DateTime? PublicMethodWithReturnNullableValue() => DateTime.Now;
		public List<T> PublicMethodWithReturnGenericTypeValue() => new List<T>();
		public Dictionary<T, F> PublicMethodWithReturnGenericType2Value() => new Dictionary<T, F>();
		public T[] PublicMethodWithReturnArrayValue() => new T[10];
		public void PublicGenericMethod<G, E>() { }

		#endregion

		#region Events

		public event EventHandler PublicEvent;
		private event EventHandler PrivateEvent;
		protected event EventHandler ProtectedEvent;
		internal event EventHandler InternalEvent;
		protected internal event EventHandler ProtectedInternalEvent;
		private protected event EventHandler PrivateProtectedEvent;
		public event MyGenericEventHandler<int, double> GenericEvent;

		#endregion
	}
}