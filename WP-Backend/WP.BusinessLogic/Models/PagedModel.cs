using System;
using System.Collections.Generic;
using System.Text;

namespace WP.BusinessLogic.Models
{
    public class PagedModel<T>
    {
        public PagedModel(T item, int count, int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            PagedItem = item;
        }

        public int CurrentPage { get; private set; }

        public int TotalPages { get; private set; }

        public int PageSize { get; private set; }

        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < TotalPages;

        public T PagedItem { get; private set; }

        public static PagedModel<T> ToPagedModel(T item, int count, int pageNumber, int pageSize) =>
            new PagedModel<T>(item, count, pageNumber, pageSize);
    }
}
