using System;

namespace DBUsageInspector
{
    public class ReferenceObject : IComparable<ReferenceObject>
    {
        public string Name;
        public string Relationship;
        public string Schema;
        public string Type;

        public ReferenceObject(string name, string type, string relationship, string schema = "")
        {
            Name = name;
            Relationship = relationship;
            Schema = schema;
            Type = type;
        }

        public int CompareTo(ReferenceObject other)
        {
            if (other == null)
                return 1;
            else
            {
                if (Type == other.Type)                     // If the type is the same
                    return Name.CompareTo(other.Name);          // Compare the names; Ignore schema as it can't be trusted definitively as it can be excluded, leave that determination up to a human
                else
                    return Type.CompareTo(other.Type);
            }
        }
    }
}