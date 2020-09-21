using Optional;

namespace SharpCode
{
    public class ClassBuilder
    {
        private readonly Class _class = new Class();

        internal ClassBuilder() { }

        internal ClassBuilder(AccessModifier accessModifier, string name)
        {
            _class.AccessModifier = accessModifier;
            _class.Name = name;
        }

        /// <summary>
        /// Sets the access modifier of the class being built.
        /// </summary>
        public ClassBuilder WithAccessModifier(AccessModifier accessModifier)
        {
            _class.AccessModifier = accessModifier;
            return this;
        }

        /// <summary>
        /// Sets the name of the class being built.
        /// </summary>
        public ClassBuilder WithName(string name)
        {
            _class.Name = name;
            return this;
        }

        /// <summary>
        /// Sets the class that the class being built inherits from.
        /// </summary>
        public ClassBuilder WithInheritedClass(string name)
        {
            _class.InheritedClass = Option.Some(name);
            return this;
        }

        /// <summary>
        /// Adds an interface to the list of interfaces that the class being built implements.
        /// </summary>
        public ClassBuilder WithImplementedInterface(string name)
        {
            _class.ImplementedInterfaces.Add(name);
            return this;
        }

        /// <summary>
        /// Adds a field to the class being built.
        /// </summary>
        public ClassBuilder WithField(FieldBuilder builder)
        {
            _class.Fields.Add(builder.Build());
            return this;
        }
        
        /// <summary>
        /// Adds a bunch of fields to the class being built.
        /// </summary>
        public ClassBuilder WithFields(params FieldBuilder[] builders)
        {
            foreach(var builder in builders)
            {
                _class.Fields.Add(builder.Build());
            }
            
            return this;
        }

        /// <summary>
        /// Adds a property to the class being built.
        /// </summary>
        public ClassBuilder WithProperty(PropertyBuilder builder)
        {
            _class.Properties.Add(builder.Build());
            return this;
        }
        
        /// <summary>
        /// Adds a bunch of properties to the class being built.
        /// </summary>
        public ClassBuilder WithProperties(params PropertyBuilder[] builders)
        {
            foreach(var builder in builders)
            {
                _class.Properties.Add(builder.Build());
            }
            
            return this;
        }

        /// <summary>
        /// Adds a constructor to the class being built.
        /// </summary>
        public ClassBuilder WithConstructor(ConstructorBuilder builder)
        {
            _class.Constructors.Add(builder.Build());
            return this;
        }

        /// <summary>
        /// Returns the source code of the built class.
        /// </summary>
        /// <param name="formatted">
        /// Indicates whether to format the source code.
        /// </param>
        public string ToSourceCode(bool formatted = true)
        {
            return Build().ToSourceCode(formatted);
        }

        /// <summary>
        /// Returns the source code of the built class.
        /// </summary>
        public override string ToString()
        {
            return ToSourceCode();
        }

        internal Class Build()
        {
            if (string.IsNullOrEmpty(_class.Name))
            {
                throw new MissingBuilderSettingException(
                    "Providing the name of the class is required when building a class.");
            }

            _class.Constructors.ForEach(ctor => ctor.ClassName = _class.Name);
            return _class;
        }
    }
}
