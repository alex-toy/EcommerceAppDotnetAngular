﻿namespace API.Helpers
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> items)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Items = items;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; } = 0;
        public IReadOnlyList<T> Items { get; set; }
    }
}
