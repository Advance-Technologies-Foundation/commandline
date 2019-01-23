// Copyright 2005-2015 Giacomo Stelluti Scala & Contributors. All rights reserved. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommandLine.Core
{
    sealed class Verb
    {
        public Verb(string name, string[] aliases,string helpText, bool hidden = false)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Aliases = aliases;
            this.HelpText = helpText ?? throw new ArgumentNullException(nameof(helpText));
            this.Hidden = hidden;
        }

		public string Name { get; }

		public string[] Aliases { get; }

		public string HelpText { get; }

		public bool Hidden { get; }

		public static Verb FromAttribute(VerbAttribute attribute)
        {
            return new Verb(
                attribute.Name,
                attribute.Aliases,
                attribute.HelpText,
                attribute.Hidden
                );
        }

        public static IEnumerable<Tuple<Verb, Type>> SelectFromTypes(IEnumerable<Type> types)
        {
            return from type in types
                   let attrs = type.GetTypeInfo().GetCustomAttributes(typeof(VerbAttribute), true).ToArray()
                   where attrs.Length == 1
                   select Tuple.Create(
                       FromAttribute((VerbAttribute)attrs.Single()),
                       type);
        }
    }
}
