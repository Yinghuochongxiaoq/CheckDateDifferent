#region Version Info
//======================================================================
// Copyright (c) 苏州同程旅游网络科技有限公司. All rights reserved.
// 命名空间：CheckDateDifferent
// 文件名称： SetCollection
// 创 建 人：FrshMan
// 创建日期：2016/4/20 16:58:08
// 用    途：记录类用途
//=======================================================================
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckDateDifferent
{
    public class Set<TElement>
    {
        int[] buckets;
        public Slot[] Slots;
        int count;
        int freeList;
        IEqualityComparer<TElement> comparer;

        public Set() : this(null) { }

        public Set(IEqualityComparer<TElement> comparer)
        {
            if (comparer == null) comparer = EqualityComparer<TElement>.Default;
            this.comparer = comparer;
            buckets = new int[7];
            this.Slots = new Slot[7];
            freeList = -1;
        }

        public bool Add(TElement value)
        {
            return !Find(value, true);
        }

        public bool Contains(TElement value)
        {
            return Find(value, false);
        }

        public bool Remove(TElement value)
        {
            int hashCode = InternalGetHashCode(value);
            int bucket = hashCode % buckets.Length;
            int last = -1;
            for (int i = buckets[bucket] - 1; i >= 0; last = i, i = this.Slots[i].next)
            {
                if (this.Slots[i].hashCode == hashCode && comparer.Equals(this.Slots[i].value, value))
                {
                    if (last < 0)
                    {
                        buckets[bucket] = this.Slots[i].next + 1;
                    }
                    else
                    {
                        this.Slots[last].next = this.Slots[i].next;
                    }
                    this.Slots[i].hashCode = -1;
                    this.Slots[i].value = default(TElement);
                    this.Slots[i].next = freeList;
                    freeList = i;
                    return true;
                }
            }
            return false;
        }

        bool Find(TElement value, bool add)
        {
            int hashCode = InternalGetHashCode(value);
            for (int i = buckets[hashCode % buckets.Length] - 1; i >= 0; i = this.Slots[i].next)
            {
                if (this.Slots[i].hashCode == hashCode && comparer.Equals(this.Slots[i].value, value)) return true;
            }
            if (add)
            {
                int index;
                if (freeList >= 0)
                {
                    index = freeList;
                    freeList = this.Slots[index].next;
                }
                else
                {
                    if (count == this.Slots.Length) Resize();
                    index = count;
                    count++;
                }
                int bucket = hashCode % buckets.Length;
                this.Slots[index].hashCode = hashCode;
                this.Slots[index].value = value;
                this.Slots[index].next = buckets[bucket] - 1;
                buckets[bucket] = index + 1;
            }
            return false;
        }

        void Resize()
        {
            int newSize = checked(count * 2 + 1);
            int[] newBuckets = new int[newSize];
            Slot[] newSlots = new Slot[newSize];
            Array.Copy(this.Slots, 0, newSlots, 0, count);
            for (int i = 0; i < count; i++)
            {
                int bucket = newSlots[i].hashCode % newSize;
                newSlots[i].next = newBuckets[bucket] - 1;
                newBuckets[bucket] = i + 1;
            }
            buckets = newBuckets;
            this.Slots = newSlots;
        }

        internal int InternalGetHashCode(TElement value)
        {
            return (value == null) ? 0 : comparer.GetHashCode(value) & 0x7FFFFFFF;
        }

        public struct Slot
        {
            internal int hashCode;
            internal TElement value;
            internal int next;
        }
    }

    public class MyEnumberable
    {
        static IEnumerable<TSource> ExceptIterator<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            Set<TSource> set = new Set<TSource>(comparer);
            foreach (TSource element in second) set.Add(element);
            foreach (TSource element in first)
                if (set.Add(element)) yield return element;
        }

        public static IEnumerable<TSource> Except<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null) return null; ;
            if (second == null) return null;
            return ExceptIterator(first, second,null);
        }
    }
}
