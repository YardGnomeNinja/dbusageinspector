using System;

namespace DBUsageInspector
{
    public class ReferenceObject : IComparable<ReferenceObject>
    {
        public string Name;
        public string Type;
        public string Relationship;

        public ReferenceObject(string name, string type, string relationship)
        {
            Name = name;
            Type = type;
            Relationship = relationship;
        }

        public int CompareTo(ReferenceObject other)
        {
            if (other == null)
                return 1;
            else
            {
                if (Type == other.Type)
                    return Name.CompareTo(other.Name);
                else
                    return Type.CompareTo(other.Type);
            }
        }
    }
}
