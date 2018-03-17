
/*
Map.cs

Copyright © 2018 Pluto Scarab Software. Most Rights Reserved.
Author: Bret Mulvey

This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/.

*/

using System;
using System.Collections.Generic;

namespace Foundations.Collections
{
    /// <summary>
    /// A one-to-one mapping between to sets of values.
    /// </summary>
    public class Map<T, U>
    {
        Dictionary<T, U> fwd;
        Dictionary<U, T> rev;

        /// <summary>
        /// Creates an instance of a <see cref="Map{T, U}"/>.
        /// </summary>
        public Map()
        {
            fwd = new Dictionary<T, U>();
            rev = new Dictionary<U, T>();
        }

        /// <summary>
        /// Add a matched pair of items to the map.
        /// </summary>
        public void Add(T t, U u)
        {
            fwd[t] = u;
            rev[u] = t;
        }

        /// <summary>
        /// Look up the <typeparamref name="T"/> the corresponds to the <typeparamref name="U"/>.
        /// </summary>
        public T this[U u]
        {
            get 
            { 
                return rev[u]; 
            }
            set 
            {
                U u1;
                if (fwd.TryGetValue(value, out u1))
                {
                    rev.Remove(u1);
                }
                T t;
                if (rev.TryGetValue(u, out t))
                {
                    fwd.Remove(t);
                }
                rev[u] = value;
                fwd[value] = u;
            }
        }

        /// <summary>
        /// Look up the <typeparamref name="T"/> the corresponds to the <typeparamref name="U"/>.
        /// </summary>
        public U this[T t]
        {
            get 
            { 
                return fwd[t]; 
            }
            set 
            {
                T t1;
                if (rev.TryGetValue(value, out t1))
                {
                    fwd.Remove(t1);
                }
                U u;
                if (fwd.TryGetValue(t, out u))
                {
                    rev.Remove(u);
                }
                fwd[t] = value;
                rev[value] = t;
            }
        }

        /// <summary>
        /// Removes the matched pair containing the <typeparamref name="T"/>.
        /// </summary>
        public void Remove(T t)
        {
            rev.Remove(fwd[t]);
            fwd.Remove(t);
        }

        /// <summary>
        /// Removes the matched pair containing the <typeparamref name="U"/>.
        /// </summary>
        public void Remove(U u)
        {
            fwd.Remove(rev[u]);
            rev.Remove(u);
        }

        /// <summary>
        /// Determines if the map contains a match for <paramref name="t"/>.
        /// </summary>
        public bool Contains(T t)
        {
            return fwd.ContainsKey(t);
        }

        /// <summary>
        /// Determines if the map contains a match for <paramref name="u"/>.
        /// </summary>
        public bool Contains(U u)
        {
            return rev.ContainsKey(u);
        }
    }
}
