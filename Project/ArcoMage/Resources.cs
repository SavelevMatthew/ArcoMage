using System;
using System.Collections.Generic;

namespace ArcoMage
{
    public class Resources
    {
        private readonly Dictionary<string, Resource> res;

        public Resources(Dictionary<string, Resource> resources)
        {
            res = resources;
        }

        public Resource this[string name]
        {
            get
            {
                if (res.ContainsKey(name))
                    return res[name];
                throw new ArgumentException("No such resource!");
            }
        }
    }
}
