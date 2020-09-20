using System;
using Optional;

namespace SharpCode
{
    public class PropertyBuilder
    {
        private readonly Property _property = new Property();

        internal PropertyBuilder() { }

        internal PropertyBuilder(AccessModifier accessModifier, string type, string name)
        {
            _property.AccessModifier = accessModifier;
            _property.Type = type;
            _property.Name = name;
        }

        internal PropertyBuilder(AccessModifier accessModifier, Type type, string name)
            : this(accessModifier, type.Name, name)
        {
        }

        /// <summary>
        /// Sets the access modifier of the property being built.
        /// </summary>
        public PropertyBuilder WithAccessModifier(AccessModifier accessModifier)
        {
            _property.AccessModifier = accessModifier;
            return this;
        }

        /// <summary>
        /// Sets the type of the property being built.
        /// </summary>
        public PropertyBuilder WithType(string type)
        {
            _property.Type = type;
            return this;
        }

        /// <summary>
        /// Sets the type of the property being built.
        /// </summary>
        public PropertyBuilder WithType(Type type)
        {
            _property.Type = type.Name;
            return this;
        }

        /// <summary>
        /// Sets the name of the property being built.
        /// </summary>
        public PropertyBuilder WithName(string name)
        {
            _property.Name = name;
            return this;
        }

        /// <summary>
        /// Sets the getter logic of the property being built.
        /// </summary>
        /// <example>
        /// This example shows the generated code for a property with a getter that is an expression.
        /// 
        /// <code>
        /// // PropertyBuilder.WithName("Identifier").WithGetter("_id")
        /// 
        /// public int Identifier
        /// {
        ///     get => _id;
        /// }
        /// </code>
        /// </example>
        /// <example>
        /// This example shows the generated code for a property with a getter that has a block body.
        /// 
        /// <code>
        /// // PropertyBuilder.WithName("IsCreated").WithGetter("{ if (_id == -1) { return false; } else { return true; } }")
        /// 
        /// public int Identifier
        /// {
        ///     get
        ///     {
        ///         if (_id == -1)
        ///         {
        ///             return false;
        ///         }
        ///         else
        ///         {
        ///             return true;
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public PropertyBuilder WithGetter(string expression)
        {
            _property.Getter = Option.Some(expression);
            return this;
        }

        /// <summary>
        /// Specifies that the property being built should not have a getter.
        /// </summary>
        public PropertyBuilder WithoutGetter()
        {
            _property.Getter = Option.None<string>();
            return this;
        }

        /// <summary>
        /// Sets the setter logic of the property being built.
        /// </summary>
        /// <param name="expression">
        /// The logic of the setter. Can be an expression or a block of code (wrapped in <c>{}</c>). The logic can
        /// make use of the <c>value</c> provided to the property setter.
        /// </param>
        /// <example>
        /// This example shows the generated code for a property with a setter that is an expression.
        /// 
        /// <code>
        /// // PropertyBuilder.WithName("Identifier").WithSetter("_id = value")
        /// 
        /// public int Identifier
        /// {
        ///     set => _id = value;
        /// }
        /// </code>
        /// </example>
        /// <example>
        /// This example shows the generated code for a property with a setter that has a block body.
        /// 
        /// <code>
        /// // PropertyBuilder.WithName("Value").WithSetter("{ if (value <= 0) { throw new Exception(); } _value = value; }")
        /// 
        /// public int Identifier
        /// {
        ///     set
        ///     {
        ///         if (_id == -1)
        ///         {
        ///             return false;
        ///         }
        ///         else
        ///         {
        ///             return true;
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public PropertyBuilder WithSetter(string expression)
        {
            _property.Setter = Option.Some(expression);
            return this;
        }

        /// <summary>
        /// Specifies that the property being built should not have a setter.
        /// </summary>
        public PropertyBuilder WithoutSetter()
        {
            _property.Setter = Option.None<string>();
            return this;
        }

        /// <summary>
        /// Returns the source code of the built property.
        /// </summary>
        /// <param name="formatted">
        /// Indicates whether to format the source code.
        /// </param>
        public string ToSourceCode(bool formatted = true)
        {
            return Build().ToSourceCode(formatted);
        }

        /// <summary>
        /// Returns the source code of the built property.
        /// </summary>
        public override string ToString()
        {
            return ToSourceCode();
        }

        internal Property Build()
        {
            if (string.IsNullOrEmpty(_property.Name))
            {
                throw new MissingBuilderSettingException(
                    "Providing the name of the property is required when building a property.");
            }
            else if (string.IsNullOrEmpty(_property.Type))
            {
                throw new MissingBuilderSettingException(
                    "Providing the type of the property is required when building a property.");
            }

            return _property;
        }
    }
}
